using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Burns : IStatusEffects
{
    public BattleUnit unit;
    public int stack = 0;
    public int damage;
    public int turnDuration;

    public void OnHit()
    {
        stack += 1;
        stack = Mathf.Clamp(stack, 0, 4);
    }

    public void OnTurnEnd()
    {
    }

    public void OnTurnStart()
    {
        unit.myStats.Health -= damage;
    }

    public void OnTurnUpdate()
    {
        turnDuration -= 1;
    }
}
