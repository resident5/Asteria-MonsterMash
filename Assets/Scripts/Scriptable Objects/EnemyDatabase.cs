using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy Database", menuName = "Create enemy database")]
public class EnemyDatabase : ScriptableObject
{
    public List<UnitCreatorScriptableObject> listOfEnemyUnits;
}
