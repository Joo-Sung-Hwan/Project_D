using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orc_Lord : Monster
{
    public override void Init()
    {
        base.Init();
        md.index = Monsters_Index.orc_lord;
        md.maxHP = 100;
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
