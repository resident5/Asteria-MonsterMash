using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverworldEvents
{
    public event Action onEnterNewScene;

    public void EnterNewScene()
    {
        if (onEnterNewScene != null)
        {
            onEnterNewScene();
        }
    }

    public event Action<string, Sprite> onReceivedItem;

    public void ReceivedItem(string info, Sprite sprite = null)
    {
        if (onReceivedItem != null)
        {
            onReceivedItem(info, sprite);
        }
    }
}
