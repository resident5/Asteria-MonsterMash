using Codice.Client.BaseCommands;
using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : Singleton<SceneController>
{
    public Scene battleScene;

    private void Start()
    {
        battleScene = SceneManager.GetSceneByName("Battle");
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
