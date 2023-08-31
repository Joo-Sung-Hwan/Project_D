using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceElementalBoss : Monster
{
    public override void Init()
    {
        base.Init();
        md.index = Monsters_Index.ice_elementalboss;
        md.maxHP = 5000;
        md.curHP = md.maxHP;
        md.speed = 0.7f;
        md.speed_origin = md.speed;
        md.armor = 45f;
    }

    void Start()
    {
        Init();
    }
}
