using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy Database", menuName = "Create Enemy Database")]
public class EnemyDatabase : ScriptableObject
{
    public List<UnitCreator> listOfEnemyUnits;
}
