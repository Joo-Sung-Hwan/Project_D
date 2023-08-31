using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Demon : Monster
{
    public override void Init()
    {
        base.Init();
        md.index = Monsters_Index.demon;
        md.maxHP = 1100;
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
