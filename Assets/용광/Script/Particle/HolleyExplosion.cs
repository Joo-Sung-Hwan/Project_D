using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HolleyExplosion : ParticleController
{
    public override void Init()
    {
        base.Init();
        pd.debuff_type = Debuff_Type.none;
        pd.damage_type = Damage_Type.magic;
        pd.element_type = Element_Type.light;
        pd.atk_type = Attack_Type.splash;
        pd.eft_type = Effect_Type.strike;
        pd.atkdelay = 0.3f;
        pd.damage = 50f;
        pd.element_const = 1f;
        pd.debufftime = 1f;
    }

    void Start()
    {
        Init();
    }
}
