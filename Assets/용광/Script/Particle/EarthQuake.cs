using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthQuake : ParticleController
{

    public override void Init()
    {
        base.Init();
        pd.debuff_type = Debuff_Type.stun;
        pd.damage_type = Damage_Type.physic;
        pd.element_type = Element_Type.earth;
        pd.atk_type = Attack_Type.splash;
        pd.eft_type = Effect_Type.strike;
        pd.atkdelay = 0.1f;
        pd.damage = 50;
        pd.element_const = 1f;
        pd.debufftime = 0.2f;
    }

    void Start()
    {
        Init();
    }

}
