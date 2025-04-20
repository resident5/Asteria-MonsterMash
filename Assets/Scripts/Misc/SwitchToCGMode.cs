using Naninovel.Commands;
using Naninovel;
using UnityEngine;
using UnityEngine.Rendering.Universal;

[CommandAlias("cgmode")]
public class SwitchToCGMode : Command
{
    [ParameterAlias("showCG")]
    public BooleanParameter showCG = true;

    public override UniTask Execute(AsyncToken token = default)
    {
        var mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        var naniCamera = Engine.GetServiceOrErr<ICameraManager>().Camera;
        var uiCamera = Engine.GetServiceOrErr<ICameraManager>().UICamera;

        naniCamera.enabled = showCG;

        var mainCameraData = mainCamera.GetUniversalAdditionalCameraData();
        var naniCameraData = naniCamera.GetUniversalAdditionalCameraData();

        //To show CG the nani camera needs to be on and the main camera needs to be off to avoid issues
        //To show the text printer the ui camera needs to be on the highest most camera (ie the CG Camera/Main Camera)

        if (showCG)
        {
            if (mainCameraData.cameraStack.Contains(uiCamera))
                mainCameraData.cameraStack.Remove(uiCamera);

            if (!naniCameraData.cameraStack.Contains(uiCamera))
                mainCameraData.cameraStack.Add(uiCamera);
        }
        else
        {
            if (!mainCameraData.cameraStack.Contains(uiCamera))
                mainCameraData.cameraStack.Add(uiCamera);

            if (naniCameraData.cameraStack.Contains(uiCamera))
                naniCameraData.cameraStack.Remove(uiCamera);
        }

        mainCamera.enabled = !showCG;

        return UniTask.CompletedTask;
    }
}
