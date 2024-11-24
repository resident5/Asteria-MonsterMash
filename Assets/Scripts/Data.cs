using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Data
{
    public int id;

    public int level;
    public string unitName;
    public string description;

    public BattleStats stats;
    public bool isCapturable;

    public bool isEnemy;

    public Sprite spriteImage;
    public Animator animator;
    public List<UnitActionSO> battleMoves;

    public void ModifyHealth()
    {

    }
}