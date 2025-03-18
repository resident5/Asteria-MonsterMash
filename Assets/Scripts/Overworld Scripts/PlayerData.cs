using System.Collections.Generic;
using System.Runtime.Serialization.Json;
using UnityEngine;

//TODO: Make a base Data script that both playerData and enemyData can inherit from
public class PlayerData : UnitData
{
    private static PlayerData instance;
    public static PlayerData Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindAnyObjectByType<PlayerData>();
            }
            return instance;
        }
    }

    public List<UnitCreatorSO> battleMons;
    //public List<BattleUnit> battleMon;
    public GameManager gameManager => GameManager.Instance;

    public UnitCreatorSO playerCreator;

    public Interactable focus;
    public float detectionRadius;

    [Header("Stats")]
    public Data data;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        data = playerCreator.Copy().data;
        data.stats.InitStats();

        EventManager.Instance.playerEvents.PlayerLevelChanged(data.stats.level);
        EventManager.Instance.playerEvents.PlayerExperienceChanged(currentXP);
        requiredXP = CalculateRequiredXP();

        SetupEmote(worldCanvas);
    }

    private void OnEnable()
    {
        EventManager.Instance.playerEvents.onPlayerExperienceGained += GainExperience;
    }

    private void OnDisable()
    {
        EventManager.Instance.playerEvents.onPlayerExperienceGained -= GainExperience;
    }

    private void Update()
    {
        CheckInteractables();
    }

    public void CheckInteractables()
    {
        bool foundtarget = false;

        Collider[] colliders = Physics.OverlapSphere(transform.position, detectionRadius);
        foreach (Collider collider in colliders)
        {
            Interactable interactable = collider.GetComponent<Interactable>();
            if (interactable != null)
            {
                SetFocus(interactable);
                foundtarget = true;
                break;
            }
        }


        if (!foundtarget)
        {
            focus = null;
        }
    }

    public void SetFocus(Interactable newFocus)
    {
        if (focus != newFocus)
        {
            if (focus != null)
            {
                focus.DeFocused();
            }
            focus = newFocus;
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        //GameObject colObj = collision.gameObject;

        //if (colObj.tag == "Enemy")
        //{
        //    EnemyData eData = colObj.GetComponent<EnemyData>();

        //    if (eData.enemyState == EnemyData.EnemyState.CHASING)
        //    {
        //        gameManager.InitiateBattle(this, eData);
        //    }
        //}
        //Change enemy to idle
    }

    public override void TakeDamage(int damage)
    {
        data.stats.Health -= damage;
    }

    public override void GainFlatExperience(int exp)
    {
        currentXP += exp;
    }

    public override void GainExperience(int experience)
    {
        currentXP += experience;

        if (currentXP >= requiredXP)
        {
            LevelUp();
            EventManager.Instance.playerEvents.PlayerLevelChanged(data.stats.level);
        }

        EventManager.Instance.playerEvents.PlayerExperienceChanged(currentXP);
    }

    public override void LevelUp()
    {
        data.stats.LevelUp();

        //data.stats.MaxHealth += (int)Mathf.Ceil((100 - data.level) * 0.05f);
        //data.stats.Health = data.stats.MaxHealth;
        //data.stats.Mana += (int)Mathf.Ceil((100 - data.level) * 0.05f);
        //data.stats.Strength += (int)Mathf.Ceil((100 - data.level) * 0.05f);
        //data.stats.Magic += (int)Mathf.Ceil((100 - data.level) * 0.05f);
        //data.stats.Speed += (int)Mathf.Ceil((100 - data.level) * 0.05f);

        currentXP = Mathf.RoundToInt(currentXP - requiredXP);
        requiredXP = CalculateRequiredXP();
    }

    private int CalculateRequiredXP()
    {
        int solverXp = 0;
        int level = data.stats.level;

        for (int i = 0; i < level; i++)
        {
            solverXp += (int)Mathf.Floor(level + additonMultiplier * Mathf.Pow(powerMultiplier, level / divisionMultiplier));
        }

        return solverXp / 4;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
    //Keep track of the player's movements and health
}
