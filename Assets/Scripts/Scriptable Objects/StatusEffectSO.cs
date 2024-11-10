using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public abstract class StatusEffectSO : ScriptableObject
{
    public int activeDuration;
    public int maxDuration;
    public BattleUnit unit;

    public bool isStackable;
    public int stack = 0;

    public GameObject visualEffect;

    public abstract void OnHit();
    public abstract void OnTurnStart();
    public abstract void OnTurnUpdate();

    public abstract void OnTurnEnd();
}
