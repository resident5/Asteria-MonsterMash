using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Unit", menuName = "Battle/Create new unit")]
public class UnitCreatorSO : ScriptableObject
{
    public Data data;

    public UnitCreatorSO Copy()
    {
        UnitCreatorSO unit = Instantiate(this);
        return unit;
    }
}
