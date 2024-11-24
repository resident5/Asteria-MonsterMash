using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using static UnityEditor.Progress;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;
using static UnityEngine.EventSystems.EventTrigger;
using Newtonsoft.Json.Linq;
using System;

public class DialogueChoiceHolder : MonoBehaviour
{
    public List<DialogueChoice> choices;
    const int MAX_CHOICES = 6;
    public GameObject choiceButtonPrefab;
    public CanvasGroup choiceCanvasGroup;

    void Start()
    {
        choiceCanvasGroup = GetComponent<CanvasGroup>();
        choices = new List<DialogueChoice>();

        //for (int i = 0; i < transform.childCount; i++)
        //{
        //    choices[i] = transform.GetChild(i).GetComponent<DialogueChoice>();
        //}
    }

    //public void CreateChoices(JObject obj)
    //{
    //    int index = 0;

    //    foreach (var item in obj)
    //    {
    //        DialogueChoice choice = choices[index];

    //        choice.id = index;
    //        choice.activated = true;
    //        choice.text.text = (string)item.Key;
    //        choice.nextDialogue = (string)item.Value;

    //        choices[index++] = choice;
    //    }

    //    ShowCanvas();
    //}

    public void CreateChoices(JObject obj)
    {
        int index = 0;

        ResetChoices();

        foreach (var item in obj)
        {
            GameObject choiceObj = Instantiate(choiceButtonPrefab, transform);
            var choice = choiceObj.GetComponent<DialogueChoice>();

            choice.id = index;
            choice.activated = true;
            choice.text.text = (string)item.Key;
            choice.nextDialogue = (string)item.Value;
            choices.Add(choice);

            index++;
        }

        ActivateAllChoices();
    }


    //public void CreateChoices(Dictionary<string, string> list)
    //{
    //    int index = 0;

    //    foreach (var item in list)
    //    {
    //        DialogueChoice choice = choices[index];

    //        choice.id = index;
    //        choice.activated = true;
    //        choice.choiceCanvasGroup.alpha = 1;
    //        choice.text.text = (string)item.Key;
    //        choice.nextDialogue = (string)item.Value;

    //        choices[index++] = choice;
    //    }

    //    ShowCanvas();
    //}

    public void ResetChoices()
    {
        for (int i = 0; i < choices.Count; i++)
        {
            Destroy(choices[i].gameObject);
        }
        choices.Clear();
    }

    public void ActivateAllChoices()
    {
        foreach (var item in choices)
        {
            if (item.activated)
            {
                item.ActivateChoice();
            }
        }
    }

    public void DisableAllChoices()
    {
        foreach (var item in choices)
        {
            item.DeactivateChoice();
        }
    }
}
