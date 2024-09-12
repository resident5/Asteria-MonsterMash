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

    public enum GameState
    {
        OVERWORLD,
        BATTLE,
        MENU
    }
    public GameState state;

    public Scene overWorldScene;

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

    public void ChangeState(GameState newState)
    {
        state = newState;

        switch (state)
        {
            case GameState.OVERWORLD:
                break;
            case GameState.BATTLE:
                Debug.Log("Load Battle");
                cameraManager.ActivateBattleCamera();
                //Scene battleScene = SceneManager.GetSceneByName("Battle");

                //if (battleScene.IsValid())
                //{
                //    GameObject[] objs = battleScene.GetRootGameObjects();

                //    foreach (var item in objs)
                //    {
                //        CinemachineVirtualCamera battleCam = item.GetComponent<CinemachineVirtualCamera>();

                //        if (battleCam != null)
                //        {

                //        }
                //        item.SetActive(true);
                //    }
                //}

                //if (overWorldScene.IsValid())
                //{
                //    GameObject[] objs = overWorldScene.GetRootGameObjects();
                //    foreach (var item in objs)
                //    {
                //        item.SetActive(true);
                //    }
                //}

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

        switch (state)
        {
            case GameState.OVERWORLD:
                break;
            case GameState.BATTLE:
                break;
            case GameState.MENU:
                break;
            default:
                break;
        }
    }

    public IEnumerator PreloadBattleScene(string sceneName)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);
            Debug.Log(progress);

            if (operation.progress >= 0.9f && GameManager.Instance.state == GameState.BATTLE)
            {
            }

            yield return null;
        }

        if (operation.isDone)
        {
            Scene battleScene = SceneManager.GetSceneByName("Battle");

            if (battleScene.IsValid())
            {
                GameObject[] objs = battleScene.GetRootGameObjects();

                foreach (var item in objs)
                {
                    CinemachineVirtualCamera battleCam = item.GetComponent<CinemachineVirtualCamera>();
                    cameraManager.SetBattleCamera(battleCam);
                }

            }
        }
        Debug.Log("Scene finished loading");

    }


}
