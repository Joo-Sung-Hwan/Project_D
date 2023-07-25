using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_Unit : Unit
{
    protected override void Init()
    {
        ud.atk_type = Attack_Type.stun;
        ud.attack = 30;
        ud.atkDelay = 0.5f;
    }   
        
    // Spublic float attack;tart is called before the first frame update
    void Start()
    {
        Init();
    }
}
