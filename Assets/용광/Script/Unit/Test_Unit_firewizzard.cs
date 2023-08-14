using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_Unit_firewizzard : Unit
{
    protected override void Init()
    {
        base.Init();
        ud.element_type = Element_Type.fire;
        ud.atk_type = Attack_Type.normal;
        ud.attack = 30f;
        ud.atkDelay = 0.5f;
        ud.mana_type = Mana_Type.attack;
        ud.maxMana = 10f;
        ud.curMana = 0f;
    }

    protected override void Attack()
    {
        StartCoroutine(C_Attack(ud.atk_type, Damage_Type.physic, Debuff_Type.slow, 2f));
    }

    void Start()
    {
        Init();
    }
}
