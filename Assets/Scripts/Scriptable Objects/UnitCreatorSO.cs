using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Unit", menuName = "Terre Tools/Unit Info/New Unit", order = 1)]
public class UnitCreatorSO : ScriptableObject
{
    public Data data;

    public UnitCreatorSO Copy()
    {
        UnitCreatorSO unit = Instantiate(this);
        return unit;
    }
}
