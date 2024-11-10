using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CustomBattleAction : MonoBehaviour
{
    public UnitActionSO actionData;

    public abstract void Effect(BattleUnit attacker, BattleUnit unit, UnitActionSO action);
}
