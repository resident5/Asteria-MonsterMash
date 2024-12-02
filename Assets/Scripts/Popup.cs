using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DentedPixel;

public class Popup : MonoBehaviour
{
    [SerializeField]
    private float delay;
    
    public Image popupIcon;
    public TMP_Text popupText;
    public bool isActive;

    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void ShowPopupImage(Sprite sprite, string text)
    {
        if (sprite != null)
        {
            popupIcon.gameObject.SetActive(true);
            popupIcon.sprite = sprite;
        }

        popupText.text = text;
        StartCoroutine(OpenPopup());
    }

    public void ShowPopup(Sprite sprite, string text)
    {
        popupIcon.gameObject.SetActive(false);
        popupText.text = text;
        StartCoroutine(OpenPopup());
    }

    public void PlayAnimation(string animName)
    {
        anim.Play(animName);
    }

    public IEnumerator OpenPopup()
    {
        PlayAnimation("PopupOpen");

        yield return new WaitForSeconds(1.3f);

        PlayAnimation("PopupClose");

        //LeanTween.moveLocalX(gameObject, 0, 0.7f).setEaseInQuart();

        //yield return new WaitForSeconds(1.2f);

        //LeanTween.moveLocalX(gameObject, -810, 0.7f).setEaseInQuart();

        //yield return new WaitForSeconds(.3f);

        //transform.localPosition = new Vector3(startPositionX, transform.localPosition.y);

        //    LeanTween.move(gameObject, transform.localPosition +
        //new Vector3(50, transform.localPosition.y, transform.localPosition.z), 0.02f).setEase(LeanTweenType.easeInBounce);

    }

    //public void ClosePopup()
    //{
    //    isActive = false;
    //    //LeanTween.move(gameObject, transform.localPosition +
    //    //    new Vector3(50, transform.localPosition.y, transform.localPosition.z), 0.02f).setEase(LeanTweenType.easeInBounce);
    //}
}
