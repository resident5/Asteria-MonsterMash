using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Camera Setting", menuName = "Custom/Store camera setting")]
public class CameraSettingsSO : ScriptableObject
{
    public Vector3 camPosition;
    public Vector3 camRotation;
    public Vector3 camScale;
}
