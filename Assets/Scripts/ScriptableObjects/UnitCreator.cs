using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Unit", menuName = "Create new Unit")]
public class UnitCreator : ScriptableObject
{
    public int id;

    public int level;
    public string unitName;
    public string description;

    public int maxHealth;
    public int maxMana;
    public int maxLust;

    public bool isEnemy;

    public Sprite spriteImage;
    public List<UnitAction> battleMoves;

}
