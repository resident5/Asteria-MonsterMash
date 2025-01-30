using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public CameraManager cameraManager;
    public DialogueSystem dialogueSystem;
    public BattleManager battleManager;
    public InputManager inputManager;
    public VariableManager variableManager;
    public EventManager eventManager;

    public Debugger debugger;

    public enum GameState
    {
        OVERWORLD,
        DIALOGUE,
        BATTLE,
        MENU
    }
    public GameState state;

    public Scene overWorldScene;
    public Scene battleScene;

    public EnemyData enemyData;
    public PlayerData playerData;

    public HUDController hudController;
    public bool isPaused = false;
    public bool isInteracting = false;

    public Teleporter[] teleportSpots;

    private void Awake()
    {
        LeanTween.reset();
        overWorldScene = SceneManager.GetActiveScene();
        cameraManager = GetComponentInChildren<CameraManager>();
        dialogueSystem = GetComponentInChildren<DialogueSystem>();
        variableManager = GetComponentInChildren<VariableManager>();
        eventManager = GetComponentInChildren<EventManager>();
        hudController = FindObjectOfType<HUDController>();
        teleportSpots = FindObjectsOfType<Teleporter>();
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
        hudController.SetupMenu(playerData);
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

                if (enemyData != null)
                {
                    enemyData.EndBattle();
                    enemyData = null;
                }

                if (hasWon == true)
                    Debug.Log("Won the fight");
                else if (hasWon == false)
                    Debug.Log("Lost the fight :(");

                break;
            case GameState.BATTLE:
                battleManager.InitializeBattle(playerData, enemyData);
                cameraManager.ActivateBattleCamera();
                cameraManager.SwapToBattleCam();
                break;
            case GameState.MENU:
                break;
            case GameState.DIALOGUE:
                dialogueSystem.gameManager = this;
                //dialogueSystem.dialogueUI.right.VNImage.sprite = actorSprite;
                break;
            default:
                break;
        }
    }

    //public void StartDialogue(string dialogue)
    //{
    //    ChangeState(GameState.DIALOGUE);
    //    //dialogueSystem.StartDialogue(dialogue);
    //}

    private void Update()
    {
        Inputs();

        if (inputManager.Interacted && !isInteracting)
        {
            if (playerData.focus != null)
            {
                playerData.focus.Focused();
                playerData.focus = null;
            }
        }

        //Cancelled Works for 1st to 2nd panel and vice versa but not 3rd to 2nd panel because getting the first button won't work if its off in the battle.
        if (inputManager.Cancelled)
        {
            if (isInteracting)
                isInteracting = false;

            hudController.HideMenu();
            battleManager.battleHUD.HideMenu();
            //TODO: REMEMBER TO UNPAUSE WHEN CANCELLING
        }
    }

    void Inputs()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            Pause();
        }
    }

    void Pause()
    {
        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0 : 1;

        if (isPaused)
        {
            hudController.ShowMenu(hudController.main);
            hudController.ShowPauseMenu(playerData, isPaused);
        }
        else
        {
            hudController.CloseMenu();
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
    }

    public void MovePlayer(Transform newLocation)
    {
        playerData.transform.position = newLocation.position;
    }

    public void InitiateBattle(PlayerData pData, EnemyData eData)
    {
        enemyData = eData;
        playerData = pData;
        ChangeState(GameState.BATTLE);
    }

    public void DeInitiateBattle()
    {
        ChangeState(GameState.OVERWORLD);
    }


}
