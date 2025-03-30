using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerData : UnitData
{
    public List<MonsterData> battleMons = new List<MonsterData>();
    //public List<BattleUnit> battleMon;
    public GameManager gameManager => GameManager.Instance;

    public UnitCreatorSO playerCreator;

    public PlayerStats playerStats;

    public void InitializeStats()
    {
        playerStats = new PlayerStats(playerCreator);
        EventManager.Instance.playerEvents.PlayerLevelChanged(playerStats.battleStats.level);
        EventManager.Instance.playerEvents.PlayerExperienceChanged(currentXP);
        requiredXP = CalculateRequiredXP();
    }

    public override void TakeDamage(int damage)
    {
        playerStats.battleStats.Health -= damage;
    }

    public override void GainFlatExperience(int exp)
    {
        currentXP += exp;
    }

    public override void GainExperience(int experience)
    {
        currentXP += experience;

        if (currentXP >= requiredXP)
        {
            LevelUp();
            EventManager.Instance.playerEvents.PlayerLevelChanged(playerStats.battleStats.level);

            foreach (var monster in battleMons)
            {
                monster.GainExperience(experience);
            }
        }

        EventManager.Instance.playerEvents.PlayerExperienceChanged(currentXP);
    }

    public override void LevelUp()
    {
        playerStats.battleStats.LevelUp();

        currentXP = Mathf.RoundToInt(currentXP - requiredXP);
        requiredXP = CalculateRequiredXP();
    }

    private int CalculateRequiredXP()
    {
        int solverXp = 0;
        int level = playerStats.battleStats.level;

        for (int i = 0; i < level; i++)
        {
            solverXp += (int)Mathf.Floor(level + additonMultiplier * Mathf.Pow(powerMultiplier, level / divisionMultiplier));
        }

        return solverXp / 4;
    }


    //Keep track of the player's movements and health
}
