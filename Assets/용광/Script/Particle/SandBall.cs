using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandBall : ParticleController
{
    public override void Init()
    {
        base.Init();
        pd.debuff_type = Debuff_Type.stun;
        pd.damage_type = Damage_Type.magic;
        pd.element_type = Element_Type.earth;
        pd.atk_type = Attack_Type.splash;
        pd.eft_type = Effect_Type.ball;
        pd.atkdelay = 0.3f;
        pd.damage = 50;
        pd.element_const = 1f;
        pd.debufftime = 0.5f;
    }

    void Start()
    {
        Init();
    }
}
