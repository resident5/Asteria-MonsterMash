using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public Scene battleScene;

    private void Start()
    {
        battleScene = SceneManager.GetSceneByName("Battle");
    }

    public IEnumerator LoadNextScene(string sceneName)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);

            if (operation.progress >= 0.9f)
            {

            }

            yield return null;
        }
        //Find out how to set cinemachine camera on new scene

        //StartCoroutine(SceneController.Instance.UnloadScenesExceptBattle());

        //if (operation.isDone)
        //{
        //    nextScene = SceneManager.GetSceneByName(nextSceneName);
        //    if (nextScene.IsValid())
        //    {
        //        Teleporter[] teleporterSpots = FindObjectsOfType<Teleporter>();
        //        CinemachineVirtualCamera cam = GetNewCinemachineCameraInScene();
        //        foreach (var spot in teleporterSpots)
        //        {
        //            if (targetTeleportID == spot.teleportID)
        //            {
        //                GameManager.Instance.MovePlayer(spot.teleportTransform);
        //                CameraManager.Instance.FindNewCameras(cam);
        //            }
        //        }
        //    }
        //}

    }

    public IEnumerator UnloadScenesExceptBattle()
    {
        int numOfScenes = SceneManager.sceneCount;
        Scene scene;
        for (int i = 0; i < numOfScenes; i++)
        {
            scene = SceneManager.GetSceneAt(i);
            if (scene.name != battleScene.name)
            {
                yield return SceneManager.UnloadSceneAsync(scene);
            }

        }
    }
}
