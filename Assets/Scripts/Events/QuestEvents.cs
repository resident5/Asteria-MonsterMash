using UnityEngine;
using System.Collections;
using System;


public class QuestEvents : MonoBehaviour
{
    public event Action<string> onStartQuest;

    public void StartQuest(string id)
    {
        if(onStartQuest != null)
        {
            onStartQuest(id);
        }   
    }

    public event Action<string> onAdvanceQuest;

    public void AdvanceQuest(string id)
    {
        if (onAdvanceQuest != null)
        {
            onAdvanceQuest(id);
        }
    }

    public event Action<string> onFinishQuest;

    public void FinishQuest(string id)
    {
        if (onFinishQuest != null)
        {
            onFinishQuest(id);
        }
    }

    public event Action<Quest> onQuestStateChanged;

    public void QuestStateChange(Quest quest)
    {
        if (onQuestStateChanged != null)
        {
            onQuestStateChanged(quest);
        }
    }

    public event Action<string, int, QuestStepState> onQuestStepStateChanged;

    public void QuestStateStepChange(string id, int questStepIndex, QuestStepState questStepState)
    {
        if (onQuestStepStateChanged != null)
        {
            onQuestStepStateChanged(id, questStepIndex, questStepState);
        }
    }
}
