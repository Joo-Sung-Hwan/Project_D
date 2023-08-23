using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flakes : ParticleController
{
    public override void Init()
    {
        base.Init();
        pd.debuff_type = Debuff_Type.slow;
        pd.damage_type = Damage_Type.magic;
        pd.element_type = Element_Type.dark;
        pd.atk_type = Attack_Type.splash;
        pd.eft_type = Effect_Type.spawn;
        pd.atkdelay = 0.3f;
        pd.damage = 3f;
        pd.element_const = 0.9f;
        pd.debufftime = 0.5f;
    }

    void Start()
    {
        Init();
    }
}
