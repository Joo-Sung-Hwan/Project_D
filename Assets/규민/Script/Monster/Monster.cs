using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Monster : MonoBehaviour
{
    public struct monster_Data
    {
        public Monsters_Index index;
        public float maxHP;
        public float curHP;
        public float speed;
        public float armor;
    }
    public monster_Data md;

    int index = 0;
    int preIndex = 0;

    [SerializeField] Hp_Bar hpBar_Prf;
    protected Hp_Bar hpBar;
    float rotated = 0;
    public float moved = 0;
    public Coroutine slowCor;
    public Coroutine stunCor;
    public Coroutine burnCor;
    public float slowTime = 0;
    public float stunTime = 0;
    public float burnTime = 0;


    void Update()
    {
        Move();
    }

    public virtual void Init()
    {
        if (hpBar == null)
            hpBar = Instantiate(hpBar_Prf, MapManager.instance.uiManager_ingame.canvas_hp.transform);
        hpBar.monster = this;
    }

    private void Move()
    {
        //index - 왼쪽:0, 아래:1, 오른쪽:2, 위:3;
        if (index != -1)
            moved += md.speed * Time.deltaTime;

        switch (index)
        {
            case 0:
                transform.Translate(Vector3.forward * md.speed * Time.deltaTime);
                if (transform.localPosition.x >= 6)
                    index = -1;
                break;
            case 1:
                transform.Translate(Vector3.forward * md.speed * Time.deltaTime);
                if (transform.localPosition.z >= 6)
                {
                    index = -1;
                    preIndex = 1;
                }
                break;
            case 2:
                transform.Translate(Vector3.forward * md.speed * Time.deltaTime);
                if (transform.localPosition.x <= 0)
                {
                    index = -1;
                    preIndex = 2;
                }
                break;
            case 3:
                transform.Translate(Vector3.forward * md.speed * Time.deltaTime);
                if (transform.localPosition.z <= 1)
                    Dead();
                break;
            default:
                transform.Rotate(new Vector3(0, -1, 0) * 120 * Time.deltaTime) ;
              //  hpBarPrf.transform.localEulerAngles = new Vector3(0, 90 - transform.rotation.eulerAngles.y, 0);
                rotated += 120 * Time.deltaTime;
                if (rotated >= 90)
                {
                    index = preIndex + 1;
                    rotated = 0;
                }
                break;
        }
    }

    private void Dead()
    {
        gameObject.SetActive(false);
        Dictionary<Monsters_Index, Queue<Monster>> dm = MapManager.instance.monsterManager.d_monsters;
        if (!dm.ContainsKey(md.index))
            dm.Add(md.index, new Queue<Monster>());

        dm[md.index].Enqueue(this);
        hpBar.gameObject.SetActive(false);
        MapManager.instance.monsterManager.killed++;
    }

    public void PoolInit(Transform mm_trans)
    {
        transform.rotation = Quaternion.Euler(0, 90, 0);
        transform.position = mm_trans.position;
        Init();
        index = 0;
        preIndex = 0;
        rotated = 0;
        moved = 0;
        hpBar.gameObject.SetActive(true);
        hpBar.hpbar.fillAmount = 1;
    }

    public void Damaged(float damage , Damage_Type type)
    {
        switch (type)
        {
            case Damage_Type.physic:
                md.curHP -= damage > md.armor ? damage - md.armor : 0;
                break;
            case Damage_Type.magic:
                md.curHP -= damage;
                break;
            case Damage_Type.trueType:
                md.curHP -= damage;
                break;
            default:
                break;
        }
        
        hpBar.hpbar.fillAmount = md.curHP / md.maxHP;
        if (md.curHP<=0)
            Dead();
    }

    private void OnMouseDown()
    {
        Dead();
    }

    /*
    public IEnumerator C_Debuff_Slow(float time ,float percent)
    {
        md.speed /= (100 - percent) / 100f;
        
    }
    */

    public void Debuff_Stun(float time)
    {
        stunTime = time;
        if (stunCor == null) 
            stunCor = StartCoroutine(C_Debuff_Stun());
    }

    public IEnumerator C_Debuff_Stun()
    {
        Debug.Log("Stun");
        while (stunTime <= 0)
        {
            md.speed = 0;
            yield return new WaitForEndOfFrame();
            stunTime -= Time.deltaTime;
        }
    }
        
    public IEnumerator C_Debuff_Burn(float time, float dps)
    {
        burnTime = time;
        while (burnTime <= 0) 
        {
            yield return new WaitForEndOfFrame();
            Damaged(dps, Damage_Type.trueType);
            burnTime -= Time.deltaTime;
        }
    }
}
