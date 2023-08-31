using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orc_Heavy : Monster
{
    public override void Init()
    {
        base.Init();
        md.index = Monsters_Index.orc_heavy;
        md.maxHP = 400;
        md.curHP = md.maxHP;
        md.speed = 1f;
        md.speed_origin = md.speed;
        md.armor = 25;
    }

    void Start()
    {
        Init();
    }
}
