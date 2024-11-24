using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

// TODO: Implement stats parameters from the attacker to modify values of the attack ie a swift attack does more damage based on speed 

public class BattleUnit : MonoBehaviour, ITurn
{
    public UnitCreatorSO unit;
    public BattleManager manager;

    public const int MAX_ACTION_VALUE = 100;

    public Data data;

    public BattleStats myStats;
    public Animator anim;

    public string unitName;
    public string description;
    public int id;

    public bool isDead = false;

    private List<StatusEffectSO> statusEffects;
    public List<UnitActionSO> myBattleMoves;

    public bool isTurn;
    public int baseActionValue = 100;
    public int currentActionValue = 100;

    public AudioClip audioClip;
    public Animator animator;
    SpriteRenderer spriteRenderer;

    HealthBar healthbar;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    //public void Init()
    //{
    //    UnitCreatorSO unitInstance = Instantiate(unit);
    //    id = unitInstance.id;
    //    unitName = unitInstance.unitName;
    //    description = unitInstance.description;
    //    myStats = unitInstance.stats;
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
    //    baseActionValue = Mathf.CeilToInt(Mathf.Clamp(MAX_ACTION_VALUE / myStats.Speed, 0, MAX_ACTION_VALUE));
    //    currentActionValue = baseActionValue;
    //    //Debug.Log($"{name}'s action value is {actionValue}");
    //}

    public void Init(UnitCreatorSO uCO, BattleStats stats = null)
    {
        UnitCreatorSO unitInstance = Instantiate(uCO);
        id = unitInstance.data.id;
        unitName = unitInstance.data.unitName;
        description = unitInstance.data.description;
        myStats = stats != null ? stats : unitInstance.data.stats;
        myBattleMoves = unitInstance.data.battleMoves;
        statusEffects = new List<StatusEffectSO>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        healthbar = GetComponentInChildren<HealthBar>();

        if (spriteRenderer.sprite == null && unitInstance.data.spriteImage != null)
        {
            spriteRenderer.sprite = unitInstance.data.spriteImage;
        }

        //Load the animator controller from Resources.Load() based on the name of the mon
        //animator = unitInstance.animator;
        //Actionvalue is calculated as 100/speed then subracted until it reaches 0

        healthbar.unit = this;
        baseActionValue = Mathf.CeilToInt(Mathf.Clamp(MAX_ACTION_VALUE / myStats.Speed, 0, MAX_ACTION_VALUE));
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
        //Debug.Log($"{unitName} has {myStats.Health}\n{attacker.unitName} casts {action.name} on {unit.unitName} for {action.baseValue} points");
        switch (action.effectType)
        {
            case UnitActionSO.EffectTypes.DAMAGE:
                //Put a method for calculating if attack landed or it missed
                TakeDamage(action.baseValue);
                ApplyDebuff(attacker, action);
                break;
            case UnitActionSO.EffectTypes.HEAL:
                myStats.Health += action.baseValue;
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
        Debug.Log($"{unitName} has {myStats.Health} left");

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
                    statusObj.unit = this;
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

    public void TakeDamage(int damage)
    {
        myStats.Health -= damage;
        if (myStats.Health <= 0)
        {
            PlayAnimation("Death");
            isDead = true;
        }
        else
        {
            //Debug.Log("HIT");
            PlayAnimation("Hit");
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
        currentActionValue = Mathf.CeilToInt(Mathf.Clamp(MAX_ACTION_VALUE / myStats.Speed, 0, MAX_ACTION_VALUE));


        foreach (var status in statusEffects)
        {
            if (status != null)
                status.OnTurnEnd();
        }
    }

    public void DecreaseActionValue(int value)
    {
        currentActionValue -= value;
    }
}
