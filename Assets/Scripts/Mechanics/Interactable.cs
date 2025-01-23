using System;
using System.Collections;
using UnityEngine;

public class Interactable: MonoBehaviour
{
    public bool isFocus = false;
    public bool hasInteracted = false;

    public void Update()
    {
        if (isFocus && !hasInteracted)
        {
            Debug.Log("INTERACT");
            Interact();
            hasInteracted = true;
        }
    }

    public virtual void Interact()
    {

    }

    public void DeFocused()
    {
        isFocus = false;
        hasInteracted = false;
    }

    public void Focused()
    {
        isFocus = true;
        hasInteracted = false;
    }


}
