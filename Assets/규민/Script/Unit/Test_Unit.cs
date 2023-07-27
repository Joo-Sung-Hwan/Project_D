using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_Unit : Unit
{
    protected override void Init()
    {
        ud.unit_type = Unit_Type.none;
        ud.atk_type = Attack_Type.normal;
        ud.attack = 30f;
        ud.atkDelay = 0.5f;
        ud.maxMana = 50f;
        ud.curMana = 0f;
    }   
        
    // Spublic float attack;tart is called before the first frame update
    void Start()
    {
        Init();
    }
}
