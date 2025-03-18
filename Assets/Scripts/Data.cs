using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Data
{
    public int id;

    public string unitName;
    public string description;

    public BattleStats stats;
    public bool cannotBeCaptured;

    public Sprite spriteImage;

    public RuntimeAnimatorController animatorController;
    public List<UnitActionSO> battleMoves;

    public Data Copy()
    {
        Data data = new Data();
        data.id = id;
        data.unitName = unitName;
        data.description = description;
        data.stats = stats;
        data.cannotBeCaptured = cannotBeCaptured;
        data.spriteImage = spriteImage;
        data.animatorController = animatorController;
        data.battleMoves = battleMoves;
        return data;
    }
}