using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BattleInfo : MonoBehaviour
{
    private List<BattleUnit> listOfAllies = new List<BattleUnit>();
    private List<BattleUnit> listOfEnemies = new List<BattleUnit>();
    private List<BattleUnit> turnOrder = new List<BattleUnit>();

    public List<BattleUnit> ListOfAllies => listOfAllies;
    public List<BattleUnit> ListOfEnemies => listOfEnemies;
    public List<BattleUnit> TurnOrder => listOfAllies.Concat(listOfEnemies).OrderByDescending(x => x.actionValue).ToList();
}
