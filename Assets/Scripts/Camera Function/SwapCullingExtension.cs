using Cinemachine;
using Naninovel;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapCullingExtension : CinemachineExtension
{
    public LayerMask cullingMask;
    private Camera mainCamera;

    protected override void PostPipelineStageCallback(CinemachineVirtualCameraBase vcam, CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
    {
        if (stage == CinemachineCore.Stage.Finalize)
        {
            CinemachineBrain brain = CinemachineCore.Instance.GetActiveBrain(0);
            if (brain != null)
            {
                CinemachineVirtualCamera currentVcam = brain.ActiveVirtualCamera as CinemachineVirtualCamera;
                if (currentVcam == vcam)
                {
                    if (mainCamera != null)
                        mainCamera.cullingMask = cullingMask;
                }
            }
        }
    }

    private void Init()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    private new void OnEnable()
    {
        Engine.OnInitializationFinished += Init;
    }

    private void OnDisable()
    {
        Engine.OnInitializationFinished -= Init;
    }
}
