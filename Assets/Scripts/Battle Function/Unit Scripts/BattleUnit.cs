using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

// TODO: Implement stats parameters from the attacker to modify values of the attack ie a swift attack does more damage based on speed 

public class BattleUnit : MonoBehaviour
{
    public UnitCreatorSO unit;
    public BattleManager manager;

    public const int MAX_ACTION_VALUE = 100;

    public Data data;
    public Animator animator;

    //public string unitName;
    //public string description;
    //public int id;

    public List<StatusEffectSO> statusEffects;
    //public List<UnitActionSO> myBattleMoves;

    public bool isTurn;
    public int baseActionValue = 100;
    public int currentActionValue = 100;

    public bool isDead = false;
    public bool isLusty = false;

    public AudioClip audioClip;

    public HealthBar healthbar;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    //public void Init()
    //{
    //    UnitCreatorSO unitInstance = Instantiate(unit);
    //    id = unitInstance.id;
    //    unitName = unitInstance.unitName;
    //    description = unitInstance.description;
    //    data.stats = unitInstance.stats;
    //    myBattleMoves = unitInstance.battleMoves;
    //    statusEffects = new List<StatusEffectSO>();
    //    spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    //    healthbar = GetComponentInChildren<HealthBar>();

    //    if (spriteRenderer.sprite == null && unitInstance.spriteImage != null)
    //    {
    //        spriteRenderer.sprite = unitInstance.spriteImage;
    //    }

    //    //Load the animator controller from Resources.Load() based on the name of the mon
    //    //animator = unitInstance.animator;
    //    //Actionvalue is calculated as 100/speed then subracted until it reaches 0

    //    healthbar.unit = this;
    //    baseActionValue = Mathf.CeilToInt(Mathf.Clamp(MAX_ACTION_VALUE / data.stats.Speed, 0, MAX_ACTION_VALUE));
    //    currentActionValue = baseActionValue;
    //    //Debug.Log($"{name}'s action value is {actionValue}");
    //}

    public void Init(UnitCreatorSO uCO, BattleStats stats = null)
    {
        UnitCreatorSO unitInstance = Instantiate(uCO);
        unit = unitInstance;
        data = unitInstance.data;
        statusEffects = new List<StatusEffectSO>();
        healthbar = GetComponentInChildren<HealthBar>();

        //Load the animator controller from Resources.Load() based on the name of the mon
        animator.runtimeAnimatorController = unitInstance.data.animatorController;
        //Actionvalue is calculated as 100/speed then subracted until it reaches 0

        healthbar.unit = this;
        baseActionValue = Mathf.CeilToInt(Mathf.Clamp(MAX_ACTION_VALUE / data.stats.Speed, 0, MAX_ACTION_VALUE));
        currentActionValue = baseActionValue;
    }


    private void Update()
    {
        if (isTurn)
        {
            OnTurnUpdate();
        }
    }

    /// <summary>
    /// Apply the affected action to the current battle unit. <attacker> for getting stats. <action> for what action is doing damage
    /// </summary>
    /// <param name="attacker"></param>
    /// <param name="action"></param>
    public void ApplyAction(BattleUnit attacker, UnitActionSO action)
    {
        Debug.Log($"Do {action.name}");

        //Debug.Log($"{unitName} has {data.stats.Health}\n{attacker.unitName} casts {action.name} on {unit.unitName} for {action.baseValue} points");
        switch (action.effectType)
        {
            case UnitActionSO.EffectTypes.DAMAGE:
                //Put a method for calculating if attack landed or it missed
                TakeDamage(action.baseValue, action);
                ApplyDebuff(attacker, action);
                break;
            case UnitActionSO.EffectTypes.HEAL:
                data.stats.Health += action.baseValue;
                break;
            case UnitActionSO.EffectTypes.SPLASH:
                //Unit takes damage then transfers to other allies
                break;
            case UnitActionSO.EffectTypes.CUSTOM:
                action.customAction.Effect(attacker, this, action);
                break;
            default:
                break;
        }
        //Debug.Log($"{data.unitName} has {data.stats.Health} left");

    }

    public void ApplyDebuff(BattleUnit attackingUnit, UnitActionSO action)
    {
        //TODO: Add Status chance when applying a debuff
        foreach (var status in action.statusEffectTypes)
        {
            if (!statusEffects.Contains(status))
            {
                if (status != null)
                {
                    StatusEffectSO statusObj = Instantiate(status);
                    statusObj.activeDuration = statusObj.maxDuration;
                    statusObj.sourceAction = action;
                    statusObj.sourceUnit = attackingUnit;
                    statusObj.unit = this;
                    statusObj.OnHit();
                    if (GameManager.Instance.debugger.isDebugging)
                        Debug.Log("Status unit name " + statusObj.unit.name);
                    statusEffects.Add(statusObj);
                }
                else
                {
                    Debug.LogWarning($"Status list has a status but its null. Found in the {action.name} action");
                }
            }
            else
            {
                var obj = action.statusEffectTypes.FirstOrDefault(x => x.name == status.name);
                if (obj.isStackable)
                {
                    obj.stack += 1;
                }
                //statusEffects[status].activeDuration = status.maxDuration;
            }

        }
    }

    //Modify totalDamage by typeadvantage, by amblifierType
    public void TakeDamage(int damage, UnitActionSO action = null)
    {
        int totalDamage = damage;
        
        //totalDamage += AmplifiedDamage(damage);
        //OnAttackHit(damage);
        if (action.elementType != UnitActionSO.ElementTypes.LUST)
        {
            data.stats.Health -= totalDamage;
            if (data.stats.Health <= 0)
            {
                PlayAnimation("Death");
                isDead = true;
            }
            else
            {
                Debug.Log("Trigger " + data.unitName + "_Hit");
                PlayAnimation(data.unitName + "_Hit");
            }
        }
        else
        {
            data.stats.Lust += totalDamage;
            //Should probably use MAX LUST instead
            if (data.stats.Lust >= 100)
            {
                //Play Loss animation
                isLusty = true;
            }
            else
            {
                //Player Lust Animation
            }
        }
    }

    public void SetInteractable(bool activate)
    {
        GetComponent<Button>().interactable = activate;
    }

    public void PlayAnimation(string animName)
    {
        animator.Play(animName);
        animator.SetBool("IsInteracting", true);
    }

    public virtual void OnTurnStart()
    {
        foreach (var status in statusEffects)
        {
            if (status != null)
                status.OnTurnStart();
        }

    }

    public virtual void OnTurnUpdate()
    {
        foreach (var status in statusEffects)
        {
            if (status != null)
                status.OnTurnUpdate();
        }
    }

    public virtual void OnTurnEnd()
    {
        if (GameManager.Instance.debugger.isDebugging)
            Debug.Log("My Turn Ended");
        currentActionValue = Mathf.CeilToInt(Mathf.Clamp(MAX_ACTION_VALUE / data.stats.Speed, 0, MAX_ACTION_VALUE));


        foreach (var status in statusEffects)
        {
            if (status != null)
                status.OnTurnEnd();
        }
    }

    public virtual void OnHit()
    {
        foreach (var status in statusEffects)
        {
            status.OnHit(); 
        }
    }

    public void DecreaseActionValue(int value)
    {
        currentActionValue -= value;
    }
}
