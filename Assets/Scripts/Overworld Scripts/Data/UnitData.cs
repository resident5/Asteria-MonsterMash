using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class UnitData
{
    public Emote emote;

    [Header("Experience System")]
    [Range(1f, 100f)]
    protected float additonMultiplier = 300;

    [Range(2f, 4f)]
    protected float powerMultiplier = 2f;

    [Range(7f, 14f)]
    protected float divisionMultiplier = 7f;

    //public UnitStats unitStats;

    [Header("Level System")]
    public int currentXP;
    public int requiredXP;

    public abstract void TakeDamage(int damage);
    public abstract void GainFlatExperience(int exp);
    public abstract void GainExperience(int exp);
    public abstract void LevelUp();

    public void SetupEmote(Transform worldCanvasTransform)
    {
        emote.transform.SetParent(worldCanvasTransform);
    }
}
