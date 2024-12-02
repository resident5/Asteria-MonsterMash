using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Battle/StatusEffects/Burn", fileName = "Burn Status")]
public class Burn : StatusEffectSO
{
    public int damage;

    public override void OnHit(int burnDamage)
    {
        stack += 1;
        stack = Mathf.Clamp(stack, 0, 4);
        Debug.Log("Unit has been burned!!");
    }

    public override void OnTurnEnd()
    {
        activeDuration -= 1;
    }

    public override void OnTurnStart()
    {
        int totalDamage = damage * stack;
        unit.TakeDamage(totalDamage);
    }

    public override void OnTurnUpdate()
    {
    }
}
