using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningBall : ParticleController
{
    public override void Init()
    {
        base.Init();
        pd.debuff_type = Debuff_Type.stun;
        pd.damage_type = Damage_Type.magic;
        pd.element_type = Element_Type.light;
        pd.atk_type = Attack_Type.splash;
        pd.eft_type = Effect_Type.ball;
        pd.atkdelay = 0.3f;
        pd.damage = 10;
        pd.element_const = 0.9f;
        pd.debufftime = 0.7f;
    }

    void Start()
    {
        Init();
    }
}
