// ShopItem.cs
using System.Collections.Generic;
using UnityEngine;

// ShopManager.cs
public class ShopManager : MonoBehaviour
{
    public List<ShopItem> shopItems = new List<ShopItem>();
    public Transform shopItemsParent;
    public ShopSlot[] shopSlots;
    public static ShopManager Instance { get; private set; }

    void Start()
    {
        shopSlots = shopItemsParent.GetComponentsInChildren<ShopSlot>();
        UpdateShop();
    }

    void UpdateShop()
    {
        for (int i = 0; i < shopSlots.Length; i++)
        {
            if (i < shopItems.Count)
            {
                shopSlots[i].AddItem(shopItems[i]);
            }
            else
            {
                shopSlots[i].ClearSlot();
            }
        }
    }

    public void PurchaseItem(Item item)
    {
        ShopItem shopItem = shopItems.Find(x => x.item == item);

        if (shopItem != null)
        {
            // Check if player has enough coins
            if (InventoryManager.Instance.coins >= shopItem.price)
            {
                // Check if shop has stock
                if (shopItem.currentStock != 0)
                {
                    // Add to inventory
                    InventoryManager.Instance.AddItem(item);

                    // Deduct coins
                    InventoryManager.Instance.RemoveCoins(shopItem.price);

                    // Reduce stock if not infinite
                    if (shopItem.currentStock > 0)
                    {
                        shopItem.currentStock--;
                    }

                    UpdateShop();
                }
            }
        }
    }
}

