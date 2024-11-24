using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueChoice : MonoBehaviour
{
    public int id;
    public TMP_Text text;
    public Button button;
    public CanvasGroup choiceCanvasGroup;
    public bool activated;
    public string nextDialogue;

    public DialogueChoice(DictionaryEntry entry)
    {
        activated = true;
        text.text = (string)entry.Key;
        nextDialogue = (string)entry.Value;
    }

    private void Start()
    {
        text = GetComponentInChildren<TMP_Text>();
        button = GetComponent<Button>();
        choiceCanvasGroup = GetComponent<CanvasGroup>();

        //button.onClick.AddListener(() => CallDialogue(nextDialogue));
        button.onClick.AddListener(() => {
            CallDialogue(nextDialogue);
            transform.parent.GetComponent<DialogueChoiceHolder>().DisableAllChoices();
        });
    }

    public void ChangeChoiceText(string choiceText)
    {
        text.text = choiceText;
    }

    public void CallDialogue(string dialogue)
    {
        StopAllCoroutines();
        Debug.Log("Call next dialogue");
        StartCoroutine(DialogueSystem.Instance.StartDialogue(dialogue));
    }

    public void ActivateChoice()
    {
        activated = true;
        choiceCanvasGroup.alpha = 1f;
        choiceCanvasGroup.interactable = true;
        choiceCanvasGroup.blocksRaycasts = true;
    }

    public void DeactivateChoice()
    {
        activated = false;
        choiceCanvasGroup.alpha = 0f;
        choiceCanvasGroup.interactable = false;
        choiceCanvasGroup.blocksRaycasts = false;
    }


}
