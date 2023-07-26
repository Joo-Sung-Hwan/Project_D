using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Undead_Archer : Monster
{
    public override void Init()
    {
        base.Init();
        md.index = Monsters_Index.archer;
        md.maxHP = 50;
        md.curHP = md.maxHP;
        md.speed = 1;
        md.speed_origin = md.speed;
        md.armor = 5;
    }

    void Start()
    {
        Init();
    }
}
