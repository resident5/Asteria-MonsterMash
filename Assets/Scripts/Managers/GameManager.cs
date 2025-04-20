using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;
using static BattleManager;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Managers")]
    public CameraManager cameraManager;
    public BattleManager battleManager;
    public InputManager inputManager;
    public VariableManager variableManager;
    public EventManager eventManager;
    public SceneController sceneManager;
    public DialogueManager dialogueManager;
    public QuestManager questManager;

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

    public EnemyController enemyController;
    public PlayerController playerController;

    public HUDController hudController;
    public bool isPaused = false;
    public bool isInteracting = false;

    [Header("World Info")]
    public Teleporter[] teleportSpots;
    public Transform worldCanvas;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(Instance);
        }
        else
        {
            Destroy(this.gameObject);
            return;
        }

        LeanTween.reset();
        overWorldScene = SceneManager.GetActiveScene();
        cameraManager = GetComponentInChildren<CameraManager>();
        variableManager = GetComponentInChildren<VariableManager>();
        eventManager = GetComponentInChildren<EventManager>();
        sceneManager = GetComponentInChildren<SceneController>();
        dialogueManager = GetComponentInChildren<DialogueManager>();
        questManager = GetComponentInChildren<QuestManager>();
        hudController = FindObjectOfType<HUDController>();
        teleportSpots = FindObjectsOfType<Teleporter>();
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        inputManager = GetComponentInChildren<InputManager>();
        debugger = GetComponent<Debugger>();
        worldCanvas = GameObject.Find("World Canvas").transform;
    }

    private void Start()
    {
        state = GameState.OVERWORLD;
        StartCoroutine(PreloadBattleScene("Battle"));
        hudController.SetupMenu(playerController.playerData);
    }

    public void ChangeState(GameState newState, PlayerData data = null)
    {
        state = newState;

        //Immediately do start function for changingstates
        switch (state)
        {
            case GameState.OVERWORLD:
                if (battleManager.state != BattleState.IDLE)
                {
                    bool hasWon = battleManager.DeInitializeBattle();
                    cameraManager.DeactivateBattleCamera();
                    cameraManager.SwapToOverWorldCam();

                    if (enemyController != null)
                    {
                        enemyController.EndBattle();
                        enemyController = null;
                    }

                    if (hasWon == true)
                        Debug.Log("Won the fight");
                    else if (hasWon == false)
                        Debug.Log("Lost the fight :(");
                }

                break;
            case GameState.BATTLE:
                battleManager.InitializeBattle(playerController, enemyController);
                cameraManager.ActivateBattleCamera();
                cameraManager.SwapToBattleCam();
                break;
            case GameState.MENU:
                break;
            case GameState.DIALOGUE:
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
            Debug.Log("INTERACT");
            if (playerController.focus != null)
            {
                playerController.focus.Focused();
                playerController.focus = null;
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
            hudController.SetupMenu(playerController.playerData);
            hudController.ShowMenu(hudController.main);
            hudController.ShowPauseMenu(playerController.playerData, isPaused);
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
        playerController.transform.position = newLocation.position;
    }

    public void InitiateBattle(PlayerController pController, EnemyController eController)
    {
        enemyController = eController;
        playerController = pController;
        ChangeState(GameState.BATTLE);
    }

    public void DeInitiateBattle()
    {
        ChangeState(GameState.OVERWORLD);
    }


}
