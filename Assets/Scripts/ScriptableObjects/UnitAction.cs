using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BattleMove", menuName = "Create New Battle Move")]
public class UnitAction : ScriptableObject
{
    public string name;
    public string description;
    public string id;
    public EffectType effectType;
    public ElementType elementType;
    public TargetTypes targetTypes;
    public ValueType valueType;

    public int value;

    /// <summary>
    /// Type of Damage for weakness checks 
    /// </summary>
    public enum EffectType
    {
        DAMAGE,
        HEAL,
        SPLASH
    }

    /// <summary>
    /// Type of element being affected for vulnerability
    /// </summary>
    public enum ElementType
    {
        FIRE,
        WATER,
        LIGHTNING,
        LUST,
        DARK,
        LIGHT
    }

    /// <summary>
    /// Type of Targets available to select
    /// </summary>
    public enum TargetTypes
    {
        ALLIES,
        ENEMIES,
        ENVIRONMENT,
        MULTIPLEALLIES,
        MULTIPLEENEMIES,
        ALL
    }

    /// <summary>
    /// Type of value that will be afflicted
    /// </summary>
    public enum ValueType
    {
        DAMAGE,
        DEBUFF,
        LUST,
        MANA
    }


    public string animationName;
    public Vector3 offset;
}