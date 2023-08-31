using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowMan : Monster
{
    public override void Init()
    {
        base.Init();
        md.index = Monsters_Index.snowman;
        md.maxHP = 800;
        md.curHP = md.maxHP;
        md.speed = 1;
        md.speed_origin = md.speed;
        md.armor = 40;
    }

    void Start()
    {
        Init();
    }
}
