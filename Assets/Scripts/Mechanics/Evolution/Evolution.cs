using UnityEngine;
using System.Collections;

[System.Serializable]
public class Evolution
{
    public string name;
    public UnitCreatorSO evolvedForm;
    public EvolutionCondition[] conditions;
}
