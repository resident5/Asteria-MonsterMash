using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueUI : MonoBehaviour
{
    public DialogueBox left;
    public DialogueBox narration;
    public DialogueBox right;

    public DialogueChoiceHolder dialogueChoiceHolder;
    public GameObject dialogueHolder;

    private DialogueBox activeBox;

    public IEnumerator TypingCouroutine;

    public bool isTyping;
    private string currentText;

    public Image fadeImage;
    public Image endingScreenImage;
    public float fadeDuration;

    private void Awake()
    {
        dialogueHolder.SetActive(false);
        isTyping = false;
    }

    private void Start()
    {
        activeBox = narration;
    }

    public void ChangeText(string text)
    {
        if (activeBox != null)
        {
            StopAllCoroutines();
            string typeText = RemoveTags(text);
            StartCoroutine(TypingCouroutine = TypeOutText(typeText));
            currentText = typeText;
        }
    }

    //IEnumerator TypeOutText(string text)
    //{
    //    activeBox.dialogueText.text = "";
    //    isTyping = true;
    //    foreach (var c in text)
    //    {
    //        activeBox.dialogueText.text += c;
    //        yield return null;
    //        yield return null;
    //        yield return null;
    //    }
    //    isTyping = false;
    //}

    IEnumerator TypeOutText(string text)
    {
        //int index = 0;
        //while (index < text.Length)
        //{
        //    if (text[index] == '<')
        //    {
        //        if (text[index] != '>')
        //    }
        //}
        activeBox.dialogueText.text = "";
        isTyping = true;
        foreach (var c in text)
        {
            activeBox.dialogueText.text += c;
            yield return null;
            yield return null;
            yield return null;
        }
        isTyping = false;
    }

    public string RemoveTags(string text)
    {
        string regexPattern = "<.*?>";
        return Regex.Replace(text, regexPattern, string.Empty);
    }

    public bool SkipOrContinue()
    {
        if (activeBox.dialogueText.text != currentText)
        {
            isTyping = false;
            StopCoroutine(TypingCouroutine);
            activeBox.dialogueText.text = currentText;
            return false;
        }
        else
        {
            StopCoroutine(TypingCouroutine);
            return true;
        }
    }

    //Find a way to keep track of all the characters/sprites/icons without having to foldersearch for them
    //Probably gonna need scriptable objects...

    public void ChangeDialogueBox(bool isNarrator, bool isLeft = true, string VNSpriteName = null)
    {
        activeBox.SetBox(false);
        Sprite vnSprite = GetSpriteFromName(VNSpriteName);

        if (isNarrator)
        {
            activeBox = narration;
        }
        else if (isLeft)
        {
            activeBox = left;
            //activeBox.VNImage.sprite = Sprite;
        }
        else
        {
            activeBox = right;
        }

        activeBox.SetBox(true, vnSprite);
    }

    public Sprite GetSpriteFromName(string spriteName)
    {
        return Resources.Load<Sprite>(spriteName);
    }

    public IEnumerator DisplayImage(Sprite sprite)
    {
        if (!endingScreenImage.isActiveAndEnabled)
        {
            endingScreenImage.sprite = sprite;
            endingScreenImage.gameObject.SetActive(true);
            //endingScreenImage.enabled = true;
        }
        else
        {
            FadeOutEffect();
            yield return new WaitForSeconds(fadeDuration * 2.5f);
            endingScreenImage.sprite = sprite;
            endingScreenImage.enabled = true;
            FadeInEffect();

        }
    }

    public void DisplayOff()
    {
        endingScreenImage.enabled = false;
        fadeImage.canvasRenderer.SetAlpha(0f);
    }

    public void FadeInEffect()
    {
        fadeImage.canvasRenderer.SetAlpha(1.0f);
        fadeImage.CrossFadeAlpha(0.0f, fadeDuration, false);
    }

    public void FadeOutEffect()
    {
        fadeImage.canvasRenderer.SetAlpha(0.0f);
        fadeImage.CrossFadeAlpha(1.0f, fadeDuration, false);
    }

    public void ActivateDialogueHolder(bool on)
    {
        dialogueHolder.SetActive(on);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
