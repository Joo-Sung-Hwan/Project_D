using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragon : Unit
{
    public override void Init()
    {
        base.Init();
        ud.element_type = Element_Type.fire;
        ud.atk_type = Attack_Type.normal;
        ud.mana_type = Mana_Type.auto;
        ud.atkDelay = 1f;
        ud.attack = 30f;
        ud.maxMana = 2f;
        ud.curMana = 0f;
    }
    void Start()
    {
        Init();
    }

    protected override void Attack()
    {
        StartCoroutine(C_Attack(ud.atk_type, Damage_Type.physic));
    }  

    public override IEnumerator Skill()
    {
        Monster target = FindTarget();
        if (!target)
            yield break;
        transform.LookAt(FindTarget().transform);
        particle.gameObject.SetActive(true);
        particle.EffStart(1,1,null);
        Debug.Log("SKILL");
        ud.curMana = 0;
        yield break;
    }
}
