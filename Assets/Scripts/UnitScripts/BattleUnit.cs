using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleUnit : MonoBehaviour
{
    public UnitCreator unit;

    #region Stats
    private string unitName;
    private string description;
    private int id;

    public int currentHealth;
    private int maxHealth;

    public int currentMana;
    private int maxMana;

    public int currentLust;
    private int maxLust;

    private List<StatusEffects> statuseffects;
    #endregion

    private void Start()
    {
        Init();
    }

    void Init()
    {
        id = unit.id;
        unitName = unit.unitName;
        description = unit.description;
        maxHealth = unit.maxHealth;
        maxMana = unit.maxMana;
        maxLust = unit.maxLust;

        currentHealth = maxHealth;
        currentMana = maxMana;
        currentLust = maxLust;
    }

    public void ApplyEffect(UnitAction unitAction)
    {
        switch (unitAction.effectType)
        {
            case UnitAction.EffectType.DAMAGE:
                currentHealth -= unitAction.value;
                if (currentHealth <= 0)
                {
                    Debug.Log($"Oh no {unitName} died");
                    //Do death animation then disappear
                    //Remove unit from game unless player.
                }
                else
                {
                    //Do Hit animation
                }
                break;
            case UnitAction.EffectType.HEAL:
                currentHealth += unitAction.value;
                break;
            case UnitAction.EffectType.SPLASH:
                //Unit takes damage then transfers to other allies
                break;
            default:
                break;
        }
        
    }
    
    public void SetInteractable(bool activate)
    {
        GetComponent<Button>().interactable = activate;
    }
}
