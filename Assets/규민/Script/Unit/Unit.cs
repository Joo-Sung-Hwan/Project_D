using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class  Unit : MonoBehaviour
{
    #region 변수/구조체 선언
    #region 선언 - 데이터
    public struct unit_Data
    {
        public Unit_Type unit_type;
        public Attack_Type atk_type;
        public float atkDelay;
        public float attack;
    }
    public unit_Data ud;
    #endregion

    //자기 자신
    [SerializeField] MovableObj movable;
    protected bool canAttack = true;
    #endregion

    protected abstract void Init();

    // Update is called once per frame
    void Update()
    {
        Test_ColorChange_isWave();

        if (movable.block.isWaiting || !MapManager.instance.monsterManager.isWave)
            return;

        if (canAttack)
            StartCoroutine(Attack(ud.atk_type));
    }

    #region 공격
    IEnumerator Attack(Attack_Type type)
    {
        Monster first_mob = FindTarget();
        if (first_mob == null)
            yield break;

        switch (type)
        {
            case Attack_Type.normal:
                NormalAttack(first_mob , Damage_Type.physic , Debuff_Type.slow , 2f);
                break;
            case Attack_Type.splash:
                SplashAttack(first_mob , Damage_Type.physic);
                break;
            default:
                break;
        }
        canAttack = false;
        StartCoroutine(Test_ColorChange());
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

    void NormalAttack(Monster target , Damage_Type damage_Type, Debuff_Type debuff_Type = Debuff_Type.normal, float debuffTime = 0)
    {
        target.Damaged(ud.attack, damage_Type , debuff_Type , debuffTime);
    }

    void SplashAttack(Monster target, Damage_Type damage_Type, Debuff_Type debuff_Type = Debuff_Type.normal, float debuffTime = 0)
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

    #region 테스트
    IEnumerator Test_ColorChange()
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
