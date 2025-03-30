using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class EvolutionCondition : ScriptableObject
{
    public string name;
    public abstract bool CheckCondition(MonsterData monData);
}

//[CreateAssetMenu(menuName = "Evolution Conditions/Battle Condition")]
//public class ItemEvolutionCondition : EvolutionCondition
//{
//    public int requiredLevel;

//    public override bool CheckCondition(MonsterData monster)
//    {
//        return monster.monsterStats.battleStats.level >= requiredLevel;
//    }
//}
