using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusEffects : MonoBehaviour
{
    public string statusName;
    public string statusDescription;

    public int value;

    public BattleUnit unit;

    //For this to work make multiple events to subscribe to
    //OnTurnBegan
    //OnTurnEnd
    //OnDamage

    public void Effect()
    {

    }
}
