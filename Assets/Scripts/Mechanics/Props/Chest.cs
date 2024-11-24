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
        Debug.Log($"Player got {randomItemAmount} {randomItem.name}{(randomItemAmount > 1 ? "s" : "")}");
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
