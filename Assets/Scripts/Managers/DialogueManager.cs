using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Naninovel;
using Naninovel.Commands;
using UnityEngine.Rendering.Universal;

public class DialogueManager : MonoBehaviour
{
    private static DialogueManager instance;
    public static DialogueManager Instance
    {
        get
        {
            return instance;
        }
    }

    PlayerController playerController;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        playerController = FindObjectOfType<PlayerController>();
    }


    //THE UI CAMERA IS WHAT HANDLES THE PRINTING TEXT
    //THE MAIN CAMERA HANDLES THE ACTUAL BACKGROUND AND STUFF FROM NANINOVEL

    private async void Start()
    {
        await RuntimeInitializer.Initialize();

        var switchCommand = new SwitchToAdventureMode { ResetState = false };
        await switchCommand.Execute();

        //Debug.Log("Adventure mode");
    }

    public void StartConversation(string script, string labels)
    {
        GameManager.Instance.ChangeState(GameManager.GameState.DIALOGUE);
        playerController.canMove = false;

        ActivateAdventureCameraMode();

        var scriptPlayer = Engine.GetServiceOrErr<IScriptPlayer>();
        scriptPlayer.LoadAndPlayAtLabel(script, labels).Forget();
    }

    public void StartConversation(string script)
    {
        GameManager.Instance.ChangeState(GameManager.GameState.DIALOGUE);

        playerController.canMove = false;

        ActivateAdventureCameraMode();
        var scriptPlayer = Engine.GetServiceOrErr<IScriptPlayer>();
        scriptPlayer.LoadAndPlay(script).Forget();
    }

    private void ActivateAdventureCameraMode()
    {
        var naniCamera = Engine.GetServiceOrErr<ICameraManager>().Camera;
        naniCamera.enabled = false;

        var inputManager = Engine.GetServiceOrErr<IInputManager>();
        inputManager.ProcessInput = true;
    }

    private void ActivateNovelCameraMode()
    {
        var naniCamera = Engine.GetServiceOrErr<ICameraManager>().Camera;
        naniCamera.enabled = false;

        var inputManager = Engine.GetServiceOrErr<IInputManager>();
        inputManager.ProcessInput = true;
    }
}
