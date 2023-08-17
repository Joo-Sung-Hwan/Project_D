using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameThrower : ParticleController
{
    public override void Init()
    {
        base.Init();
        pd.debuff_type = Debuff_Type.none;
        pd.damage_type = Damage_Type.magic;
        pd.element_type = Element_Type.fire;
        pd.atk_type = Attack_Type.splash;
        pd.damage = 0.5f;
        pd.element_const = 0.8f;
        pd.debufftime = 0.1f;
    }

    void Start()
    {
        Init();
    }

}
