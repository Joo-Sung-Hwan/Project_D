using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class  Unit : MonoBehaviour
{
    #region ����/����ü ����
    #region ���� - ������
    public struct unit_Data
    {
        public Unit_Type unit_type;
        public Attack_Type atk_type;
        public Mana_Type mana_type;
        public float atkDelay;
        public float attack;
        public float maxMana;
        public float curMana;
    }
    public unit_Data ud;
    #endregion

    //�ڱ� �ڽ�
    [SerializeField] MovableObj movable;
    public int level = 1;
    protected bool canAttack = true;
    protected bool union;
    #endregion

    protected abstract void Init();

    // Update is called once per frame
    void Update()
    {
        Test_ColorChange_isWave();

        if (movable.block.isWaiting || !MapManager.instance.monsterManager.isWave)
            return;

        if (canAttack)
            StartCoroutine(Attack(ud.atk_type , Damage_Type.physic , Debuff_Type.stun , 2f));
    }

    #region ���� ��ġ��
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
            if (ud.unit_type == otherUnit.ud.unit_type && level == otherUnit.level && level < 3)
            {
                otherUnit.LevelUp_Test();
                Destroy(gameObject);
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
                Debug.Log("Error : level���� �߸��Ǿ����ϴ�.");
                break;
        }

        Debug.Log($"{name} level : {level}");
    }
    #endregion


    #region ����
    IEnumerator Attack(Attack_Type attack_Type, Damage_Type damage_Type, Debuff_Type debuff_Type = Debuff_Type.none, float debuff_Time = 0f)
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
        canAttack = false;
        StartCoroutine(Test_ColorChange_Attack());
        yield return new WaitForSeconds(ud.atkDelay);
        canAttack = true;
    }
    #region Attack - �μ��Լ�
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
        target.Damaged(ud.attack, damage_Type , debuff_Type , debuffTime);
    }

    void SplashAttack(Monster target, Damage_Type damage_Type, Debuff_Type debuff_Type = Debuff_Type.none, float debuffTime = 0)
    {
        Collider[] monsters = Physics.OverlapSphere(target.transform.position, 1);
        foreach (var item in monsters)
        {
            Monster mob;
            if (mob = item.GetComponent<Monster>())
                mob.Damaged(ud.attack, damage_Type, debuff_Type , debuffTime);
        }
    }
    #endregion
    #endregion

    #region ��ų
    void UseSkill()
    {

    }

    // public abstract IEnumerator Skill(Attack_Type attack_Type, Damage_Type damage_Type, Debuff_Type debuff_Type = Debuff_Type.none, float debuff_Time = 0f);

    void ManaRestore()
    {
        switch (ud.mana_type)
        {
            case Mana_Type.auto:
                if (ud.curMana >= ud.maxMana)
                {
                    CancelInvoke("Mana_AutoRestore()");
                    return;

                }
                break;
            case Mana_Type.attack:
                break;
            default:
                break;
        }
        
    }

    #endregion


    #region �׽�Ʈ
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
