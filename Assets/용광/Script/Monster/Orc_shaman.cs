using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orc_shaman : Monster
{
    public override void Init()
    {
        base.Init();
        md.index = Monsters_Index.orc_shaman;
        md.maxHP = 200;
        md.curHP = md.maxHP;
        md.speed = 1.3f;
        md.speed_origin = md.speed;
        md.armor = 20;
    }

    void Start()
    {
        Init();
    }
}
