using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Windows.WebCam;
using static GameManager;

public class Teleporter : MonoBehaviour, Interactable
{
    [SerializeField]
    Scene nextScene;

    public string nextSceneName;
    public enum SceneMarker
    {
        A, B, C, D, E, F
    }

    //This teleporter's marker
    public SceneMarker marker;

    public Transform teleportTransform;

    //The marker for the next scene where the player will be transported
    public SceneMarker targetMarker;

    public IEnumerator TransitionScenes()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(nextSceneName, LoadSceneMode.Additive);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);

            if (operation.progress >= 0.9f)
            {
            }

            yield return null;
        }
        //Find out how to set cinemachine camera on new scene
        StartCoroutine(SceneController.Instance.UnloadScenesExceptBattle());

        if (operation.isDone)
        {
            nextScene = SceneManager.GetSceneByName(nextSceneName);
            if (nextScene.IsValid())
            {
                Teleporter[] teleporterSpots = FindObjectsOfType<Teleporter>();
                CinemachineVirtualCamera cam = GetNewCinemachineCameraInScene();
                foreach (var spot in teleporterSpots)
                {
                    if (targetMarker == spot.marker)
                    {
                        GameManager.Instance.MovePlayer(spot.teleportTransform);
                        CameraManager.Instance.FindNewCameras(cam);
                    }
                }
            }
        }
    }

    public CinemachineVirtualCamera GetNewCinemachineCameraInScene()
    {
        GameObject[] rootObjectsInNewScene = nextScene.GetRootGameObjects();
        foreach (var item in rootObjectsInNewScene)
        {
            CinemachineVirtualCamera newCam = item.GetComponent<CinemachineVirtualCamera>();
            if (newCam != null)
                return newCam;
        }

        return null;
    }

    public void Interact()
    {
        Debug.Log("Start scene transition");
        StartCoroutine(TransitionScenes());
    }
}
