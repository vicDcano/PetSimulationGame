using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.XR.Interaction.Toolkit;

public class InventorySlot : MonoBehaviour
{
    public Image icon;
    public TMP_Text itemName;
    public int slotIndex;

    private XRSimpleInteractable xrInteractable;

    void Start()
    {
        xrInteractable = GetComponent<XRSimpleInteractable>();
        xrInteractable.selectEntered.AddListener(_ => SelectSlot());
    }

    public void SelectSlot()
    {
        InventoryManager.Instance.SelectItem(slotIndex);
    }

    public void SetupSlot(InventoryItem item)
    {
        
        itemName.text = item.itemName;
    }
}