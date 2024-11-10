using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Unit", menuName = "Battle/Create new unit")]
public class UnitCreatorSO : ScriptableObject
{
    public int id;

    public int level;
    public string unitName;
    public string description;

    public BattleStats stats;

    //public int maxHealth;
    //public int maxMana;
    //public int maxLust;
    public bool isCapturable;

    public bool isEnemy;

    public Sprite spriteImage;
    public Animator animator;
    public List<UnitActionSO> battleMoves;

}
