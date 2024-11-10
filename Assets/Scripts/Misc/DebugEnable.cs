using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugEnable : MonoBehaviour
{
    private void OnEnable()
    {
        Debug.Log("Turned on");
    }

    private void OnDisable()
    {
        Debug.Log("Turned off");
    }
}
