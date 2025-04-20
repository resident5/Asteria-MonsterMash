using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Item item;

    public TMP_Text nameText;
    public TMP_Text descText;
    public Image icon;

    private int amount;
    public int Amount
    {
        get => amount;
        set => amount = Mathf.Clamp(value, 0, item.maxAmount);
    }

    //private void Start()
    //{
    //    Button btn = GetComponent<Button>();
    //    if (item is ConsumableItem useableItem)
    //    {
    //        btn.onClick.AddListener(() =>
    //        {
    //            item.Use(HUDController.Instance.itemTargetStats);
    //        });
    //    }
    //}

    public void UseItem()
    {
        Debug.Log($"Use item {item.name}");
        //item.Use(HUDController.Instance.itemTargetStats);

    }

    public void Remove()
    {

    }
}
