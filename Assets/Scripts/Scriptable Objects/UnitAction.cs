using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BattleMove", menuName = "Create new battle move")]
public class UnitAction : ScriptableObject
{
    public string name;
    public string description;
    public string id;
    public EffectType effectType;
    public ElementType elementType;
    public TargetTypes targetTypes;
    public ValueType valueType;
    public MenuType menuType;

    public int baseValue;


    public enum MenuType
    {
        FIGHT,
        OTHER
    }

    /// <summary>
    /// Type of Damage for weakness checks 
    /// </summary>
    public enum EffectType
    {
        DAMAGE,
        HEAL,
        SPLASH,
        CUSTOM
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
        RANDOMALLY,
        RANDOMOPPONENT,
        ALLALLIES,
        ALLENEMIES,
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

    public CustomBattleAction customAction;
    public string animationName;
    public Vector3 offset;
}
