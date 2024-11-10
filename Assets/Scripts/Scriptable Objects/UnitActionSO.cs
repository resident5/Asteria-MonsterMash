using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GDX.Collections.Generic;


[CreateAssetMenu(fileName = "BattleMove", menuName = "Battle/Create new battle move")]
public class UnitActionSO : ScriptableObject
{
    public string name;
    public string description;
    public string id;
    public EffectTypes effectType;
    public ElementTypes elementType;
    public TargetTypes targetTypes;
    public MenuType menuType;

    public int baseValue;

    public StatusEffectSO[] statusEffectTypes;
    
    public enum StatusEffectType { FIRE, ICE }

    public enum MenuType
    {
        FIGHT,
        OTHER,
        SUMMON
    }

    /// <summary>
    /// Type of Damage for weakness checks 
    /// </summary>
    public enum EffectTypes
    {
        DAMAGE,
        DEBUFF,
        BUFF,
        HEAL,
        SPLASH,
        CUSTOM
    }

    /// <summary>
    /// Type of element being affected for vulnerability
    /// </summary>
    public enum ElementTypes
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

    public CustomBattleAction customAction;
    public string animationName;
    public Vector3 offset;
}
