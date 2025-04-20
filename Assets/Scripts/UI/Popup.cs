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

        yield return null;

        yield return new WaitUntil(() => InputManager.Instance.Interacted);

        PlayAnimation("PopupClose");
    }

    //public void ClosePopup()
    //{
    //    isActive = false;
    //    //LeanTween.move(gameObject, transform.localPosition +
    //    //    new Vector3(50, transform.localPosition.y, transform.localPosition.z), 0.02f).setEase(LeanTweenType.easeInBounce);
    //}
}
