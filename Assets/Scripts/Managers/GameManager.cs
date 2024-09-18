using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Cinemachine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public CameraManager cameraManager;
    public BattleManager battleManager;

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

    private void Awake()
    {
        overWorldScene = SceneManager.GetActiveScene();
        cameraManager = GetComponentInChildren<CameraManager>();

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

        if(Input.GetKeyDown(KeyCode.Tab))
        {

        }
    }

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
