using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Cinemachine;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public CameraManager cameraManager;
    public BattleManager battleManager;
    public InputManager inputManager;

    public Debugger debugger;

    public enum GameState
    {
        OVERWORLD,
        BATTLE,
        MENU
    }
    public GameState state;

    public Scene overWorldScene;
    public Scene battleScene;

    public GameObject enemyObj;
    public PlayerData playerData;

    public HUDController hudController;
    public bool isPaused = false;

    private void Awake()
    {
        overWorldScene = SceneManager.GetActiveScene();
        cameraManager = GetComponentInChildren<CameraManager>();
        hudController = FindObjectOfType<HUDController>();
        playerData = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerData>();
        inputManager = GetComponentInChildren<InputManager>();
        debugger = GetComponent<Debugger>();

        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(Instance);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        state = GameState.OVERWORLD;
        StartCoroutine(PreloadBattleScene("Battle"));
    }

    public void ChangeState(GameState newState, PlayerData data = null)
    {
        state = newState;

        //Immediately do start function for changingstates
        switch (state)
        {
            case GameState.OVERWORLD:
                bool hasWon = battleManager.DeInitializeBattle();
                cameraManager.DeactivateBattleCamera();
                cameraManager.SwapToOverWorldCam();

                if (enemyObj != null)
                    Destroy(enemyObj);

                if (hasWon)
                    Debug.Log("Won the fight");
                else
                    Debug.Log("Lost the fight :(");

                break;
            case GameState.BATTLE:
                battleManager.InitializeBattle(playerData);
                cameraManager.ActivateBattleCamera();
                cameraManager.SwapToBattleCam();
                break;
            case GameState.MENU:
                break;
            default:
                break;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            cameraManager.SwapToOverWorldCam();
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            cameraManager.SwapToBattleCam();
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            isPaused = !isPaused;
            Time.timeScale = isPaused ? 0 : 1;

            hudController.ShowPauseMenu(playerData, isPaused);
        }

        //Cancelled Works for 1st to 2nd panel and vice versa but not 3rd to 2nd panel due to getting the first button not working if its off in the battle.
        if (inputManager.Cancelled)
        {
            if (isPaused)
            {
                hudController.HideAllPanels();
            }
            else
            {
                battleManager.battleHUD.ReturnPanel();
            }
        }

    }

    private void ReturnToPreviousBattleMenu()
    {

    }

    //private void ReturnToPreviousMenu()
    //{
    //    hudController.HideAllPanels();
    //}

    public IEnumerator PreloadBattleScene(string sceneName)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);

            if (operation.progress >= 0.9f && GameManager.Instance.state == GameState.BATTLE)
            {
            }

            yield return null;
        }

        if (operation.isDone)
        {
            battleScene = SceneManager.GetSceneByName("Battle");

            if (battleScene.IsValid())
            {
                GameObject[] objs = battleScene.GetRootGameObjects();
                foreach (var item in objs)
                {
                    if (battleManager == null)
                    {
                        battleManager = item.GetComponent<BattleManager>();
                    }

                    CinemachineVirtualCamera battleCam = item.GetComponent<CinemachineVirtualCamera>();

                    if (battleCam != null)
                    {
                        cameraManager.SetBattleCamera(battleCam);
                    }
                }

            }
        }
        Debug.Log("Scene finished loading");

    }

    public void InitiateBattle(PlayerData pData, GameObject eObj)
    {
        enemyObj = eObj;
        playerData = pData;
        ChangeState(GameState.BATTLE);
    }

    public void DeInitiateBattle()
    {
        enemyObj = null;
        ChangeState(GameState.OVERWORLD);
    }


}
