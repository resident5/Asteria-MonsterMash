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
        playerController.canMove = false;

        ActivateAdventureCameraMode();

        var scriptPlayer = Engine.GetServiceOrErr<IScriptPlayer>();
        scriptPlayer.LoadAndPlayAtLabel(script, labels).Forget();
    }

    public void StartConversation(string script)
    {
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

//[CommandAlias("adventure")]
//public class SwitchToAdventureMode : Command
//{
//    [ParameterAlias("reset")]
//    public BooleanParameter ResetState = true;

//    public override async UniTask Execute(AsyncToken token = default)
//    {
//        // 1. Disable Naninovel input.
//        var inputManager = Engine.GetServiceOrErr<IInputManager>();
//        inputManager.ProcessInput = false;

//        // 2. Stop script player.
//        var scriptPlayer = Engine.GetServiceOrErr<IScriptPlayer>();
//        scriptPlayer.Stop();

//        // 3. Hide text printer.
//        var hidePrinter = new HidePrinter();
//        hidePrinter.Execute(token).Forget();

//        // 4. Reset state (if required).
//        if (ResetState)
//        {
//            var stateManager = Engine.GetServiceOrErr<IStateManager>();
//            await stateManager.ResetState();
//        }

//        // 5. Switch cameras.
//        var advCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
//        advCamera.enabled = true;

//        var naniCamera = Engine.GetServiceOrErr<ICameraManager>().Camera;
//        naniCamera.enabled = false;

//        // 6. Enable character control.
//        var controller = Object.FindObjectOfType<PlayerController>();
//        controller.canMove = true;
//    }
//}

//[CommandAlias("novel")]
//public class SwitchToNovelMode : Command
//{
//    public StringParameter ScriptName;
//    public StringParameter Label;

//    public override async UniTask Execute(AsyncToken token = default)
//    {
//        // 1. Disable character control.
//        var controller = Object.FindObjectOfType<PlayerController>();
//        controller.canMove = false;

//        // 2. Switch cameras.
//        var advCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
//        advCamera.enabled = false;
//        var naniCamera = Engine.GetServiceOrErr<ICameraManager>().Camera;
//        naniCamera.enabled = true;

//        // 3. Load and play specified script (is required).
//        if (Assigned(ScriptName))
//        {
//            var scriptPlayer = Engine.GetServiceOrErr<IScriptPlayer>();
//            await scriptPlayer.LoadAndPlayAtLabel(ScriptName, Label);
//        }

//        // 4. Enable Naninovel input.
//        var inputManager = Engine.GetServiceOrErr<IInputManager>();
//        inputManager.ProcessInput = true;
//    }
//}
