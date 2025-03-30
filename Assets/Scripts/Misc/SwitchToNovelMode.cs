using Naninovel.Commands;
using Naninovel;
using UnityEngine;
using UnityEngine.Rendering.Universal;

[CommandAlias("novel")]
public class SwitchToNovelMode : Command
{
    public StringParameter ScriptName;
    public StringParameter Label;

    public override async UniTask Execute(AsyncToken token = default)
    {
        // 1. Disable character control.
        var controller = Object.FindObjectOfType<PlayerController>();
        controller.canMove = false;

        // 2. Switch cameras.
        //var mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        //mainCamera.enabled = false;
        //var naniCamera = Engine.GetServiceOrErr<ICameraManager>().Camera;
        //naniCamera.enabled = true;

        var mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        var naniCamera = Engine.GetServiceOrErr<ICameraManager>().Camera;
        var uiCamera = Engine.GetServiceOrErr<ICameraManager>().UICamera;

        mainCamera.enabled = false;
        naniCamera.enabled = true;

        var naniCameraData = naniCamera.GetUniversalAdditionalCameraData();
        if (!naniCameraData.cameraStack.Contains(uiCamera))
            naniCameraData.cameraStack.Add(uiCamera);

        // 3. Load and play specified script (is required).
        if (Assigned(ScriptName))
        {
            var scriptPlayer = Engine.GetServiceOrErr<IScriptPlayer>();
            await scriptPlayer.LoadAndPlayAtLabel(ScriptName, Label);
        }

        // 4. Enable Naninovel input.
        var inputManager = Engine.GetServiceOrErr<IInputManager>();
        inputManager.ProcessInput = true;
    }
}
