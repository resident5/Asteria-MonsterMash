using JetBrains.Annotations;
using System.Collections;
using UnityEngine;

//Split it up into Curative Consumables
//Dispel Consummables
//Buff Consummables
[CreateAssetMenu(fileName = "New Consumable", menuName = "Inventory/Consumable")]
public class ConsumableItem : Item
{
    public int flatValueModifier;

    [Range(1, 100)]
    public int percentageValueModifier;

    public enum EffectType { ModHealth, ModMana, ModLust, ModHealthPercent, ModManaPercent, ModLustPercent }
    public EffectType effectType;

    public override void Use(UnitStats target)
    {
        base.Use(target);

        if (target is PlayerStats targetStats)
        {
            ApplyEffect(targetStats);

        }
        else if (target is MonsterStats targetMonster)
        {
            ApplyEffect(targetMonster);
            RemoveFromInventory();

        }
        else if (target is EnemyStats targetEnemy)
        {
            ApplyEffect(targetEnemy);
            RemoveFromInventory();

        }
        else
        {
            Debug.LogError("Target is not a valid type for consumable item.");
            return;
        }

    }

    private void ApplyEffect(UnitStats target)
    {
        //Change Data to have modifier methods instead of directly messing with the variables

        switch (effectType)
        {
            case EffectType.ModHealth:
                target.battleStats.Health += flatValueModifier;
                break;
            case EffectType.ModMana:
                target.battleStats.Mana += flatValueModifier;
                break;
            case EffectType.ModLust:
                target.battleStats.Lust += flatValueModifier;
                break;
            case EffectType.ModHealthPercent:
                float maxHp = target.battleStats.MaxHealth;
                float percentageValue = (percentageValueModifier / 100) * maxHp;
                target.battleStats.Health += percentageValue;
                break;
            case EffectType.ModManaPercent:
                float maxMp = target.battleStats.MaxHealth;
                float percentageMpValue = (percentageValueModifier / 100) * maxMp;
                target.battleStats.Mana += (int)percentageMpValue;
                break;
            case EffectType.ModLustPercent:
                float maxLust = target.battleStats.MaxHealth;
                float percentageLustValue = (percentageValueModifier / 100) * maxLust;
                target.battleStats.Lust += (int)percentageLustValue;
                break;
            default:
                break;
        }

        RemoveFromInventory();

    }


}
