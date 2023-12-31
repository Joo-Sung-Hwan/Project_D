using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallenGuardin : Unit
{
    public override void Init()
    {
        base.Init();
        ud.element_type = Element_Type.dark;
        ud.atk_type = Attack_Type.normal;
        ud.attack = 50f;
        ud.atkDelay = 1f;
        ud.mana_type = Mana_Type.attack;
        ud.maxMana = 3f;
        ud.curMana = 0f;
        ud.isBuy = false;
    }

    private void Awake()
    {
        Init();
    }

    protected override void Attack()
    {
        StartCoroutine(C_Attack(ud.atk_type, Damage_Type.physic, Debuff_Type.slow, 2f));
    }
    public void ESkill_Start()
    {
        isSkill = true;
        transform.LookAt(target.transform);
        ud.curMana = 0;
        canManaRestore = false;
    }

    public override IEnumerator Skill()
    {
        anim.SetTrigger("skill");
        yield break;
    }
}
