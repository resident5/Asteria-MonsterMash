using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UIElements;
using JetBrains.Annotations;

public class BattleManager : MonoBehaviour
{
    private static BattleManager instance;
    public static BattleManager Instance { get { return instance; } }

    public EventSystem eventSystem;

    public UnitList unitList;

    public GameObject[] targets;

    public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST}

    public enum TurnPhase
    {
        NONE,
        ATTACKSELECTPHASE,
        TARGETSELECTPHASE,
        EXECUTEPHASE
    }

    public BattleState state;
    public TurnPhase phase;

    public GameObject playerPrefab;
    public GameObject enemyPrefab;

    BattleUnit playerUnit;
    BattleUnit enemyUnit;

    public BattleHUDController HUDcontroller;
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }

        eventSystem = EventSystem.current;

    }

    private void Start()
    {
        //activeUnit = unitList.playerList[0];
        state = BattleState.START;

        StartCoroutine(BattleSetup());
    }

    private void Update()
    {
        if (eventSystem.currentSelectedGameObject == null)
        {
            eventSystem.SetSelectedGameObject(eventSystem.firstSelectedGameObject);
        }

        onPlayerTurn();
    }

    IEnumerator BattleSetup()
    {
        GameObject pGo = Instantiate(playerPrefab, unitList.playerSpots.GetChild(0));
        playerUnit = pGo.GetComponent<BattleUnit>();

        GameObject eGo = Instantiate(enemyPrefab, unitList.enemySpots.GetChild(0));
        enemyUnit = eGo.GetComponent<BattleUnit>();

        HUDcontroller.SetHUD(playerUnit);

        yield return new WaitForSeconds(2f);

        state = BattleState.PLAYERTURN;
        onPlayerTurn();
        //HUDcontroller.SetHUD(enemyUnit);
    }

    public void onPlayerTurn()
    {
    }

    public void AttackButton(UnitAction battleMove)
    {
        if(state != BattleState.PLAYERTURN)
            return;

        StartCoroutine(PlayerAttack(battleMove));
    }

    public IEnumerator PlayerAttack(UnitAction battleMove)
    {
        enemyUnit.ApplyEffect(battleMove);

        yield return new WaitForSeconds(2f);

        state = BattleState.ENEMYTURN;
    }

    //Make an IEnumerator to handle the turns
    //Make it so the player can only proceed unless they target an enemy or cancel


    //public void SelectUnit(BattleUnit unit)
    //{
    //    selectedUnit = unit;
    //}


}
