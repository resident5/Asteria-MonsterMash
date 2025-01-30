using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectItemQuestStep : QuestStep
{
    private int numOfItemsCollected = 0;

    private int numOfItemsRequired = 5;

    [SerializeField]
    private Item itemToBeCollected;

    private void OnEnable()
    {
        EventManager.Instance.miscEvents.onItemCollectedAmount += Collected;
        Debug.Log("Should be done");
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
            }
        }

        if (numOfItemsCollected >= numOfItemsRequired)
        {
            FinishQuestStep();
        }
    }
}
