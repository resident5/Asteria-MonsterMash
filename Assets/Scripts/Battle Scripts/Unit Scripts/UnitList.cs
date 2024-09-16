using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class UnitList : MonoBehaviour
{
    public BattleUnit[] playerList = new BattleUnit[3];
    public BattleUnit[] enemyList = new BattleUnit[5];

    public Transform playerSpots;
    public Transform enemySpots;

    public void Start()
    {
        for (int i = 0; i < playerSpots.childCount; i++)
        {
            if (playerSpots.GetChild(i).GetComponent<BattleUnit>())
            {
                playerList[i] = playerSpots.GetChild(i).GetComponent<BattleUnit>();
            }
        }

        for (int i = 0; i < enemySpots.childCount; i++)
        {
            if (enemySpots.GetChild(i).GetComponent<BattleUnit>())
            {
                enemyList[i] = enemySpots.GetChild(i).GetComponent<BattleUnit>();
            }
        }
    }

    public void ActivateAllUnits(bool activate)
    {
        foreach (var item in playerList)
        {
            if (item != null)
                item.SetInteractable(activate);
        }

        foreach (var item in enemyList)
        {
            if (item != null)
                item.SetInteractable(activate);
        }
    }
}
