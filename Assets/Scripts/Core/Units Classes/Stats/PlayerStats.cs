using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;

[System.Serializable]
public class PlayerStats : UnitStats
{
    public PlayerStats(string unitName, string description, BattleStats stats, bool cannotBeCaptured, 
        Sprite spriteImage, RuntimeAnimatorController animatorController, 
        List<UnitActionSO> battleMoves): base(null)
    {
        this.unitName = unitName;
        this.description = description;
        this.battleStats = stats;
        this.cannotBeCaptured = cannotBeCaptured;
        this.spriteImage = spriteImage;
        this.animatorController = animatorController;
        this.battleMoves = battleMoves;

        battleStats.InitStats();
    }

    public PlayerStats(UnitCreatorSO unitBaseData) : base(unitBaseData)
    {
        UnitCreatorSO baseStats = unitBaseData.Copy();
        unitName = baseStats.data.unitName;
        description = baseStats.data.description;
        battleStats = baseStats.data.stats;
        cannotBeCaptured = baseStats.data.cannotBeCaptured;
        spriteImage = baseStats.data.spriteImage;
        animatorController = baseStats.data.animatorController;
        battleMoves = baseStats.data.battleMoves;

        battleStats.InitStats();
    }


}
