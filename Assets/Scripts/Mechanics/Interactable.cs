using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class Interactable: MonoBehaviour
{
    public bool isFocus = false;
    public bool hasInteracted = false;

    public void Update()
    {
        if (isFocus && !hasInteracted)
        {
            Interact();
            hasInteracted = true;
        }
    }

    public virtual void Interact() { }

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

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController playerController = other.GetComponent<PlayerController>();
            playerController.SetFocus(this);
        }
    }


}
