using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Monsters_Index
{
    pirate_warrior,
    orc_shaman,
    orc_heavy,
    orc_grunt,
    orc_lord,
    snowman,
    demon,
    imp,
    ghost,
    ice_elementalboss
}

public enum Element_Type
{
    water,
    wind,
    earth,
    fire,
    light,
    dark
}

public enum Attack_Type
{
    normal,
    splash
}

public enum Damage_Type
{
    physic,
    magic,
    trueType
}

public enum Debuff_Type
{
    none,
    slow,
    stun,
    burn
}

public enum Mana_Type
{
    auto,
    attack
}

public enum Position_Index
{
    left,
    down,
    right,
    up,
    other
}

public enum Effect_Type
{
    ball,
    thrower,
    spawn,
    strike,
}