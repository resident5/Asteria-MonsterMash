using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public List<Item> inventoryItems;
    public List<InventorySlot> inventorySlots;
    public HUDController controller;
    public GameObject inventorySlotPrefab;

    private static InventoryManager instance;
    public static InventoryManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindAnyObjectByType<InventoryManager>();
            }
            return instance;
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        controller = GameManager.Instance.hudController;
    }

    public bool HasItem(Item item) => inventoryItems.Contains(item);

    public void AddItem(Item item, int amount = 1)
    {
        //If item already exists AND the item can stack increment the item by 1

        if (inventoryItems.Contains(item))
        {
            int index = inventoryItems.IndexOf(item);
            //inventoryItems[index].amount += amount;

            foreach (InventorySlot iSlot in inventorySlots)
            {
                if (iSlot.item == item)
                {
                    //Maybe give a warning that there's too much orrrrrr start stockpiling another
                    iSlot.Amount += amount;
                    iSlot.nameText.text = ParseName(item.name, iSlot.Amount);
                }
            }
            EventManager.Instance.overworldEvents.ReceivedItem(item.description, item.spriteIcon);

            return;
        }

        GameObject obj = Instantiate(inventorySlotPrefab, controller.inventoryHolder);
        InventorySlot slot = obj.GetComponent<InventorySlot>();

        slot.item = item;
        slot.Amount = amount;
        slot.nameText.text = ParseName(item.name, slot.Amount);
        slot.descText.text = ParseDescription(item.description, item);
        slot.icon.sprite = item.spriteIcon;
        inventoryItems.Add(item);
        inventorySlots.Add(slot);

        EventManager.Instance.overworldEvents.ReceivedItem(item.description, item.spriteIcon);

        //Auto Sort list
    }

    private string ParseDescription(string desc, Item item)
    {
        if (item is ConsumableItem consumable)
        {
            return desc.Replace("{value}", "" + (consumable.effectType == ConsumableItem.EffectType.ModHealthPercent ||
                consumable.effectType == ConsumableItem.EffectType.ModManaPercent ||
                consumable.effectType == ConsumableItem.EffectType.ModLustPercent
                ? consumable.percentageValueModifier : consumable.flatValueModifier));
        }

        return desc.Replace("{value}", "");
    }

    private string ParseName(string name, int amount)
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
                    if (iSlot.Amount > 1)
                    {
                        iSlot.Amount -= 1;
                        iSlot.nameText.text = ParseName(item.name, iSlot.Amount);
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
