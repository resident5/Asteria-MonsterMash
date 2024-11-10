using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyDatabase", menuName = "Database/Create enemy database")]
public class EnemyDatabaseSO : ScriptableObject
{
    public List<UnitCreatorSO> listOfEnemyUnits;
}
