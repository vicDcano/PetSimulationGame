using UnityEngine;

[System.Serializable]
public class ShopItem
{
    public ConsumableItem item;  // References your ConsumableItem ScriptableObject
    public int price;
    public int currentStock = -1;

    // Helper property for UI
    public string StockText => currentStock < 0 ? "∞" : currentStock.ToString();
}