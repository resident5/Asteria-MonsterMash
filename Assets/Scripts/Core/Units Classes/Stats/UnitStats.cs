using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UnitStats
{
    public string unitName;
    public string description;

    public bool cannotBeCaptured;

    public Sprite spriteImage;
    public RuntimeAnimatorController animatorController;

    public BattleStats battleStats;
    public List<UnitActionSO> battleMoves;

    public Evolution[] evolutions;

    public UnitStats(UnitCreatorSO unitBaseData)
    {
        UnitCreatorSO baseStats = unitBaseData.Copy();
        unitName = baseStats.data.unitName;
        description = baseStats.data.description;
        battleStats = baseStats.data.stats;
        cannotBeCaptured = baseStats.data.cannotBeCaptured;
        spriteImage = baseStats.data.spriteImage;
        animatorController = baseStats.data.animatorController;
        battleMoves = baseStats.data.battleMoves;
        evolutions = baseStats.data.evolutions;

        battleStats.InitStats();
    }

    public UnitStats(string unitName, string description,
        BattleStats stats, bool cannotBeCaptured, Sprite spriteImage, 
        RuntimeAnimatorController animatorController, List<UnitActionSO> battleMoves,
        Evolution[] evolutions)
    {
        this.unitName = unitName;
        this.description = description;
        this.battleStats = stats;
        this.cannotBeCaptured = cannotBeCaptured;
        this.spriteImage = spriteImage;
        this.animatorController = animatorController;
        this.battleMoves = battleMoves;
        this.evolutions = evolutions;

        battleStats.InitStats();
    }

    public UnitStats()
    {
        this.unitName = "";
        this.description = "";
        this.battleStats = new BattleStats();
        this.cannotBeCaptured = true;
        this.spriteImage = null;
        this.animatorController = null;
        this.battleMoves = new List<UnitActionSO>();
        this.evolutions = new Evolution[0];
    }
}
