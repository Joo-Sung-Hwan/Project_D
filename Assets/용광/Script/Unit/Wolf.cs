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
        ud.maxMana = 2f;
        ud.curMana = 0f;
    }

    bool isSkillActive = false;

    void Start()
    {
        Init();
    }

    protected override void Attack()
    {
        if (isSkillActive)
        {
            StartCoroutine(C_Attack(ud.atk_type, Damage_Type.physic , Debuff_Type.burn , 3f));
            isSkillActive = false;
            SetMana(0);
            canManaRestore = true;
        }
        else
            StartCoroutine(C_Attack(ud.atk_type, Damage_Type.physic));
    }

    //스킬 애니메이션 시작할 때 호출
    public void ESkill_Start()
    {
        transform.LookAt(target.transform);
        particle_Prf.gameObject.SetActive(true);
        particle_Prf.EffStart(1, 1, null);
        ud.curMana = 0;
        canManaRestore = false;
    }

    public override IEnumerator Skill()
    {
        isSkillActive = true;
        ud.curMana = 0;
        canManaRestore = false;
        yield break;
    }
}
