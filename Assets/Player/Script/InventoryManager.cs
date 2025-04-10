// InventoryManager.cs
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance { get; private set; }

    public List<InventoryItem> inventory = new List<InventoryItem>();
    public int coins { get; private set; }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Add item to inventory
    public void AddItem(Item item, int quantity = 1)
    {
        // Check if item already exists in inventory and is stackable
        InventoryItem existingItem = inventory.Find(i => i.item == item && i.item.isStackable);

        if (existingItem != null)
        {
            existingItem.quantity += quantity;
        }
        else
        {
            inventory.Add(new InventoryItem(item, quantity));
        }

        UpdateUI();
    }

    // Remove item from inventory
    public bool RemoveItem(Item item, int quantity = 1)
    {
        InventoryItem existingItem = inventory.Find(i => i.item == item);

        if (existingItem != null)
        {
            if (existingItem.item.isUnlimited)
            {
                // Unlimited items never deplete
                return true;
            }

            if (existingItem.quantity >= quantity)
            {
                existingItem.quantity -= quantity;

                if (existingItem.quantity <= 0)
                {
                    inventory.Remove(existingItem);
                }

                UpdateUI();
                return true;
            }
        }
        return false;
    }

    // Currency management
    public void AddCoins(int amount)
    {
        coins += amount;
        UpdateUI();
    }

    public bool RemoveCoins(int amount)
    {
        if (coins >= amount)
        {
            coins -= amount;
            UpdateUI();
            return true;
        }
        return false;
    }

    // Update UI (connect this to your UI system)
    void UpdateUI()
    {
        // Implement your UI update logic here
        // This should update both inventory display and coin display
    }
}