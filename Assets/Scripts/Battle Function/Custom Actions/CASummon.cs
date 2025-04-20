using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CASummon : CustomBattleAction
{
    public GameObject emptyUnitPrefab;

    public override void Effect(BattleUnit attacker, BattleUnit unit, UnitActionSO action)
    {
        BattleManager manager = BattleManager.Instance;
        Transform pos = GetUnitList(manager.unitList.playerSpots);        

        //Get the Chosen Summon Unit
        //Add the summon's stats from the player's list to the summon

    }

    private Transform GetUnitList(Transform availablePositions)
    {
        Transform spot = null;
        foreach (Transform t in availablePositions)
        {
            if (t.childCount <= 0)
            {
                spot = t;
            }
        }

        return spot;
    }
}
