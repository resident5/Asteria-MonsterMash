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
        //Assign target 
        ApplyEffect();
        RemoveFromInventory();
    }

    private void ApplyEffect()
    {
        //Change Data to have modifier methods instead of directly messing with the variables
        
        switch (effectType)
        {
            case EffectType.ModHealth:
                playerData.playerStats.battleStats.Health += flatValueModifier;
                break;
            case EffectType.ModMana:
                playerData.playerStats.battleStats.Mana += flatValueModifier;
                break;
            case EffectType.ModLust:
                playerData.playerStats.battleStats.Lust += flatValueModifier;
                break;
            case EffectType.ModHealthPercent:
                float maxHp = playerData.playerStats.battleStats.MaxHealth;
                float percentageValue = (percentageValueModifier / 100) * maxHp;
                playerData.playerStats.battleStats.Health += percentageValue;
                break;
            case EffectType.ModManaPercent:
                float maxMp = playerData.playerStats.battleStats.MaxHealth;
                float percentageMpValue = (percentageValueModifier / 100) * maxMp;
                playerData.playerStats.battleStats.Mana += (int)percentageMpValue;
                break;
            case EffectType.ModLustPercent:
                float maxLust = playerData.playerStats.battleStats.MaxHealth;
                float percentageLustValue = (percentageValueModifier / 100) * maxLust;
                playerData.playerStats.battleStats.Lust += (int)percentageLustValue;
                break;
            default:
                break;
        }
    }


}
