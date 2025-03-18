using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using JetBrains.Annotations;

public class CameraManager : Singleton<CameraManager>
{
    public List<CinemachineVirtualCamera> cameras;

    public CinemachineVirtualCamera overWorldCamera;
    public CinemachineVirtualCamera battleCamera;

    public CinemachineVirtualCamera startCamera;
    private CinemachineVirtualCamera currentCamera;

    CinemachineBrain brain;

    protected override void Awake()
    {
        base.Awake();
        GameObject cameraObj = GameObject.FindGameObjectWithTag("MainCamera");
        currentCamera = startCamera;
        if (currentCamera == null)
        {
            currentCamera = FindObjectOfType<CinemachineVirtualCamera>();
        }
    }

    private void Start()
    {
        currentCamera.Follow = GameManager.Instance.playerData.transform;
        CameraRefresh();
    }

    void CameraRefresh()
    {
        for (int i = 0; i < cameras.Count; i++)
        {
            if (cameras[i] != null)
            {
                if (cameras[i] == currentCamera)
                {
                    cameras[i].Priority = 20;
                }
                else
                {
                    cameras[i].Priority = 10;
                }
            }
        }
    }

    public void SetBattleCamera(CinemachineVirtualCamera battleCam)
    {
        if (battleCam == null)
            return;

        battleCamera = battleCam;

        if (!cameras.Contains(battleCam))
        {
            cameras.Add(battleCam);
        }
    }

    public void FindNewCameras(CinemachineVirtualCamera newCamera)
    {
        if (newCamera != null)
            currentCamera = newCamera;
        currentCamera.Follow = GameManager.Instance.playerData.transform;
    }

    public void ActivateBattleCamera()
    {
        currentCamera = battleCamera;
        CameraRefresh();
    }

    public void DeactivateBattleCamera()
    {
        currentCamera = overWorldCamera;
        CameraRefresh();
    }

    public void SwapToOverWorldCam()
    {
        overWorldCamera.Priority = 20;
        battleCamera.Priority = 1;
    }

    public void SwapToBattleCam()
    {
        overWorldCamera.Priority = 1;
        battleCamera.Priority = 20;

    }
}
