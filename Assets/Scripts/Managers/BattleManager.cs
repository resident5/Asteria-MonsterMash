using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Linq;

//TODO: Set Enemies' idle animation to include graphix local position*
//TODO: Setup Daespec's animations
//TODO: Set up merchant
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
    public BattleUnit enemyUnit;

    public BattleInfo battleInfo;

    public BattleHUDController battleHUD;

    public BattleUnit currentUnitTurn;

    public PlayerData playerData;
    public EnemyData enemyData;

    public float battleRate = 1f;
    bool IsAllPlayersDead;
    bool IsAllEnemiesDead;
    bool IsPlayerSubmitting;

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

    public void InitializeBattle(PlayerData playData, EnemyData eData)
    {
        state = BattleState.START;
        playerData = playData;
        enemyData = eData;
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
        playerData.data.stats = playerBattleUnit.data.stats;
        Destroy(playerBattleUnit.gameObject);
        Destroy(enemyUnit.gameObject);
        battleHUD.turnOrderSlider.DeInit();

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
            IsPlayerSubmitting = PlayerSubmitted(playerBattleUnit);

            if (IsPlayerSubmitting)
            {
                state = BattleState.LOST;
                //Play submit dialogue
                GameManager.Instance.ChangeState(GameManager.GameState.OVERWORLD);
            }

            if (IsAllPlayersDead)
            {
                state = BattleState.LOST;
                //Play Dialogue for lost scene
                GameManager.Instance.ChangeState(GameManager.GameState.OVERWORLD);
            }
            if (IsAllEnemiesDead)
            {
                state = BattleState.WON;
                //Play dislogue for win scene
                GameManager.Instance.ChangeState(GameManager.GameState.OVERWORLD);
            }
            
        }

        if (state == BattleState.BATTLESTART && currentUnitTurn == null)
        {
            CalculateTurnOrder();
        }

    }

    bool AllUnitsDead(List<BattleUnit> listOfUnits)
    {
        if (listOfUnits != null)
        {
            foreach (var unit in listOfUnits)
            {
                if (unit.isDead)
                {
                    return true;
                }

                if(unit.isLusty)
                {
                    return true;
                }
            }
        }

        return false;
    }

    public bool PlayerSubmitted(PlayerUnit player)
    {
        return player.isLusty;
    }

    //TODO: Change Battle Scene setup to instantiate the player and the enemies then set their stats from the Overworld Player/Enemy DATA script
    //TODO: Remove the unit scriptableobject reference and have it be fed from the playerinfo/enemyinfo scripts
    IEnumerator BattleSceneSetup()
    {
        GameObject pGo = Instantiate(playerPrefab, unitList.playerSpots.GetChild(0));
        playerBattleUnit = pGo.GetComponent<PlayerUnit>();

        playerBattleUnit.manager = this;
        playerBattleUnit.unit.data.stats = playerData.data.stats;
        playerBattleUnit.Summons = playerData.battleMons;

        playerBattleUnit.Init(playerData);

        GameObject eGo = Instantiate(enemyPrefab, unitList.enemySpots.GetChild(0));
        enemyUnit = eGo.GetComponent<BattleUnit>();
        enemyUnit.animator.runtimeAnimatorController = enemyData.unitInfo.data.animatorController;

        enemyUnit.manager = this;
        enemyUnit.Init(enemyData.unitInfo);

        battleInfo.ListOfAllies.Add(playerBattleUnit);
        battleInfo.ListOfEnemies.Add(enemyUnit);

        battleHUD.turnOrderSlider.Init(battleInfo.ListOfAllUnits);

        yield return null;

        state = BattleState.BATTLESTART;

        //ChangeBattleState(BattleState.PLAYERTURN);
        //HUDcontroller.SetHUD(enemyUnit);
    }

    void CalculateTurnOrder()
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

    //Dictionary<BattleUnit, int> GetTurnOrder(List<BattleUnit> turnUnits)
    //{
    //    List<BattleUnit> tempList = turnUnits;
    //    turnOrders = new Dictionary<BattleUnit, int>();

    //    int listCounter = 0;
    //    int actionVal = 0;

    //    while (listCounter < 10)
    //    {
    //        BattleUnit fastestUnit = GetFastestUnit(turnUnits);

    //        Debug.Log($"{fastestUnit.name} has a base of {fastestUnit.baseActionValue} action points and is currently at {fastestUnit.currentActionValue}");
    //        //Create a list to hold the turn order based on the CURRENTaction value


    //        //turnOrders.Add(fastestUnit, fastestUnit.currentActionValue);
    //        fastestUnit.currentActionValue += fastestUnit.baseActionValue;

    //        listCounter++;
    //    }

    //    //Get unit's action values
    //    //If Unit A's value is higher than B put unit B in list
    //    //Add Unit B's value to itself then check if that value is higher than A if not add B again
    //    //Repeat until 10 times 

    //    return turnOrders;
    //}

    public BattleUnit GetFastestUnit(List<BattleUnit> units)
    {
        return units.OrderBy(x => x.currentActionValue).First();
    }

    public void OnPlayerTurn(UnitActionSO battleMove)
    {
        //Change this to any player unit or check if the unit has a player tag
        if (currentUnitTurn != playerBattleUnit)
            return;

        StartCoroutine(PlayerAttack(battleMove));
    }

    public IEnumerator PlayerAttack(UnitActionSO battleMove)
    {
        enemyUnit.ApplyAction(currentUnitTurn, battleMove);

        currentUnitTurn.OnTurnEnd();

        yield return new WaitForSeconds(battleRate);

        if (GameManager.Instance.debugger.isDebugging)
            Debug.Log("Player Turn has ended");

        currentUnitTurn = null;
    }

    //TODO: After summoning, you should insert the current unit's turn into the list.
    public bool SummonNewUnit(UnitCreatorSO unit, bool isSummon)
    {
        GameObject sGo;
        Transform spot = null;
        if (isSummon)
        {
            spot = GetAvailableSpots(unitList.playerSpots);
            sGo = Instantiate(emptySummon, spot);
        }
        else
        {
            spot = GetAvailableSpots(unitList.playerSpots);
            sGo = Instantiate(emptySummon, spot);
        }

        if (spot == null)
            return false;

        sGo.GetComponent<BattleUnit>().Init(unit);

        currentUnitTurn.OnTurnEnd();

        currentUnitTurn = null;
        return true;
    }

    public Transform GetAvailableSpots(Transform spots)
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

    //public void OnEnemyTurn()
    //{
    //    if (currentUnitTurn != enemyUnit)
    //        return;

    //    StartCoroutine(EnemyAttack());
    //}

    //public IEnumerator EnemyAttack()
    //{
    //    yield return new WaitForSeconds(battleRate);

    //    UnitActionSO chosenMove = GetEnemyUnitAction();
    //    BattleUnit[] targetList = GetEnemyTargets(chosenMove);

    //    foreach (var target in targetList)
    //    {
    //        target.ApplyAction(currentUnitTurn, chosenMove);
    //    }

    //    currentUnitTurn.OnTurnEnd();

    //    Debug.Log("Enemy Turn has ended");

    //    currentUnitTurn = null;
    //}

    //public void ChangeBattleState(BattleState newState)
    //{
    //    state = newState;
    //}

    //private UnitActionSO GetEnemyUnitAction()
    //{
    //    List<UnitActionSO> enemyActionsList = enemyUnit.myBattleMoves;
    //    int rand = Random.Range(0, enemyActionsList.Count);

    //    return enemyActionsList[rand];
    //}

    //public BattleUnit[] GetEnemyTargets(UnitActionSO selectedAction)
    //{
    //    int randTarget = 0;
    //    List<BattleUnit> targets = new List<BattleUnit>();

    //    switch (selectedAction.targetTypes)
    //    {
    //        case UnitActionSO.TargetTypes.RANDOMALLY:
    //            randTarget = Random.Range(0, battleInfo.ListOfEnemies.Count);
    //            targets.Add(battleInfo.ListOfEnemies[randTarget]);
    //            break;
    //        case UnitActionSO.TargetTypes.RANDOMOPPONENT:
    //            randTarget = Random.Range(0, battleInfo.ListOfAllies.Count);
    //            targets.Add(battleInfo.ListOfAllies[randTarget]);
    //            break;
    //        case UnitActionSO.TargetTypes.ALLALLIES:
    //            targets.AddRange(battleInfo.ListOfEnemies);
    //            //Allow enemy to select multiple allies
    //            break;
    //        case UnitActionSO.TargetTypes.ALLENEMIES:
    //            targets.AddRange(battleInfo.ListOfAllies);
    //            //Allow multiple enemies but not all
    //            break;
    //        case UnitActionSO.TargetTypes.ALL:
    //            targets.AddRange(battleInfo.ListOfAllies);
    //            targets.AddRange(battleInfo.ListOfEnemies);
    //            break;
    //    }

    //    return targets.ToArray();

    //}

    //Make an IEnumerator to handle the turns
    //Make it so the player can only proceed unless they target an enemy or cancel


    //public void SelectUnit(BattleUnit unit)
    //{
    //    selectedUnit = unit;
    //}


}
