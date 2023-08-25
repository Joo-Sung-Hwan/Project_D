using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Golem : Unit
{

    public override void Init()
    {
        base.Init();
        ud.element_type = Element_Type.earth;
        ud.atk_type = Attack_Type.splash;
        ud.mana_type = Mana_Type.auto;
        ud.atkDelay = 3f;
        ud.attack = 35f;
        ud.maxMana = 10f;
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
        particle.EffStart(1, 1, null);
        Debug.Log("SKILL");
        ud.curMana = 0;
        yield break;
    }
}
