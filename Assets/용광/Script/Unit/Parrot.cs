using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parrot : Unit
{
    public override void Init()
    {
        base.Init();
        ud.element_type = Element_Type.fire;
        ud.atk_type = Attack_Type.normal;
        ud.attack = 30f;
        ud.atkDelay = 0.5f;
        ud.mana_type = Mana_Type.auto;
        ud.maxMana = 2f;
        ud.curMana = 0f;
        ud.isBuy = false;
    }
    void Start()
    {
        Init();
    }

    protected override void Attack()
    {
        //StartCoroutine(C_Attack(ud.atk_type, Damage_Type.physic, Debuff_Type.slow, 2f));
    }

    //스킬 애니메이션 시작할 때 호출
    public void ESkill_Start()
    {
        isSkill = true;
        transform.LookAt(target.transform);
        particle.gameObject.SetActive(true);
        particle.EffStart(1, 1, null);
        ud.curMana = 0;
        canManaRestore = false;
    }

    public override IEnumerator Skill()
    {
        particle.gameObject.SetActive(true);
        particle.EffStart(1, 1, null);
        SetMana(0);
        canSkill = true;
        yield break;
    }
}
