using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

[System.Serializable]
public class InventoryItem
{
    public string itemName;
    public GameObject itemPrefab;
    public string foodTag;
}


public class InventoryManager : MonoBehaviour
{
    [Header("Hand References")]
    public XRRayInteractor leftHandRay; // For UI
    public XRRayInteractor rightHandRay; // For UI
    public XRDirectInteractor leftHandDirect; // For grabbing
    public XRDirectInteractor rightHandDirect; // For grabbing

    private static InventoryManager _instance;

    public static InventoryManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<InventoryManager>();
                if (_instance == null)
                {
                    GameObject obj = new GameObject("InventoryManager");
                    _instance = obj.AddComponent<InventoryManager>();
                }
            }
            return _instance;
        }
    }

    public InventoryItem[] items;
    
    [Header("XR References")]
    public XRDirectInteractor handInteractor;
    public InputActionReference gripAction;
    private GameObject currentHeldItem;
    private bool isHolding = false;

    private void Awake()
    {
        gripAction.action.started += OnGripPressed;
        gripAction.action.canceled += OnGripReleased;
    }

    private void OnDestroy()
    {
        gripAction.action.started -= OnGripPressed;
        gripAction.action.canceled -= OnGripReleased;
    }

    public void SelectItem(int itemIndex)
    {
        // Get which hand is pointing at UI
        bool usingLeftHand = leftHandRay.isSelectActive;

        XRDirectInteractor targetHand = usingLeftHand ? leftHandDirect : rightHandDirect;

        // Spawn logic
        Vector3 spawnPos = targetHand.transform.position +
                         targetHand.transform.forward * 0.3f;

        currentHeldItem = Instantiate(items[itemIndex].itemPrefab, spawnPos, Quaternion.identity);

        var grabInteractable = currentHeldItem.GetComponent<XRGrabInteractable>();
        targetHand.StartManualInteraction(grabInteractable);
    }

    public void SpawnItem(int itemIndex)
    {
        currentHeldItem = Instantiate(
            items[itemIndex].itemPrefab,
            handInteractor.transform.position,
            Quaternion.identity
        );

        var grabInteractable = currentHeldItem.GetComponent<XRGrabInteractable>();
        handInteractor.StartManualInteraction(grabInteractable);
        isHolding = true;
    }

    public void OnGripPressed(InputAction.CallbackContext context)
    {
        // Optional: Add haptic feedback
    }

    public void OnGripReleased(InputAction.CallbackContext context)
    {
        if (!isHolding) return;

        var rb = currentHeldItem.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.AddForce(handInteractor.transform.forward * 5f, ForceMode.Impulse);
        }
        isHolding = false;
    }

    public void OnItemConsumed(GameObject consumedItem)
    {
        
        Debug.Log($"Item consumed: {consumedItem.name}");

        
        if (currentHeldItem == consumedItem)
        {
            currentHeldItem = null;
        }
    }
}