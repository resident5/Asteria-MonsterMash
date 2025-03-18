using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class QuestStep : MonoBehaviour
{
    private bool isFinished = false;
    private string questID;
    private int stepIndex;

    public void InitializeQuestStep(string questID, int stepIndex, string questStepState)
    {
        this.questID = questID;
        this.stepIndex = stepIndex;
        if(questStepState != null && questStepState != "")
        {
            SetQuestStepState(questStepState);
        }
    }

    protected void FinishQuestStep()
    {
        if (!isFinished)
        {
            isFinished = true;

            EventManager.Instance.questEvents.AdvanceQuest(questID);

            Destroy(this.gameObject);
        }
    }

    protected void ChangeState(string newState)
    {
        EventManager.Instance.questEvents.QuestStateStepChange(questID, stepIndex, new QuestStepState(newState));
    }

    protected abstract void SetQuestStepState(string state);

    //Make update step from the coin collection quest an abstract method that SHOULD be in every quest step
}