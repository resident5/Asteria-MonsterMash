using Cinemachine;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Teleporter : MonoBehaviour
{
    public string nextSceneName;
    public enum SceneMarker
    {
        A, B, C, D, E, F
    }

    //This teleporter's marker
    public Transform teleportTransform;

    /// <summary>
    /// The ID for this teleporter
    /// </summary>
    public SceneMarker teleportID;

    /// <summary>
    /// The teleport ID for the teleporter in the next scene
    /// </summary>
    public SceneMarker targetTeleportID;

    public IEnumerator TransitionScenes()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(nextSceneName, LoadSceneMode.Additive);

        if (operation != null)
        {
            while (!operation .isDone)
            {
                float progress = Mathf.Clamp01(operation.progress / .9f);

                HUDController.Instance.fadeCanvas.alpha = progress;

                if (operation.progress >= 0.9f)
                {
                }

                yield return null;
            }
            //Find out how to set cinemachine camera on new scene


            if (operation.isDone)
            {
                Scene nextScene = SceneManager.GetSceneByName(nextSceneName);
                if (nextScene.IsValid())
                {
                    Transform teleporterParentTransform = GetRootTeleporterObject(nextScene, transform.parent.name);
                    Teleporter teleporter = GetNextTeleport(teleporterParentTransform);

                    CinemachineVirtualCamera cam = GetNewCinemachineCameraInScene(nextScene);

                    if (teleporter != null)
                    {
                        GameManager.Instance.MovePlayer(teleporter.teleportTransform);
                        CameraManager.Instance.FindNewCameras(cam);
                    }
                }
                yield return null;
                HUDController.Instance.FadeOut();

                StartCoroutine(SceneController.Instance.UnloadScenesExceptBattle());
            }
        }

    }

    public Transform GetRootTeleporterObject(Scene loadedScene, string objectName)
    {
        GameObject[] sceneRootObjects = loadedScene.GetRootGameObjects();
        foreach (var obj in sceneRootObjects)
        {
            if (obj.name.Contains("ENVIRONMENT"))
            {
                foreach (Transform child in obj.transform)
                {
                    if(child.name == objectName)
                        return child;
                }
            }
        }

        return null;
    }

    public Teleporter GetNextTeleport(Transform teleporterParent)
    {
        foreach (Transform child in teleporterParent)
        {
            Teleporter tele = child.GetComponent<Teleporter>();
            if (targetTeleportID == tele.teleportID)
            {
                return tele;
            }
        }
        return null;
    }

    public CinemachineVirtualCamera GetNewCinemachineCameraInScene(Scene nextScene)
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
            Interact();
    }

    public void Interact()
    {
        StartCoroutine(TransitionScenes());
    }
}
