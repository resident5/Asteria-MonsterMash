using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CACapture : CustomBattleAction
{
    public float baseChance = 100f;
    //Select a number between 1 and 100. Expected Chance
    //Select a number between 1 and 25. Actual Chance. This is the players actual capture chance and needs to be higher than the Expected
    public override void Effect(BattleUnit attacker, BattleUnit unit, UnitActionSO currentAction)
    {
        PlayerUnit playerUnit = attacker.GetComponent<PlayerUnit>();
        bool success = Random.Range(0, 100) <= baseChance;
        Debug.Log("Capture");

        if (success)
        {
            MonsterData capturedUnit = null;
            if (unit.myData is EnemyData enemyData)
            {
                capturedUnit = new MonsterData();
                capturedUnit.TurnEnemyIntoMonster(enemyData);
            }
            
            BattleManager.Instance.playerData.battleMons.Add(capturedUnit);
            unit.isDead = true;
        }
        else
        {
            Debug.Log("Failed to capture");
            //Print to the UI that the player failed
        }
    }
}
