using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceLanceball : ParticleController
{
    public override void Init()
    {
        base.Init();
        pd.debuff_type = Debuff_Type.slow;
        pd.damage_type = Damage_Type.magic;
        pd.element_type = Element_Type.water;
        pd.atk_type = Attack_Type.normal;
        pd.damage = 20;
        pd.element_const = 0.8f;
        pd.debufftime = 0.25f;
    }

    void Start()
    {
        Init();
    }

}
