using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : Monster
{

    public override void Init()
    {
        base.Init();
        md.index = Monsters_Index.ghost;
        md.element_Type = Element_Type.wind;
        md.maxHP = 900;
        md.curHP = md.maxHP;
        md.speed = 1;
        md.speed_origin = md.speed;
        md.armor = 50;
    }

    void Start()
    {
        Init();
    }
}
