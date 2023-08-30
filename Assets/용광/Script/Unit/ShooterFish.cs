using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterFish : Unit
{
    public override void Init()
    {
        base.Init();
        ud.element_type = Element_Type.water;
        ud.atk_type = Attack_Type.normal;
        ud.attack = 50f;
        ud.atkDelay = 1f;
        ud.mana_type = Mana_Type.attack;
        ud.maxMana = 5f;
        ud.curMana = 0f;
        ud.isBuy = false;
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
        ud.curMana = 0;
        canManaRestore = false;
        anim.SetBool("skill", true);
        float preAttack = ud.attack;
        float preDelay = ud.atkDelay;
        ud.attack *= 1.35f;
        ud.atkDelay *= 1.2f;
        ud.atk_type = Attack_Type.splash;
        float skillTime = 0;
        while (skillTime <= 5)
        {
            skillTime += Time.deltaTime;
            mpBar.mpbar.fillAmount = (5 - skillTime) / 5f;
            yield return new WaitForEndOfFrame();
        }
        canManaRestore = true;
        anim.SetBool("skill", false);
        ud.attack = preAttack;
        ud.atkDelay = preDelay;
        ud.atk_type = Attack_Type.normal;
        SetMana(0);
    }
}
