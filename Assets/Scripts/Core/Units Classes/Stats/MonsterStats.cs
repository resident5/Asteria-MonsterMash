using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class MonsterStats : UnitStats
{
    public bool HasEvolutions => evolutions.Length > 0;


    public MonsterStats(UnitCreatorSO unitBaseData) : base(unitBaseData)
    {
    }

    public MonsterStats(string unitName, string description, BattleStats stats, bool cannotBeCaptured,
        Sprite spriteImage, RuntimeAnimatorController animatorController, List<UnitActionSO> battleMoves,
        Evolution[] evolutions)
        : base(unitName, description, stats, cannotBeCaptured, spriteImage, animatorController, battleMoves, evolutions)
    {

    }

    public MonsterStats() : base()
    {
    }
}
