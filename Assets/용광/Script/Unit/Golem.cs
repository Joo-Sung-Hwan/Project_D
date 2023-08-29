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
        ud.atkDelay = 2.5f;
        ud.attack = 35f;
        ud.maxMana = 3f;
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
        anim.SetTrigger("skill");
        yield break;
    }
}
