using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.XR.Interaction.Toolkit;

public class ShopSlot : MonoBehaviour
{
    public MeshRenderer iconRenderer;
    public TextMeshPro priceText;
    public TextMeshPro stockText;
    public XRSimpleInteractable interactable;

    private ShopItem _item;

    void Start()
    {
        interactable.onSelectEntered.AddListener(_ => TryPurchase());
    }

    public void AddItem(ShopItem item)
    {
        _item = item;
        iconRenderer.material.mainTexture = item.item.icon.texture;
        priceText.text = $"{item.price} coins";
        stockText.text = item.currentStock < 0 ? "∞" : item.currentStock.ToString();
    }

    public void TryPurchase()
    {
        if (_item != null)
            ShopManager.Instance.PurchaseItem(_item.item);
    }
}