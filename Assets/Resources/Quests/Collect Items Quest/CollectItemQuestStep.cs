using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectItemQuestStep : QuestStep
{
    //Quest step for collecting items (From Ground or Chests)

    private int numOfItemsCollected = 0;

    private int numOfItemsRequired = 5;

    [SerializeField]
    private Item itemToBeCollected;

    private void OnEnable()
    {
        EventManager.Instance.miscEvents.onItemCollectedAmount += Collected;
    }

    private void OnDisable()
    {
        EventManager.Instance.miscEvents.onItemCollectedAmount -= Collected;
    }

    private void Collected(Item item, int num)
    {
        if (item != null && itemToBeCollected == item)
        {
            if (numOfItemsCollected < numOfItemsRequired)
            {
                numOfItemsCollected += num;
                UpdateState();
            }
        }

        if (numOfItemsCollected >= numOfItemsRequired)
        {
            FinishQuestStep();
        }
    }

    private void UpdateState()
    {
        string state = numOfItemsCollected.ToString();
        ChangeState(state);
    }

    protected override void SetQuestStepState(string state)
    {
        this.numOfItemsCollected = System.Int32.Parse(state);
        UpdateState();
    }
}
