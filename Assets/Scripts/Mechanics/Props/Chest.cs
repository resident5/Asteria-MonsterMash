using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Chest : MonoBehaviour, Interactable
{
    public List<Item> possibleListOfItems;
    public bool isOpened = false;
    
    [Range(1, 50)]
    public int min, max;

    public void Interact()
    {
        if (isOpened)
            return;

        Item randomItem = GetRandomItem();
        int randomItemAmount = Random.Range(min, max);

        InventoryManager.Instance.AddItem(randomItem, randomItemAmount);
        //Display Message System
        string openChestInfo = $"Acquired {randomItemAmount} {randomItem.name}{(randomItemAmount > 1 ? "s" : "")}";
        
        HUDController.Instance.DisplayPopupInfo(randomItem.spriteIcon, openChestInfo);
        Debug.Log("OPENED CHEST");
        
        isOpened = true;
    }

    private Item GetRandomItem()
    {
        int rand = Random.Range(0, possibleListOfItems.Count);
        return possibleListOfItems[rand];
    }

    private void GetItemAmount()
    {

    }
}
