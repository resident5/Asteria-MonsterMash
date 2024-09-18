using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CACapture : CustomBattleAction
{

    //Select a number between 1 and 100. Expected Chance
    //Select a number between 1 and 25. Actual Chance. This is the players actual capture chance and needs to be higher than the Expected
    public override void Effect(BattleUnit attacker, BattleUnit unit, UnitAction currentAction)
    {
        PlayerUnit playerUnit = attacker.GetComponent<PlayerUnit>();
        float baseChance = 25f;

        int expectedChance = Random.Range(1, 101);
        float actualChance = Random.Range(1, baseChance + 1);

        bool success = actualChance >= expectedChance ? true : false;
        
        if(success)
        {
            BattleUnit capturedUnit = Instantiate(unit);
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
