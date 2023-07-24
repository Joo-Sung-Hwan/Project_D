using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class  Unit : MonoBehaviour
{
    public struct unit_Data
    {
        public Unit_Type unit_type;
        public Attack_Type atk_type;
        public float atkDelay;
        public float attack;
    }

    public unit_Data ud;

    [SerializeField] MovableObj movable;

    protected bool canAttack = true;

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

    IEnumerator Attack(Attack_Type type)
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
        if (first_mob == null)
            yield break;

        switch (type)
        {
            case Attack_Type.normal:
                first_mob.Damaged(ud.attack, Damage_Type.physic);
                break;
            case Attack_Type.splash:
                SplashAttack(first_mob);
                break;
            default:
                break;
        }
        canAttack = false;
        StartCoroutine(Test_ColorChange());
        yield return new WaitForSeconds(ud.atkDelay);
        canAttack = true;
    }

    IEnumerator Test_ColorChange()
    {
        GetComponent<Renderer>().material.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        GetComponent<Renderer>().material.color = Color.white;
    }

    void Test_ColorChange_isWave()
    {
        if (GetComponent<Renderer>().material.color!=Color.red)
        {
            GetComponent<Renderer>().material.color = MapManager.instance.monsterManager.isWave ? Color.green : Color.white;
        }
    }

    void SplashAttack(Monster target)
    {
        Collider[] monsters = Physics.OverlapSphere(target.transform.position, 1);
        foreach (var item in monsters)
        {
            Monster mob;
            if (mob = item.GetComponent<Monster>())
                mob.Damaged(ud.attack, Damage_Type.physic);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, 2);
    }
}
