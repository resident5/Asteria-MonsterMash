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

    public SelectablePosition playerPositions;
    public SelectablePosition enemyPositions;

    public void Start()
    {
        //for (int i = 0; i < playerSpots.childCount; i++)
        //{
        //    if (playerSpots.GetChild(i).GetComponent<BattleUnit>())
        //    {
        //        playerList[i] = playerSpots.GetChild(i).GetComponent<BattleUnit>();
        //    }
        //}

        //for (int i = 0; i < enemySpots.childCount; i++)
        //{
        //    if (enemySpots.GetChild(i).GetComponent<BattleUnit>())
        //    {
        //        enemyList[i] = enemySpots.GetChild(i).GetComponent<BattleUnit>();
        //    }
        //}
    }

    public void ActivateAllUnits(bool activate)
    {
        foreach (var player in playerList)
        {
            if (player != null)
                player.SetInteractable(activate);
        }

        foreach (var enemy in enemyList)
        {
            if (enemy != null)
                enemy.SetInteractable(activate);
        }
    }
}
