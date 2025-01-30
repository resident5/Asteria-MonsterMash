using System.Collections;
using UnityEngine;


public class Quest
{
    public QuestInfoSO info;

    public QuestState state;
    private int currentQuestStepIndex;

    public Quest(QuestInfoSO questInfo)
    {
        this.info = questInfo;
        this.state = QuestState.REQUIREMENTS_NOT_MET;
        this.currentQuestStepIndex = 0;
    }

    public void MoveToNextStep()
    {
        currentQuestStepIndex++;
    }

    public bool CurrentStepExists()
    {
        return (currentQuestStepIndex < info.questStepPrefabs.Length);
    }

    public void InstantiateCurrentQuestStep(Transform parentTransform)
    {
        GameObject questStepPrefab = GetCurrentQuestStepPrefab();
        if (questStepPrefab != null)
        {
            Object.Instantiate<GameObject>(questStepPrefab, parentTransform);
        }
    }

    private GameObject GetCurrentQuestStepPrefab()
    {
        GameObject questStepPrefab = null;
        if(CurrentStepExists())
        {
            questStepPrefab = info.questStepPrefabs[currentQuestStepIndex];
        }
        else
        {
            Debug.LogError("Tried to get quest step prefab but stepIndex is out of range");
        }
        return questStepPrefab;
    }

    public override string ToString()
    {
        string log = $"Name: {info.displayName}\nLevel Requirement: {info.levelRequirement}\nCurrent State: {state}\nCurrent Step Exists: {CurrentStepExists()}";
        return log;
    }
}
