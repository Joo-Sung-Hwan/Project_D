using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceWizzard : Unit
{
    public override void Init()
    {
        base.Init();
        ud.element_type = Element_Type.water;
        ud.atk_type = Attack_Type.normal;
        ud.attack = 30f;
        ud.atkDelay = 0.5f;
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
        StartCoroutine(C_Attack(ud.atk_type, Damage_Type.physic, Debuff_Type.slow, 2f));
    }

    public override IEnumerator Skill()
    {
        ud.curMana = 0;
        float skillTime = 3f;
        float preDelay = ud.atkDelay;
        canManaRestore = false;
        ud.atkDelay /= 2;
        while (skillTime > 0)
        {
            yield return new WaitForEndOfFrame();
            skillTime -= Time.deltaTime;
            mpBar.mpbar.fillAmount = skillTime / 3f;
        }
        ud.atkDelay = preDelay;
        canManaRestore = true;
    }
}
