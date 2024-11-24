using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class InventoryManager : Singleton<InventoryManager>
{
    public List<Item> inventoryItems;
    public List<InventorySlot> inventorySlots;
    public HUDController controller;
    public GameObject inventorySlotPrefab;

    private void Start()
    {
        controller = GameManager.Instance.hudController;
    }

    private void Update()
    {
        
    }

    public void AddItem(Item item, int amount = 1)
    {
        //If item already exists AND the item can stack increment the item by 1

        if (inventoryItems.Contains(item))
        {
            int index = inventoryItems.IndexOf(item);
            //inventoryItems[index].amount += amount;

            foreach (InventorySlot iSlot in inventorySlots)
            {
                if (iSlot.item = item)
                {
                    iSlot.amount += amount;
                    iSlot.nameText.text = ParseName(item.name, iSlot.amount);
                }
            }
            return;
        }

        GameObject obj = Instantiate(inventorySlotPrefab, controller.inventoryHolder);
        InventorySlot slot = obj.GetComponent<InventorySlot>();

        slot.item = item;
        slot.amount = amount;
        slot.nameText.text = ParseName(item.name, slot.amount);
        slot.descText.text = ParseDescription(item.description, (Consumable)item);
        slot.icon.sprite = item.spriteIcon;
        inventoryItems.Add(item);
        inventorySlots.Add(slot);

        //Auto Sort list
    }

    public string ParseDescription(string desc, Consumable consumable)
    {
        return desc.Replace("{value}", "" + (consumable.effectType == Consumable.EffectType.ModHealthPercent ||
            consumable.effectType == Consumable.EffectType.ModManaPercent ||
            consumable.effectType == Consumable.EffectType.ModLustPercent
            ? consumable.percentageValueModifier : consumable.flatValueModifier));
    }

    public string ParseName(string name, int amount)
    {
        return name.Replace("{value}", "" + amount);
    }

    public void RemoveItem(Item item)
    {
        if (inventorySlots.Count > 0)
        {
            foreach (InventorySlot iSlot in inventorySlots)
            {
                if (iSlot.item = item)
                {
                    if (iSlot.amount > 1)
                    {
                        iSlot.amount -= 1;
                        iSlot.nameText.text = ParseName(item.name, iSlot.amount);
                    }
                    else
                    {
                        inventoryItems.Remove(item);
                        inventorySlots.Remove(iSlot);
                        Destroy(iSlot.gameObject);
                        return;
                    }
                }
            }
        }
    }
    
    
}
