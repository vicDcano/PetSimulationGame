// Item.cs - Base class for all items
using UnityEngine;

public abstract class Item : ScriptableObject
{
    public string itemName;
    public Sprite icon;
    public string description;
    public int basePrice;
    public bool isStackable;
    public bool isUnlimited; // For items like apples, beds that don't deplete
}

// InventoryItem.cs - Represents an item in inventory
[System.Serializable]
public class InventoryItem
{
    public Item item;
    public int quantity;

    public InventoryItem(Item item, int quantity)
    {
        this.item = item;
        this.quantity = quantity;
    }
}