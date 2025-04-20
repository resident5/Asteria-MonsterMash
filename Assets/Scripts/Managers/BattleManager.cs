using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Linq;
using Naninovel.Commands;
using System.Data;

public class BattleManager : MonoBehaviour
{
    private static BattleManager instance;
    public static BattleManager Instance { get { return instance; } }

    public EventSystem eventSystem;

    public UnitList unitList;

    public enum BattleState { START, BATTLESTART, WON, LOST, IDLE }

    public BattleState state;

    public GameObject emptySummon;
    public GameObject playerPrefab;
    public GameObject enemyPrefab;

    public PlayerUnit playerBattleUnit;
    public BattleUnit enemyBattleUnit;

    public BattleInfo battleInfo;

    public BattleHUDController battleHUD;

    public BattleUnit currentUnitTurn;

    public PlayerData playerData;
    public EnemyData enemyData;

    public float battleRate = 1f;
    private bool IsAllPlayersDead;
    private bool IsAllEnemiesDead;
    private bool IsPlayerSubmitting;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

        eventSystem = EventSystem.current;
        state = BattleState.IDLE;
        battleHUD.transform.parent.gameObject.SetActive(false);
    }

    public void InitializeBattle(PlayerController pController, EnemyController eController)
    {
        state = BattleState.START;
        playerData = pController.playerData;
        enemyData = eController.enemyData;
        battleInfo = new BattleInfo();
        battleHUD.transform.parent.gameObject.SetActive(true);

        StartCoroutine(BattleSceneSetup());
    }

    public bool DeInitializeBattle()
    {
        state = BattleState.IDLE;
        battleInfo = null;
        battleHUD.transform.parent.gameObject.SetActive(false);
        Debug.Log("Disable HUD");
        battleHUD.DisableHUD();
        playerData.playerStats.battleStats = playerBattleUnit.myDataStats.battleStats;
        //Destroy summons too
        Destroy(playerBattleUnit.gameObject);
        Destroy(enemyBattleUnit.gameObject);
        battleHUD.turnOrderSlider.DeInit();

        StopAllCoroutines();

        if (state == BattleState.LOST)
            return false;

        return true;

    }

    private void OnEnable()
    {
        EventManager.Instance.battleEvents.onPlayerDefeated += LoseCutscene;
    }

    private void OnDisable()
    {
        EventManager.Instance.battleEvents.onPlayerDefeated -= LoseCutscene;
    }

    private void Update()
    {
        if (state != BattleState.IDLE)
        {
            if (eventSystem.currentSelectedGameObject == null)
            {
                eventSystem.SetSelectedGameObject(eventSystem.firstSelectedGameObject);
            }

            IsAllPlayersDead = AllUnitsDead(battleInfo.ListOfAllies);
            IsAllEnemiesDead = AllUnitsDead(battleInfo.ListOfEnemies);
            IsPlayerSubmitting = PlayerSubmitted(playerBattleUnit);

            if (IsPlayerSubmitting)
            {
                state = BattleState.LOST;
                GameManager.Instance.ChangeState(GameManager.GameState.OVERWORLD);
                EventManager.Instance.battleEvents.PlayerSubmitted(enemyData);
            }

            if (IsAllPlayersDead)
            {
                state = BattleState.LOST;
                GameManager.Instance.ChangeState(GameManager.GameState.OVERWORLD);
                EventManager.Instance.battleEvents.PlayerDefeated(enemyData);
            }
            if (IsAllEnemiesDead)
            {
                state = BattleState.WON;
                GameManager.Instance.ChangeState(GameManager.GameState.OVERWORLD);
                EventManager.Instance.battleEvents.EnemyDefeated(enemyData);
            }

        }

        if (state == BattleState.BATTLESTART && currentUnitTurn == null)
        {
            CalculateTurnOrder();
        }

    }

    private bool AllUnitsDead(List<BattleUnit> listOfUnits)
    {
        if (listOfUnits != null)
        {
            foreach (var unit in listOfUnits)
            {
                if (unit.isDead)
                {
                    return true;
                }

                if (unit.isLusty)
                {
                    return true;
                }
            }
        }

        return false;
    }

    private bool PlayerSubmitted(PlayerUnit player)
    {
        return player.isLusty;
    }

    //TODO: Change Battle Scene setup to instantiate the player and the enemies then set their stats from the Overworld Player/Enemy DATA script
    //TODO: Remove the unit scriptableobject reference and have it be fed from the playerinfo/enemyinfo scripts
    private IEnumerator BattleSceneSetup()
    {
        GameObject pGo = Instantiate(playerPrefab, unitList.playerSpots.GetChild(0));
        GameObject eGo = Instantiate(enemyPrefab, unitList.enemySpots.GetChild(0));

        playerBattleUnit = pGo.GetComponent<PlayerUnit>();
        enemyBattleUnit = eGo.GetComponent<BattleUnit>();

        playerBattleUnit.SetupUnit(playerData);
        enemyBattleUnit.SetupUnit(enemyData);

        playerBattleUnit.manager = this;
        enemyBattleUnit.manager = this;

        playerBattleUnit.myDataStats.battleStats = playerData.playerStats.battleStats;
        playerBattleUnit.Summons = playerData.battleMons;

        enemyBattleUnit.animator.runtimeAnimatorController = enemyData.unitInfo.data.animatorController;


        battleInfo.ListOfAllies.Add(playerBattleUnit);
        battleInfo.ListOfEnemies.Add(enemyBattleUnit);

        battleHUD.turnOrderSlider.Init(battleInfo.ListOfAllUnits);

        yield return null;

        state = BattleState.BATTLESTART;

        //ChangeBattleState(BattleState.PLAYERTURN);
        //HUDcontroller.SetHUD(enemyUnit);
    }

    private void CalculateTurnOrder()
    {
        if (currentUnitTurn != null)
            return;

        foreach (var unitTurn in battleInfo.ListOfAllUnits)
        {
            unitTurn.DecreaseActionValue(1);
            //battleHUD.turnOrderSlider.UpdateHUD(1);

            if (unitTurn.currentActionValue <= 0)
            {
                currentUnitTurn = unitTurn;
                currentUnitTurn.OnTurnStart();
                break;
            }
        }

    }

    public BattleUnit GetFastestUnit(List<BattleUnit> units)
    {
        return units.OrderBy(x => x.currentActionValue).First();
    }

    public void OnPlayerTurn(UnitActionSO battleMove)
    {
        StartCoroutine(PlayerAttack(battleMove));
    }

    public IEnumerator PlayerAttack(UnitActionSO battleMove)
    {
        enemyBattleUnit.ApplyAction(currentUnitTurn, battleMove);

        currentUnitTurn.OnTurnEnd();

        yield return new WaitForSeconds(battleRate);

        if (GameManager.Instance.debugger.isDebugging)
            Debug.Log("Player Turn has ended");

        currentUnitTurn = null;
    }

    public bool SummonNewUnit(UnitData data)
    {
        GameObject sGo;
        Transform spot = null;

        if (data is MonsterData monData)
        {
            spot = GetAvailableSpots(unitList.playerSpots);
        }
        else if (data is EnemyData enemyData)
        {
            spot = GetAvailableSpots(unitList.enemySpots);
        }
        else
        {
            Debug.LogError("Invalid UnitData type for summoning.");
            return false;
        }

        if (spot == null)
            return false;
        else
        {
            sGo = Instantiate(emptySummon, spot);
            BattleUnit bUnit = sGo.GetComponent<BattleUnit>();
            bUnit.SetupUnit(data);
            battleHUD.turnOrderSlider.AddNewUnit(bUnit);

            if(bUnit is PlayerUnit)
            {
                battleInfo.ListOfAllies.Add(bUnit);
            }
            else
            {
                battleInfo.ListOfEnemies.Add(bUnit);
            }
        }
        currentUnitTurn.OnTurnEnd();

        currentUnitTurn = null;
        return true;
    }

    private Transform GetAvailableSpots(Transform spots)
    {
        Transform availableSpot = null;
        foreach (Transform spot in spots)
        {
            if (spot.childCount <= 0)
            {
                availableSpot = spot;
            }
        }

        return availableSpot;
    }

    private void LoseCutscene(EnemyData eData)
    {
        DialogueManager.Instance.StartConversation(eData.dialogueScript, "Lost");
    }
}
