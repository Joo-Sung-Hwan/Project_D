using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public abstract class  Unit : MonoBehaviour
{
    #region ����/����ü ����
    #region ���� - ������
    public struct unit_Data
    {
        public Element_Type element_type;
        public Attack_Type atk_type;
        public Mana_Type mana_type;
        public float atkDelay;
        public float attack;
        public float maxMana;
        public float curMana;
        public bool isBuy;
    }
    public unit_Data ud;
    #endregion

    #region ���� - ����
    [Header("mp��")]
    [SerializeField] protected Mp_Bar mpBar_Prf;
    protected Mp_Bar mpBar;
    protected bool canManaRestore = true;
    protected bool isManaRestore = false;
    #endregion

    #region ���� - ����
    float clickTime = 0f;
    #endregion

    [Header("�ڱ� �ڽ�")]
    [SerializeField] MovableObj movable;
    public PhotonView pv;
    public Animator anim;

    public ParticleController particle_Prf;
    [HideInInspector] public ParticleController ptc;

    [HideInInspector] public bool isBuy;
    public int level = 1;
    protected Monster target;
    protected bool canSkill = true;
    protected bool isSkill = false;
    protected bool canAttack = true;
    protected bool union;

    #endregion

    #region Init
    public virtual void Init()
    {
        MapManager.instance.unitManager.AddUnits(this);
        mpBar = Instantiate(mpBar_Prf, MapManager.instance.uiManager_ingame.canvas_hp.transform);
        mpBar.transform.SetParent(MapManager.instance.uiManager_ingame.bar_Parent);
        mpBar.unit = this;
        movable.InitBlock();
        Init_Mp();
        anim = GetComponent<Animator>();
    }

    public void Init_Mp()
    { 
        if (MapManager.instance.monsterManager.isWave && !movable.block.isWating)
            mpBar.gameObject.SetActive(true);
        else
        {
            mpBar.gameObject.SetActive(false);
            SetMana(0);
        }   
    }

    public void Init_Mp(bool isWave)
    {
        if (isWave && !movable.block.isWating)
            mpBar.gameObject.SetActive(true);
        else
        {
            mpBar.gameObject.SetActive(false);
            SetMana(0);
        }
    }
    #endregion

    // Update is called once per frame
    void Update()
    {
        if (movable.block.isWating || !MapManager.instance.monsterManager.isWave)
            return;

        target = FindTarget();
        if (target != null)
        {
            if (ud.curMana >= ud.maxMana && canSkill)
            {
                canSkill = false;
                StartCoroutine(Skill());
            }
                
            else if (canAttack && !isSkill)
                Attack();
        }
        

        if (ud.mana_type == Mana_Type.auto && !isManaRestore)
            StartCoroutine(ManaRestore_Auto());
    }
    
    public void DestroyUnit()
    {
        MapManager.instance.unitManager.units.Remove(this);
        Destroy(gameObject);
        Destroy(mpBar);
    }

    #region ���� ����
    private void OnMouseDown()
    {
        clickTime = 0f;
    }
    private void OnMouseDrag()
    {
        clickTime += Time.deltaTime;
    }
    private void OnMouseUp()
    {
        if (clickTime < 0.2f)
        {
            MapManager.instance.uiManager_ingame.SetActiveInform(transform, this);
            MapManager.instance.uiManager_ingame.information.GetComponent<Information>().sellbtn.onClick.AddListener(() => MapManager.instance.uiManager_ingame.information.GetComponent<Information>().OnSellUnit(this));
        }
    }

    #endregion

    #region ���� ������
    public void LevelUp_Test()
    {
        if(pv.IsMine)
        pv.RPC("RPC_LevelUp", RpcTarget.All);
    }
    
    [PunRPC]
    public void RPC_LevelUp()
    {
        level++;
        switch (level)
        {
            case 2:
                ud.attack *= 1.5f;
                //ud.atk_type = Attack_Type.splash;
                break;

            default:
                Debug.LogError("Error : �߸��� level��.");
                break;
        }
    }
    #endregion


    #region ����
    protected abstract void Attack();

    protected IEnumerator C_Attack(Attack_Type attack_Type, Damage_Type damage_Type, Debuff_Type debuff_Type = Debuff_Type.none, float debuff_Time = 0f)
    {
        Monster first_mob = target;

        #region ������ ���� �κ� (��ƼŬ ��ũ��Ʈ�� �̵�?)
        //�̵��� �� �Լ��� �μ��� �ΰ� �ʿ�, ����ü �߻糪 �ִϸ��̼��� �� �Լ���
        switch (attack_Type)
        {
            case Attack_Type.normal:
                NormalAttack(first_mob , damage_Type, debuff_Type, debuff_Time);
                break;
            case Attack_Type.splash:
                SplashAttack(first_mob , damage_Type, debuff_Type, debuff_Time);
                break;
            default:
                break;
        }
        #endregion
        
        if (ud.mana_type == Mana_Type.attack && canManaRestore)
            ManaRestore_Attack();
        canAttack = false;
        transform.LookAt(first_mob.transform);
        anim.SetTrigger("attack");
        yield return new WaitForSeconds(ud.atkDelay);
        canAttack = true;
    }
    #region Attack - �μ��Լ�
    protected Monster FindTarget()
    {
        float first = 0;
        Collider[] monsters = Physics.OverlapSphere(transform.position, 2);
        Monster first_mob = null;
        foreach (var item in monsters)
        {
            Monster mob;
            if (mob = item.GetComponent<Monster>())
            {
                if (first <= mob.moved)
                {
                    first = mob.moved;
                    first_mob = mob;
                }
            }
        }
        return first_mob;
    }

    void NormalAttack(Monster target , Damage_Type damage_Type, Debuff_Type debuff_Type = Debuff_Type.none, float debuffTime = 0)
    {
        target.Damaged(ud.attack, damage_Type , Element_Const(target) , debuff_Type , debuffTime);
    }

    void SplashAttack(Monster target, Damage_Type damage_Type, Debuff_Type debuff_Type = Debuff_Type.none, float debuffTime = 0)
    {
        Collider[] monsters = Physics.OverlapSphere(target.transform.position, 1);
        foreach (var item in monsters)
        {
            Monster mob;
            if (mob = item.GetComponent<Monster>())
                mob.Damaged(ud.attack, damage_Type, Element_Const(mob), debuff_Type , debuffTime);
        }
    }

    //�󼺿� ���� ���
    float Element_Const(Monster monster)
    {
        Element_Type md_et = monster.md.element_Type;
        switch (ud.element_type)
        {
            case Element_Type.water:
                return md_et == Element_Type.wind ? 0.75f
                    : md_et == Element_Type.fire ? 1.25f
                    : 1;
            case Element_Type.wind:
                return md_et == Element_Type.earth ? 0.75f
                    : md_et == Element_Type.water ? 1.25f
                    : 1;
            case Element_Type.earth:
                return md_et == Element_Type.fire ? 0.75f
                    : md_et == Element_Type.wind ? 1.25f
                    : 1;
            case Element_Type.fire:
                return md_et == Element_Type.water ? 0.75f
                    : md_et == Element_Type.earth ? 1.25f
                    : 1;
            case Element_Type.light:
                return md_et == Element_Type.dark ? 1.25f
                    : 1;
            case Element_Type.dark:
                return md_et == Element_Type.light ? 1.25f
                    : 1;
            default:
                return 1;
        }
    }
    #endregion
    #endregion

    #region ��ų
    //�׽�Ʈ ��ų
    IEnumerator UseSkill()
    {
        ud.curMana = 0;
        float skillTime = 3f;
        float preDelay = ud.atkDelay;
        canManaRestore = false;
        ud.atkDelay /= 2;
        while (skillTime > 0) 
        {
            yield return new WaitForEndOfFrame();
            skillTime -= Time.deltaTime;
            mpBar.mpbar.fillAmount = skillTime /3f;
        }
        ud.atkDelay = preDelay;
        canManaRestore = true;
    }

    public abstract IEnumerator Skill();

    IEnumerator ManaRestore_Auto()
    {
        //amount = �ʴ� ���� ȸ����
        float amount = 1;

        isManaRestore = true;
        while (ud.curMana < ud.maxMana && canManaRestore)
        {
            ud.curMana += Time.deltaTime * amount;
            mpBar.mpbar.fillAmount = ud.curMana / ud.maxMana;
            yield return new WaitForEndOfFrame();
        }

        if (ud.curMana > ud.maxMana)
            ud.curMana = ud.maxMana;

        isManaRestore = false;
    }

    void ManaRestore_Attack()
    {
        if (!canManaRestore)
            return;

        // amount = ���ݴ� ���� ȸ����
        float amount = 1;

        if (ud.curMana < ud.maxMana)
        {
            if (ud.curMana + amount >= ud.maxMana)
                ud.curMana = ud.maxMana;
            else
                ud.curMana += amount;
        }
        mpBar.mpbar.fillAmount = ud.curMana / ud.maxMana;
    }

    protected void SetMana(float mp)
    {
        ud.curMana = mp;
        mpBar.mpbar.fillAmount = ud.curMana / ud.maxMana;
    }
    #endregion


    #region �׽�Ʈ
    IEnumerator Test_ColorChange_Attack()
    {
        GetComponent<Renderer>().material.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        GetComponent<Renderer>().material.color = Color.white;
    }

    /*void Test_ColorChange_isWave()
    {
        if (GetComponent<Renderer>().material.color != Color.red)
        {
            GetComponent<Renderer>().material.color = MapManager.instance.monsterManager.isWave ? Color.green : Color.white;
        }
    }*/
    /*
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, 2);
    }
    */
    #endregion

    #region �ִϸ��̼� event
    //���� �ִϸ��̼� ������ �� ȣ��
    public void EAttack_Start()
    {
        canSkill = false;
    }

    //���� �ִϸ��̼� ���� �� ȣ��
    public void EAttack_End()
    {
        canSkill = true;
    }

    //��ų �ִϸ��̼� ���� �� ȣ��
    public void ESkill_End()
    {
        isSkill = false;
        canManaRestore = true;
    }
    #endregion

    public void ESkill_Particle()
    {
        if (target == null)
            return;
        if (ptc == null)
            ptc = Instantiate(particle_Prf, target.transform);
        else
            ptc.transform.position = target.transform.position;

        ptc.Init();
        ptc.transform.SetParent(MapManager.instance.particle_parent);
        ptc.transform.localScale = Vector3.one;
        ptc.EffStart(1, 1, null);
    }
}