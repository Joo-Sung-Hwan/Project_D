using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class  Unit : MonoBehaviour
{
    #region 변수/구조체 선언
    #region 선언 - 데이터
    public struct unit_Data
    {
        public Element_Type element_type;
        public Attack_Type atk_type;
        public Mana_Type mana_type;
        public float atkDelay;
        public float attack;
        public float maxMana;
        public float curMana;
    }
    public unit_Data ud;
    #endregion

    #region 선언 - 마나
    [SerializeField] protected Mp_Bar mpBar_Prf;
    protected Mp_Bar mpBar;
    protected bool canManaRestore = true;
    protected bool isManaRestore = false;
    #endregion

    //자기 자신
    [SerializeField] MovableObj movable;
    public int level = 1;
    protected bool canAttack = true;
    protected bool union;
    #endregion

    protected virtual void Init()
    {
        mpBar = Instantiate(mpBar_Prf, MapManager.instance.uiManager_ingame.canvas_hp.transform);
        mpBar.unit = this;
    }

    public void Init_Wave(bool isWave)
    {
        if (isWave && !movable.block.isWaiting)
            mpBar.gameObject.SetActive(true);
        else
        {
            mpBar.gameObject.SetActive(false);
            ud.curMana = 0;
            mpBar.mpbar.fillAmount = 0;
        }   
    }

    // Update is called once per frame
    void Update()
    {
        if (ud.curMana >= ud.maxMana)
            StartCoroutine(UseSkill());

        Test_ColorChange_isWave();

        if (movable.block.isWaiting || !MapManager.instance.monsterManager.isWave)
            return;


        if (canAttack)
            Attack();

        if (ud.mana_type == Mana_Type.auto && !isManaRestore)
            StartCoroutine(ManaRestore_Auto());

    }

    void DestroyUnit()
    {
        Destroy(gameObject);
        Destroy(mpBar);
        MapManager.instance.unitManager.units.Remove(this);
    }

    #region 유닛 합치기
    private void OnMouseDown()
    {
        union = true;
    }
    private void OnMouseUp()
    {
        union = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (union && !MapManager.instance.monsterManager.isWave)
        {
            if (!collision.gameObject.GetComponent<Unit>())
                return;

            Unit otherUnit = collision.gameObject.GetComponent<Unit>();
            if (ud.element_type == otherUnit.ud.element_type && level == otherUnit.level && level < 3)
            {
                otherUnit.LevelUp_Test();
                DestroyUnit();
            }
        }
    }

    public void LevelUp_Test()
    {
        level++;
        switch (level)
        {
            case 2:
                ud.attack *= 1.5f;
                ud.atk_type = Attack_Type.splash;
                break;

            default:
                Debug.LogError("Error : 잘못된 level값.");
                break;
        }

        Debug.Log($"{name} level : {level}");
    }
    #endregion


    #region 공격
    protected abstract void Attack();

    protected IEnumerator C_Attack(Attack_Type attack_Type, Damage_Type damage_Type, Debuff_Type debuff_Type = Debuff_Type.none, float debuff_Time = 0f)
    {
        Monster first_mob = FindTarget();
        if (first_mob == null)
            yield break;

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
        if (ud.mana_type == Mana_Type.attack)
            ManaRestore_Attack();
        canAttack = false;
        StartCoroutine(Test_ColorChange_Attack());
        yield return new WaitForSeconds(ud.atkDelay);
        canAttack = true;
    }
    #region Attack - 부속함수
    Monster FindTarget()
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

    //상성에 따른 계수
    float Element_Const(Monster monster)
    {
        Element_Type md_et = monster.md.element_Type;
        switch (ud.element_type)
        {
            case Element_Type.none:
                return 1;
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
            default:
                return 1;
        }
    }
    #endregion
    #endregion

    #region 스킬
    //테스트 스킬
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

    // public abstract IEnumerator Skill(Attack_Type attack_Type, Damage_Type damage_Type, Debuff_Type debuff_Type = Debuff_Type.none, float debuff_Time = 0f);

    IEnumerator ManaRestore_Auto()
    {
        //amount = 초당 마나 회복량
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

        // amount = 공격당 마나 회복량
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
    #endregion


    #region 테스트
    IEnumerator Test_ColorChange_Attack()
    {
        GetComponent<Renderer>().material.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        GetComponent<Renderer>().material.color = Color.white;
    }

    void Test_ColorChange_isWave()
    {
        if (GetComponent<Renderer>().material.color != Color.red)
        {
            GetComponent<Renderer>().material.color = MapManager.instance.monsterManager.isWave ? Color.green : Color.white;
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, 2);
    }
    #endregion
}
