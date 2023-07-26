using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : Monster
{

    public override void Init()
    {
        base.Init();
        md.index = Monsters_Index.ghost;
        md.maxHP = 50;
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
