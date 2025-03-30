using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Terre Tools/Evolution Conditions/Level Condition", order = 20)]
public class LevelEvolutionCondition : EvolutionCondition
{
    public int requiredLevel;

    public override bool CheckCondition(MonsterData monster)
    {
        return monster.monsterStats.battleStats.level >= requiredLevel;
    }
}
