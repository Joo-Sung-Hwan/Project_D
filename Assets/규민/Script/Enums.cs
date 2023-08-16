using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Monsters_Index
{
    ghost,
    wolf,
    snowman,
    snake,
    skeleton_king,
    shooterFish,
    seaTitan,
    pirate_Warrior,
    piranha,
    parrot,
    orc_heavy,
    orc_shaman,
    orc_lord,
    orc_grunt,
    naturewizzard,
    lightningwizzard,
    knight,
    king,
    icewizzard,
    ice_minion,
    ice_elementalboss,
    golem,
    firewizzard,
    dragon,
    demon,
    imp,
    guardin,
}

public enum Element_Type
{
    none,
    water,
    wind,
    earth,
    fire
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