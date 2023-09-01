using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piranha : Unit
{
    public override void Init()
    {
        base.Init();
        ud.element_type = Element_Type.wind;
        ud.atk_type = Attack_Type.normal;
        ud.attack = 30f;
        ud.atkDelay = 1f;
        ud.mana_type = Mana_Type.attack;
        ud.maxMana = 10f;
        ud.curMana = 0f;
        ud.isBuy = false;
    }
    private void Awake()
    {
        Init();
    }
    protected override void Attack()
    {
        StartCoroutine(C_Attack(ud.atk_type, Damage_Type.physic));
    }

    //스킬 애니메이션 시작할 때 호출
    public void ESkill_Start()
    {
        float preAtk = ud.attack;
        ud.attack *= 2f;
        isSkill = true;
        transform.LookAt(target.transform);
        StartCoroutine(C_Attack(Attack_Type.normal, Damage_Type.trueType));
        ud.attack = preAtk;
        ud.curMana = 0;
        canManaRestore = false;
    }

    public override IEnumerator Skill()
    {
        anim.SetTrigger("skill");
        yield break;
    }
}
