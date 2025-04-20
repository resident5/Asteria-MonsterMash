using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Chest : Interactable
{
    public List<Item> possibleListOfItems;
    public bool isOpened = false;

    [Range(1, 50)]
    public int min, max;
    private int totalAmount = 0;

    public override void Interact()
    {
        if (isOpened)
            return;

        totalAmount = Random.Range(min, max);

        Item randomItem = GetRandomItem();

        InventoryManager.Instance.AddItem(randomItem, totalAmount);

        string openChestInfo = $"Acquired {totalAmount} {randomItem.name}{(totalAmount > 1 ? "s" : "")}";

        EventManager.Instance.miscEvents.ItemCollectedAmount(randomItem, totalAmount);

        isOpened = true;
    }

    private Item GetRandomItem()
    {
        int rand = Random.Range(0, possibleListOfItems.Count);
        Item randomItem = possibleListOfItems[rand];

        return randomItem;
    }

    private void GetItemAmount()
    {

    }
}
