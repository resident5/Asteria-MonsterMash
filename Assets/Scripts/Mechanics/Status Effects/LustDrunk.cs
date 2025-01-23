using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO: Add lust drunk to effect the user's lust
public class LustDrunk : StatusEffectSO
{
    public int percentageIncrease;
    //If the player is gettin hit for 10 damage
    //While they have LustDrunk which is a 30% increase to all lust attacks they should take
    //An extra 3 damage

    public override void OnHit(int lustDamage)
    {
        int extraDamage = lustDamage * (percentageIncrease/100);
        unit.TakeDamage(extraDamage, sourceAction);
        Debug.Log("Unit is Lust Drunk!!");
    }

    public override void OnTurnEnd()
    {
        activeDuration -= 1;
    }

    public override void OnTurnStart()
    {
    }

    public override void OnTurnUpdate()
    {
    }
}
