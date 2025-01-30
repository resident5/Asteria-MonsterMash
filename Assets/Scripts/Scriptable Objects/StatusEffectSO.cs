using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public abstract class StatusEffectSO : ScriptableObject
{
    public int activeDuration;
    public int maxDuration;
    public BattleUnit sourceUnit;
    public BattleUnit unit;
    public UnitActionSO sourceAction;

    public bool isStackable;
    public int stack = 0;

    public GameObject visualEffect;
    public enum StatusType
    {
        DOT,
        AMPLIFIER
    }

    public StatusType type;

    public UnitActionSO.ElementTypes elementType;

    public abstract void OnHit();
    public abstract void OnTurnStart();
    public abstract void OnTurnUpdate();
    public abstract void OnTurnEnd();
}
