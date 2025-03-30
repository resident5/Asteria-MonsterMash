using Naninovel.Commands;
using Naninovel;
using UnityEngine;
using UnityEngine.Rendering.Universal;

[CommandAlias("adventure")]
public class SwitchToAdventureMode : Command
{
    [ParameterAlias("reset")]
    public BooleanParameter ResetState = true;

    public override async UniTask Execute(AsyncToken token = default)
    {
        // 1. Disable Naninovel input.
        var inputManager = Engine.GetServiceOrErr<IInputManager>();
        inputManager.ProcessInput = true;

        // 2. Stop script player.
        var scriptPlayer = Engine.GetServiceOrErr<IScriptPlayer>();
        scriptPlayer.Stop();

        // 3. Hide text printer.
        var hidePrinter = new HidePrinter();
        hidePrinter.Execute(token).Forget();

        // 4. Reset state (if required).
        if (ResetState)
        {
            var stateManager = Engine.GetServiceOrErr<IStateManager>();
            await stateManager.ResetState();
        }

        // 5. Switch cameras.
        var mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        var naniCamera = Engine.GetServiceOrErr<ICameraManager>().Camera;
        var uiCamera = Engine.GetServiceOrErr<ICameraManager>().UICamera;

        mainCamera.enabled = true;
        naniCamera.enabled = false;

        var mainCameraData = mainCamera.GetUniversalAdditionalCameraData();
        if (!mainCameraData.cameraStack.Contains(uiCamera))
            mainCameraData.cameraStack.Add(uiCamera);

        // 6. Return player control
        var controller = Object.FindObjectOfType<PlayerController>();
        controller.canMove = true;

    }
}
