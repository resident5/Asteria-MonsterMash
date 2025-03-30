using Naninovel.Commands;
using Naninovel;
using UnityEngine;
using UnityEngine.Rendering.Universal;

[CommandAlias("cgmode")]
public class SwitchToCGMode : Command
{
    [ParameterAlias("reset")]
    public BooleanParameter ResetState = false;

    [ParameterAlias("cgReset")]
    public BooleanParameter cgReset = true;

    public override async UniTask Execute(AsyncToken token = default)
    {
        if (ResetState)
        {
            var stateManager = Engine.GetServiceOrErr<IStateManager>();
            await stateManager.ResetState();
        }

        // 5. Switch cameras.
        var mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        var naniCamera = Engine.GetServiceOrErr<ICameraManager>().Camera;
        var uiCamera = Engine.GetServiceOrErr<ICameraManager>().UICamera;

        naniCamera.enabled = cgReset;

        var mainCameraData = mainCamera.GetUniversalAdditionalCameraData();
        var naniCameraData = naniCamera.GetUniversalAdditionalCameraData();

        if(mainCameraData.cameraStack.Contains(uiCamera) == cgReset)
        {
            mainCameraData.cameraStack.Remove(uiCamera);
        }
        else
        {
            mainCameraData.cameraStack.Add(uiCamera);
        }

        if(!naniCameraData.cameraStack.Contains(uiCamera) == cgReset)
        {
            naniCameraData.cameraStack.Add(uiCamera);
        }
        else
        {
            naniCameraData.cameraStack.Remove(uiCamera);
        }

        //if (mainCameraData.cameraStack.Contains(uiCamera))
        //    mainCameraData.cameraStack.Remove(uiCamera);

        //if (!naniCameraData.cameraStack.Contains(uiCamera))
        //    naniCameraData.cameraStack.Add(uiCamera);

        mainCamera.enabled = !cgReset;


    }
}
