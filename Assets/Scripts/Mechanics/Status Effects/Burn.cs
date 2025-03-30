using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Terre Tools/Unit Info/New Status Effect/Burn", order = 3)]
public class Burn : StatusEffectSO
{
    public int damage;

    public override void OnHit()
    {
        stack += 1;
        stack = Mathf.Clamp(stack, 0, 4);
        Debug.Log($"Unit has been burned by {sourceAction} from {sourceUnit.name}");
    }

    public override void OnTurnEnd()
    {
        activeDuration -= 1;
    }

    public override void OnTurnStart()
    {
        int totalDamage = damage * stack;
        Debug.Log($"{unit.name} just took burn damage");
        unit.TakeDamage(totalDamage, sourceAction);
    }

    public override void OnTurnUpdate()
    {
    }
}
