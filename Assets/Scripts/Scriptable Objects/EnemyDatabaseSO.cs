using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Terre Tools/Database/Create enemy database", order = 30)]
public class EnemyDatabaseSO : ScriptableObject
{
    //List to hold all the possible enemy units in game\
    //TODO: Assign the shrubs to read from this list instead setting in in scene
    public List<UnitCreatorSO> listOfEnemyUnits;
}
