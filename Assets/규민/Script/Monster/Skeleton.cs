using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : Monster
{
    public override void Init()
    {
        md.type = Monsters_Index.skeleton;
        md.maxHP = 50;
        md.curHP = md.maxHP;
        md.speed = 1;
    }

    void Start()
    {
        Init();
    }
}
