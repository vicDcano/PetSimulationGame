using UnityEngine;
using System.Collections.Generic;

public class ShopManager : MonoBehaviour
{
    public List<ShopItem> shopItems = new List<ShopItem>();
    public Transform shopItemsParent;
    public ShopSlot[] shopSlots;
    public static ShopManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        shopSlots = shopItemsParent.GetComponentsInChildren<ShopSlot>();
        UpdateShop();
    }

    void UpdateShop()
    {
        var slots = shopItemsParent.GetComponentsInChildren<ShopSlot>();
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < shopItems.Count)
                slots[i].AddItem(shopItems[i]);
            else
                slots[i].gameObject.SetActive(false); // Hide unused slots
        }
    }

    public void PurchaseItem(Item item)
    {
        ShopItem shopItem = shopItems.Find(x => x.item == item);

        if (shopItem != null)
        {
            // Check if player has enough coins
            if (CoinManager.Instance.HasEnoughCoins(shopItem.price))
            {
                // Check if shop has stock
                if (shopItem.currentStock != 0)
                {
                    // Add to XR inventory
                    InventoryManager.Instance.SpawnItem(GetItemIndex(item));

                    // Deduct coins
                    CoinManager.Instance.RemoveCoins(shopItem.price);

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

    private int GetItemIndex(Item item)
    {
        // Find which index in XRInventorySystem corresponds to this item
        for (int i = 0; i < InventoryManager.Instance.items.Length; i++)
        {
            if (InventoryManager.Instance.items[i].itemName == item.itemName)
            {
                return i;
            }
        }
        return -1; // Not found
    }
}