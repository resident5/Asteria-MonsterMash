using Naninovel;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnItemQuestStep : QuestStep
{
    public Item requiredItem;

    public string npcId;

    [SerializeField] private string variableName;
    [SerializeField] private bool variableValue;

    public bool finishedSpeakingWithNPC = false;

    private void OnEnable()
    {
        EventManager.Instance.questEvents.onInteractWithNPC += SpeakToNPC;
    }

    private void OnDisable()
    {
        EventManager.Instance.questEvents.onInteractWithNPC -= SpeakToNPC;
    }

    public void SpeakToNPC(NPCData npcData)
    {
        //TODO: This will be a problem if we have multiple quests from the same NPC
        if (npcData.npcId == npcId && InventoryManager.Instance.HasItem(requiredItem))
        {
            UpdateState();
            GameManager.Instance.variableManager.SetVariable(variableName, variableValue);
            finishedSpeakingWithNPC = true;
        }

        if (finishedSpeakingWithNPC)
            FinishQuestStep();
    }

    private void UpdateState()
    {
        string state = finishedSpeakingWithNPC.ToString();
        ChangeState(state);
    }

    protected override void SetQuestStepState(string state)
    {
        this.finishedSpeakingWithNPC = System.Boolean.Parse(state);
        UpdateState();
    }
}
