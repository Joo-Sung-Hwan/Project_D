using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Monster : MonoBehaviour
{
    #region 변수/구조체 선언

    #region 선언 - 데이터
    public struct monster_Data
    {
        public Monsters_Index index;
        public Element_Type element_Type;
        public float maxHP;
        public float curHP;
        public float speed;
        public float speed_origin;
        public float armor;
    }
    public monster_Data md;
    #endregion

    #region 선언 - 이동
    Position_Index index = 0;
    Position_Index nextIndex = 0;
    float rotated = 0;
    public float rotateSpeed = 0;
    public float moved = 0;
    int ranmove;
    #endregion

    #region 선언 - 체력
    [SerializeField] Hp_Bar hpBar_Prf;
    protected Hp_Bar hpBar;
    #endregion

    #region 선언 - 디버프
    Dictionary<Debuff_Type, bool> isDebuff_dic = new Dictionary<Debuff_Type, bool>();
    float slowTime = 0;
    float stunTime = 0;
    float burnTime = 0;
    #endregion

    public Animator anim;

    #endregion

    void Update()
    {
        Move();
    }

    public virtual void Init()
    {
        if (hpBar == null)
        {
            hpBar = Instantiate(hpBar_Prf, MapManager.instance.uiManager_ingame.canvas_hp.transform);
            hpBar.transform.SetParent(MapManager.instance.uiManager_ingame.bar_Parent);
        }
        hpBar.monster = this;
        anim = GetComponent<Animator>();
    }

    private void Move()
    {
        anim.SetInteger("w&r", ranmove = Random.Range(1,3));
        //index - 왼쪽:0, 아래:1, 오른쪽:2, 위:3;
        if (index != Position_Index.other)
            moved += md.speed * Time.deltaTime;

        switch (index)
        {
            case Position_Index.left:
                transform.Translate(Vector3.forward * md.speed * Time.deltaTime);
                if (transform.localPosition.x >= 6)
                {
                    index = Position_Index.other;
                    nextIndex = Position_Index.down;
                }
                break;
            case Position_Index.down:
                transform.Translate(Vector3.forward * md.speed * Time.deltaTime);
                if (transform.localPosition.z >= 6)
                {
                    index = Position_Index.other;
                    nextIndex = Position_Index.right;
                }
                break;
            case Position_Index.right:
                transform.Translate(Vector3.forward * md.speed * Time.deltaTime);
                if (transform.localPosition.x <= 0)
                {
                    index = Position_Index.other;
                    nextIndex = Position_Index.up;
                }
                break;
            case Position_Index.up:
                transform.Translate(Vector3.forward * md.speed * Time.deltaTime);
                if (transform.localPosition.z <= 1)
                    Dead();
                break;
            default:
                if (rotateSpeed == 0)
                {
                    transform.Rotate(new Vector3(0, -1, 0) * 90);
                    index = nextIndex;
                    break;
                }
                else
                {
                    transform.Rotate(new Vector3(0, -1, 0) * rotateSpeed * Time.deltaTime);
                    rotated += 120 * Time.deltaTime;
                    if (rotated >= 90)
                    {
                        index = nextIndex;
                        rotated = 0;
                    }
                    break;
                }
        }
    }

    public void PoolInit(Transform mm_trans)
    {
        transform.rotation = Quaternion.Euler(0, 90, 0);
        transform.position = mm_trans.position;
        Init();
        index = 0;
        nextIndex = 0;
        rotated = 0;
        moved = 0;
        slowTime = 0;
        stunTime = 0;
        burnTime = 0;
        hpBar.gameObject.SetActive(true);
        hpBar.hpbar.fillAmount = 1;
    }

    #region 피격

    public void Damaged(float damage , Damage_Type damage_type , float element_const, Debuff_Type debuff_Type = Debuff_Type.none , float debuffTime = 0)
    {
        switch (debuff_Type)
        {
            case Debuff_Type.slow:
                Debuff_Slow(debuffTime , 70);
                break;
            case Debuff_Type.stun:
                Debuff_Stun(debuffTime);
                break;
            case Debuff_Type.burn:
                Debuff_Burn(debuffTime);
                break;
            default:
                break;
        }

        switch (damage_type)
        {
            case Damage_Type.physic:
                md.curHP -= damage > md.armor ? (damage - md.armor) * element_const : 0;
                break;
            case Damage_Type.magic:
                md.curHP -= damage * element_const;
                break;
            case Damage_Type.trueType:
                md.curHP -= damage * element_const;
                break;
            default:
                break;
        }
        
        hpBar.hpbar.fillAmount = md.curHP / md.maxHP;
        if (md.curHP<=0)
            Dead();
    }
    private void Dead()
    {
        //StopAllCoroutines();
        gameObject.SetActive(false);
        Dictionary<Monsters_Index, Queue<Monster>> dm = MapManager.instance.monsterManager.d_monsters;
        if (!dm.ContainsKey(md.index))
            dm.Add(md.index, new Queue<Monster>());

        //anim.SetTrigger("die");
        dm[md.index].Enqueue(this);
        hpBar.gameObject.SetActive(false);
        MapManager.instance.monsterManager.killed++;
    }

    private void OnMouseDown()
    {
        Dead();
    }

    #region 디버프
    public void AddDebuff_Dic(Debuff_Type debuff_Type)
    {
        if (!isDebuff_dic.ContainsKey(debuff_Type))
            isDebuff_dic.Add(debuff_Type, true);
        else
            isDebuff_dic[debuff_Type] = true;
    }

    #region 디버프 - 둔화
    public void Debuff_Slow(float time , float rate_percent)
    {
        float preSlowTime = slowTime;
        slowTime = time;
        if(preSlowTime <=0)
            StartCoroutine(C_Debuff_Slow(rate_percent));
    }

    public IEnumerator C_Debuff_Slow(float rate_percent)
    {
        AddDebuff_Dic(Debuff_Type.slow);
        md.speed *= ((100 - rate_percent) / 100f);
        while (slowTime > 0)
        {
            yield return new WaitForEndOfFrame();
            slowTime -= Time.deltaTime;
        }
        md.speed = md.speed_origin;
        isDebuff_dic[Debuff_Type.slow] = false;
    }
    #endregion

    #region 디버프 - 스턴
    public void Debuff_Stun(float time)
    {
        float preStunTime = stunTime;
        stunTime = time;
        if (preStunTime <= 0)
            StartCoroutine(C_Debuff_Stun());
    }

    public IEnumerator C_Debuff_Stun()
    {
        AddDebuff_Dic(Debuff_Type.stun);
        while (stunTime > 0)
        {
            md.speed = 0;
            yield return new WaitForEndOfFrame();
            stunTime -= Time.deltaTime;
        }
        md.speed = md.speed_origin;
        isDebuff_dic[Debuff_Type.stun] = false;
    }
    #endregion

    #region 디버프 - 화상
    public void Debuff_Burn(float time)
    {
        float preBurnTime = burnTime;
        burnTime = time;
        if (preBurnTime <= 0)
            StartCoroutine(C_Debuff_Burn());
    }

    public IEnumerator C_Debuff_Burn()
    {
        AddDebuff_Dic(Debuff_Type.burn);
        float i = 0;
        while (burnTime > 0)
        {
            yield return new WaitForEndOfFrame();

            burnTime -= Time.deltaTime;
            i += Time.deltaTime;
            if (i >= 0.5f)
            {
                //화상 데미지 고정? -> 조정 필요
                Damaged(3, Damage_Type.trueType, 1);
                i = 0;
            }
        }
        isDebuff_dic[Debuff_Type.burn] = false;
    }
    #endregion
    #endregion
    #endregion
}
