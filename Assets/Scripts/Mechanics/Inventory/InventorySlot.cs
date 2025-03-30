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

    public int amount;

    private void Start()
    {
        Button btn = GetComponent<Button>();
        item.playerData = GameManager.Instance.playerController.playerData;
        btn.onClick.AddListener(() => 
        {
            item.Use(); 
        });
    }

    public void Remove()
    {

    }
}
