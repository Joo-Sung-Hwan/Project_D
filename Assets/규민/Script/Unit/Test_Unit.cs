using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_Unit : Unit
{
    protected override void Init()
    {
        base.Init();
        ud.unit_type = Element_Type.none;
        ud.atk_type = Attack_Type.normal;
        ud.attack = 30f;
        ud.atkDelay = 0.5f;
        ud.mana_type = Mana_Type.attack;
        ud.maxMana = 10f;
        ud.curMana = 0f;
    }   
        
    // Spublic float attack;tart is called before the first frame update
    void Start()
    {
        Init();
    }
}
