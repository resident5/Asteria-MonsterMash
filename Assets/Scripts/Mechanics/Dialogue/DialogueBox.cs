using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DialogueBox : MonoBehaviour
{
    public TMP_Text nameText;
    public TMP_Text dialogueText;
    public Image charImage;
    public Image VNImage;

    public bool isNarration;

    public void SetBox(bool on, Sprite vnSprite = null)
    {
        gameObject.SetActive(on);

        if (!isNarration)
        {
            if (VNImage.sprite != null)
            {
                //VNImage.sprite = vnSprite;
                VNImage.gameObject.SetActive(on);
            }
        }
        //charImage.gameObject.SetActive(on);
    }

    public void ActivateVNImage()
    {
        if(!VNImage.isActiveAndEnabled)
        {
            VNImage.gameObject.SetActive(true);
        }
        else
        {
            //VNImage is already on and should animate to show the person is talking
        }
    }

    public void DeactivateVNImage()
    {
        if (VNImage.isActiveAndEnabled)
        {
            VNImage.gameObject.SetActive(false);
        }
        else
        {
            //VNImage is already on and should animate to show the person is talking
        }
    }
}
