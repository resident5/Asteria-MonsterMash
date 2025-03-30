using JetBrains.Annotations;
using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    new public string name = "New Item";
    public Sprite spriteIcon = null;
    public string description;

    public bool canBeThrown = true;

    //TODO: Change this to accept any target (player, enemy, monster, npc?)
    public PlayerData playerData;

    public virtual void Use()
    {
    }

    public void RemoveFromInventory()
    {
        InventoryManager.Instance.RemoveItem(this);
    }
}
