using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orc_Grunt : Monster
{
    public override void Init()
    {
        base.Init();
        md.index = Monsters_Index.orc_grunt;
        md.maxHP = 500;
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
