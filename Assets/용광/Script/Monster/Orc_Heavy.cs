using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orc_Heavy : Monster
{
    public override void Init()
    {
        base.Init();
        md.index = Monsters_Index.orc_heavy;
        md.maxHP = 250;
        md.curHP = md.maxHP;
        md.speed = 1.5f;
        md.speed_origin = md.speed;
        md.armor = 20;
    }

    void Start()
    {
        Init();
    }
}
