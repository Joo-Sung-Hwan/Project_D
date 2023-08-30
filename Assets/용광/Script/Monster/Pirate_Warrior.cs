using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pirate_Warrior : Monster
{
    public override void Init()
    {
        base.Init();
        md.index = Monsters_Index.pirate_warrior;
        md.maxHP = 150;
        md.curHP = md.maxHP;
        md.speed = 1;
        md.speed_origin = md.speed;
        md.armor = 20;
    }

    void Start()
    {
        Init();
    }
}
