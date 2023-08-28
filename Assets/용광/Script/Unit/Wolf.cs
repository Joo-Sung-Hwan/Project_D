using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wolf : Unit
{
    public override void Init()
    {
        base.Init();
        ud.element_type = Element_Type.wind;
        ud.atk_type = Attack_Type.normal;
        ud.mana_type = Mana_Type.attack;
        ud.atkDelay = 0.6f;
        ud.attack = 35f;
        ud.maxMana = 5f;
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
        transform.LookAt(target.transform);
        particle.gameObject.SetActive(true);
        particle.EffStart(1, 1, null);
        ud.curMana = 0;
        canAttack = false;
        canManaRestore = false;
    }

    public override IEnumerator Skill()
    {
        anim.SetBool("skill", true);
        yield break;
    }
}
