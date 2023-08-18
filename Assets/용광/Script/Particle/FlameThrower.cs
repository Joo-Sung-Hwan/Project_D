using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameThrower : ParticleController
{
    public override void Init()
    {
        base.Init();
        pd.debuff_type = Debuff_Type.burn;
        pd.damage_type = Damage_Type.magic;
        pd.element_type = Element_Type.fire;
        pd.atk_type = Attack_Type.splash;
        pd.eft_type = Effect_Type.thrower;
        pd.atkdelay = 0.2f;
        pd.damage = 2f;
        pd.element_const = 0.6f;
        pd.debufftime = 0.2f;
    }

    void Start()
    {
        Init();
    }

}
