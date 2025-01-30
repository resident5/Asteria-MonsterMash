using JetBrains.Annotations;
using System.Collections;
using UnityEngine;

//Split it up into Curative Consumables
//Dispel Consummables
//Buff Consummables
[CreateAssetMenu(fileName = "New Consumable", menuName = "Inventory/Consumable")]
public class Consumable : Item
{
    public int flatValueModifier;

    [Range(1,100)]
    public int percentageValueModifier;

    public enum EffectType { ModHealth, ModMana, ModLust, ModHealthPercent, ModManaPercent, ModLustPercent }
    public EffectType effectType;

    public override void Use()
    {
        base.Use();
        ApplyEffect();
        RemoveFromInventory();
    }

    private void ApplyEffect()
    {
        //Change Data to have modifier methods instead of directly messing with the variables
        
        switch (effectType)
        {
            case EffectType.ModHealth:
                playerData.data.stats.Health += flatValueModifier;
                break;
            case EffectType.ModMana:
                playerData.data.stats.Mana += flatValueModifier;
                break;
            case EffectType.ModLust:
                playerData.data.stats.Lust += flatValueModifier;
                break;
            case EffectType.ModHealthPercent:
                float maxHp = playerData.data.stats.MaxHealth;
                float percentageValue = (percentageValueModifier / 100) * maxHp;
                playerData.data.stats.Health += percentageValue;
                break;
            case EffectType.ModManaPercent:
                float maxMp = playerData.data.stats.MaxHealth;
                float percentageMpValue = (percentageValueModifier / 100) * maxMp;
                playerData.data.stats.Mana += (int)percentageMpValue;
                break;
            case EffectType.ModLustPercent:
                float maxLust = playerData.data.stats.MaxHealth;
                float percentageLustValue = (percentageValueModifier / 100) * maxLust;
                playerData.data.stats.Lust += (int)percentageLustValue;
                break;
            default:
                break;
        }
    }


}
