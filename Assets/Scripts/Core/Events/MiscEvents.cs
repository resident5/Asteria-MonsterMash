using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiscEvents
{
    //public event Action<Item> onItemCollected;

    //public void ItemCollected(Item item)
    //{
    //    if (onItemCollected != null)
    //    {
    //        onItemCollected(item);
    //    }
    //}

    public event Action<Item, int> onItemCollectedAmount;

    public void ItemCollectedAmount(Item item, int num)
    {
        if (onItemCollectedAmount != null)
        {
            onItemCollectedAmount(item, num);
        }
    }

}
