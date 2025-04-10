using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopSlot : MonoBehaviour
{
    public Image icon;
    public Text itemName;
    public Text priceText;
    public Text stockText;

    ShopItem item;

    public void AddItem(ShopItem newItem)
    {
        item = newItem;
        icon.sprite = item.item.icon;
        icon.enabled = true;
        itemName.text = item.item.name;
        priceText.text = item.price.ToString();

        if (item.currentStock == -1)
        {
            stockText.text = "∞";
        }
        else
        {
            stockText.text = item.currentStock.ToString();
        }
    }

    public void ClearSlot()
    {
        item = null;
        icon.sprite = null;
        icon.enabled = false;
        itemName.text = "";
        priceText.text = "";
        stockText.text = "";
    }

    public void OnPurchaseButton()
    {
        if (item != null && ShopManager.Instance != null)
        {
            ShopManager.Instance.PurchaseItem(item.item);
        }
    }
}