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


[CreateAssetMenu(fileName = "New Consumable", menuName = "Inventory/Consumable Item")]
public class ConsumableItem : Item  // Inherits from your abstract Item class
{
    public int hungerRestore;  // Example unique property
}

/*using UnityEngine;

[System.Serializable]
public class Item
{
    public string itemName;
    public Sprite icon;
    // Add any other properties you need
}*/