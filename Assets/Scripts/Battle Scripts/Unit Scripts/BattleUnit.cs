using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.UI;

public class BattleUnit : MonoBehaviour, ITurn
{
    [SerializeField] private UnitCreatorScriptableObject unit;

    public const int MAX_ACTION_VALUE = 100;

    public BattleStats myStats;
    public Animator anim;

    private string unitName;
    private string description;
    private int id;

    public bool isDead = false;

    private List<IStatusEffects> statusEffects;
    public List<UnitAction> myBattleMoves;

    public bool isTurn;
    public int actionValue;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void Init()
    {
        UnitCreatorScriptableObject unitInstance = Instantiate(unit);
        id = unitInstance.id;
        unitName = unitInstance.unitName;
        description = unitInstance.description;
        myStats = unitInstance.stats;
        myBattleMoves = unitInstance.battleMoves;
        statusEffects = new List<IStatusEffects>();
        //Actionvalue is calculated as 100/speed then subracted until it reaches 0
        actionValue = Mathf.CeilToInt(Mathf.Clamp(MAX_ACTION_VALUE / myStats.Speed, 0, MAX_ACTION_VALUE));
        Debug.Log($"{name}'s action value is {actionValue}");
    }

    private void Update()
    {
        if (isTurn)
        {
            OnTurnUpdate();
        }
    }

    public void ApplyEffect(BattleUnit attacker, UnitAction action)
    {
        switch (action.effectType)
        {
            case UnitAction.EffectType.DAMAGE:
                myStats.Health -= action.baseValue;

                if (myStats.Health <= 0)
                {
                    PlayAnimation("Death");
                    isDead = true;
                }
                else
                {
                    Debug.Log("HIT");
                    PlayAnimation("Hit");
                }
                break;
            case UnitAction.EffectType.HEAL:
                myStats.Health += action.baseValue;
                break;
            case UnitAction.EffectType.SPLASH:
                //Unit takes damage then transfers to other allies
                break;
            case UnitAction.EffectType.CUSTOM:
                action.customAction.Effect(attacker, this, action);
                break;
            default:
                break;
        }

    }

    public void SetInteractable(bool activate)
    {
        GetComponent<Button>().interactable = activate;
    }

    public void PlayAnimation(string animName)
    {
        anim.Play(animName);
        anim.SetBool("IsInteracting", true);
    }

    public virtual void OnTurnStart()
    {
        
        foreach (var item in statusEffects)
        {
            if (item != null)
                item.OnTurnStart();
        }

    }

    public virtual void OnTurnUpdate()
    {
        foreach (var item in statusEffects)
        {
            if (item != null)
                item.OnTurnUpdate();
        }
    }

    public virtual void OnTurnEnd()
    {
        actionValue = Mathf.CeilToInt(Mathf.Clamp(MAX_ACTION_VALUE / myStats.Speed, 0, MAX_ACTION_VALUE));

        foreach (var item in statusEffects)
        {
            if (item != null)
                item.OnTurnEnd();
        }
    }

    public void DecreaseActionValue(int value)
    {
        actionValue -= value;
    }
}
