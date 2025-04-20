using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class MonsterData : UnitData
{
    [Header("Stats")]
    public MonsterStats monsterStats;

    public void TurnEnemyIntoMonster(EnemyData eData)
    {
        monsterStats = new MonsterStats();
        monsterStats.unitName = eData.enemyStats.unitName;
        monsterStats.description = eData.enemyStats.description;
        monsterStats.battleStats = eData.enemyStats.battleStats;
        monsterStats.cannotBeCaptured = eData.enemyStats.cannotBeCaptured;
        monsterStats.spriteImage = eData.enemyStats.spriteImage;
        monsterStats.animatorController = eData.enemyStats.animatorController;
        monsterStats.battleMoves = eData.enemyStats.battleMoves;
        monsterStats.evolutions = eData.enemyStats.evolutions;

        currentXP = 0;
        requiredXP = CalculateRequiredXP();

    }

    public override void TakeDamage(int damage)
    {

    }

    public override void GainFlatExperience(int exp)
    {
        currentXP += exp;
    }

    public override void GainExperience(int exp)
    {
        currentXP += exp;

        if (currentXP >= requiredXP)
        {
            LevelUp();
            EventManager.Instance.monsterEvents.MonsterLevelChanged(monsterStats.battleStats.level);
        }

        EventManager.Instance.monsterEvents.MonsterExperienceChanged(currentXP);
    }

    public override void LevelUp()
    {
        monsterStats.battleStats.LevelUp();

        CheckEvolutionStatus();

        currentXP = Mathf.RoundToInt(currentXP - requiredXP);
        requiredXP = CalculateRequiredXP();
    }

    public void CheckEvolutionStatus()
    {
        if (!monsterStats.HasEvolutions)
            return;

        List<UnitCreatorSO> possibleEvolutions = new List<UnitCreatorSO>();
        foreach (var evolution in monsterStats.evolutions)
        {

            bool canEvolve = true;
            foreach (var cond in evolution.conditions)
            {
                if (!cond.CheckCondition(this))
                {
                    canEvolve = false;
                    break;
                }
            }

            if(canEvolve)
            {
                possibleEvolutions.Add(evolution.evolvedForm);
                Debug.Log($"Start Evolving into {evolution.evolvedForm.name}");
            }
        }

        if(possibleEvolutions.Count > 0)
        {
            //Choose an evolution (Maybe a choose evo screen)
            //Evolve unit into chosen

            //Test Evolution
            UnitCreatorSO evolution = possibleEvolutions[0];
            monsterStats = new MonsterStats(evolution);
        }
    }

    private int CalculateRequiredXP()
    {
        int solverXp = 0;
        int level = monsterStats.battleStats.level;

        for (int i = 0; i < level; i++)
        {
            solverXp += (int)Mathf.Floor(level + additonMultiplier * Mathf.Pow(powerMultiplier, level / divisionMultiplier));
        }

        return solverXp / 4;
    }
}
