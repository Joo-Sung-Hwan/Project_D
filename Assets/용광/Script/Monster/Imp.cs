using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Imp : Monster
{
    public override void Init()
    {
        base.Init();
        md.index = Monsters_Index.imp;
        md.maxHP = 1200;
        md.curHP = md.maxHP;
        md.speed = 1;
        md.speed_origin = md.speed;
        md.armor = 25;
    }

    void Start()
    {
        Init();
    }
}
