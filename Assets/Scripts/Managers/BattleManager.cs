using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Linq;
using Unity.VisualScripting.ReorderableList;

public class BattleManager : MonoBehaviour
{
    private static BattleManager instance;
    public static BattleManager Instance { get { return instance; } }

    public EventSystem eventSystem;

    public UnitList unitList;

    public enum BattleState { START, BATTLESTART, WON, LOST, IDLE }

    public BattleState state;

    public GameObject playerPrefab;
    public GameObject enemyPrefab;

    BattleUnit playerUnit;
    BattleUnit enemyUnit;

    public BattleInfo battleInfo;

    public BattleHUDController HUDcontroller;

    public List<BattleUnit> turnOrder;

    public BattleUnit currentUnitTurn;

    public float battleRate = 1f;
    bool IsAllPlayersDead;
    bool IsAllEnemiesDead;

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
        HUDcontroller.transform.parent.gameObject.SetActive(false);
    }

    public void InitializeBattle()
    {
        state = BattleState.START;
        battleInfo = new BattleInfo();
        HUDcontroller.transform.parent.gameObject.SetActive(true);

        StartCoroutine(BattleSceneSetup());
    }

    public bool DeInitializeBattle()
    {
        state = BattleState.IDLE;
        battleInfo = null;
        HUDcontroller.transform.parent.gameObject.SetActive(false);
        HUDcontroller.DisableHUD();
        Destroy(playerUnit.gameObject);
        Destroy(enemyUnit.gameObject);

        StopAllCoroutines();

        if (state == BattleState.LOST)
            return false;

        return true;
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

            if (IsAllPlayersDead)
            {
                state = BattleState.LOST;
                GameManager.Instance.ChangeState(GameManager.GameState.OVERWORLD);
            }
            if (IsAllEnemiesDead)
            {
                state = BattleState.WON;
                GameManager.Instance.ChangeState(GameManager.GameState.OVERWORLD);
            }
            while (currentUnitTurn == null)
            {
                CalculateTurnOrder();
            }

            if (state != BattleState.WON || state != BattleState.LOST)
            {
                OnEnemyTurn();
            }
        }

    }

    public bool AllUnitsDead(List<BattleUnit> listOfUnits)
    {
        if (listOfUnits != null)
        {
            foreach (var unit in listOfUnits)
            {
                if (!unit.isDead)
                {
                    return false;
                }
            }
        }

        return true;
    }

    IEnumerator BattleSceneSetup()
    {
        GameObject pGo = Instantiate(playerPrefab, unitList.playerSpots.GetChild(0));
        playerUnit = pGo.GetComponent<BattleUnit>();
        playerUnit.myStats.Health = playerUnit.myStats.MaxHealth;
        battleInfo.ListOfAllies.Add(playerUnit);
        playerUnit.Init();

        GameObject eGo = Instantiate(enemyPrefab, unitList.enemySpots.GetChild(0));
        enemyUnit = eGo.GetComponent<BattleUnit>();
        enemyUnit.myStats.Health = enemyUnit.myStats.MaxHealth;
        battleInfo.ListOfEnemies.Add(enemyUnit);
        enemyUnit.Init();

        HUDcontroller.SetHUD(playerUnit);

        yield return null;
        
        state = BattleState.BATTLESTART;

        CalculateTurnOrder();


        //ChangeBattleState(BattleState.PLAYERTURN);
        //HUDcontroller.SetHUD(enemyUnit);
    }

    void CalculateTurnOrder()
    {
        //List<BattleUnit> list = battleInfo.ListOfAllies.Concat(battleInfo.ListOfEnemies).OrderByDescending(x => x.actionValue).ToList();
        if (currentUnitTurn != null)
            return;

        foreach (var item in battleInfo.TurnOrder)
        {
            item.DecreaseActionValue(1);

            if (item.actionValue <= 0)
            {
                currentUnitTurn = item;
                currentUnitTurn.OnTurnStart();
                break;
            }
        }

    }

    public void OnPlayerTurn(UnitAction battleMove)
    {
        //Change this to any player unit or check if the unit has a player tag
        if (currentUnitTurn != playerUnit)
            return;

        StartCoroutine(PlayerAttack(battleMove));
    }

    public IEnumerator PlayerAttack(UnitAction battleMove)
    {
        enemyUnit.ApplyEffect(battleMove);

        yield return new WaitForSeconds(battleRate);

        currentUnitTurn.OnTurnEnd();

        currentUnitTurn = null;
    }

    public void OnEnemyTurn()
    {
        if (currentUnitTurn != enemyUnit)
            return;

        StartCoroutine(EnemyAttack());
    }

    public IEnumerator EnemyAttack()
    {
        yield return null;

        UnitAction chosenMove = GetEnemyUnitAction();
        BattleUnit[] targetList = GetEnemyTargets(chosenMove);

        foreach (var target in targetList)
        {
            target.ApplyEffect(chosenMove);
        }

        currentUnitTurn.OnTurnEnd();

        currentUnitTurn = null;
    }

    //public void ChangeBattleState(BattleState newState)
    //{
    //    state = newState;
    //}

    private UnitAction GetEnemyUnitAction()
    {
        List<UnitAction> enemyActionsList = enemyUnit.myBattleMoves;
        int rand = Random.Range(0, enemyActionsList.Count);

        return enemyActionsList[rand];
    }

    private BattleUnit[] GetEnemyTargets(UnitAction selectedAction)
    {
        int randTarget = 0;
        List<BattleUnit> targets = new List<BattleUnit>();

        switch (selectedAction.targetTypes)
        {
            case UnitAction.TargetTypes.RANDOMALLY:
                randTarget = Random.Range(0, battleInfo.ListOfEnemies.Count);
                targets.Add(battleInfo.ListOfEnemies[randTarget]);
                break;
            case UnitAction.TargetTypes.RANDOMOPPONENT:
                randTarget = Random.Range(0, battleInfo.ListOfAllies.Count);
                targets.Add(battleInfo.ListOfAllies[randTarget]);
                break;
            case UnitAction.TargetTypes.ALLALLIES:
                targets.AddRange(battleInfo.ListOfEnemies);
                //Allow enemy to select multiple allies
                break;
            case UnitAction.TargetTypes.ALLENEMIES:
                targets.AddRange(battleInfo.ListOfAllies);
                //Allow multiple enemies but not all
                break;
            case UnitAction.TargetTypes.ALL:
                targets.AddRange(battleInfo.ListOfAllies);
                targets.AddRange(battleInfo.ListOfEnemies);
                break;
        }

        return targets.ToArray();

    }

    //Make an IEnumerator to handle the turns
    //Make it so the player can only proceed unless they target an enemy or cancel


    //public void SelectUnit(BattleUnit unit)
    //{
    //    selectedUnit = unit;
    //}


}
