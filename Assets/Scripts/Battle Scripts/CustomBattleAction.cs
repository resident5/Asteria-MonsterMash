using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CustomBattleAction : MonoBehaviour
{
    public UnitAction actionData;

    public abstract void Effect(BattleUnit attacker, BattleUnit unit, UnitAction action);
}
