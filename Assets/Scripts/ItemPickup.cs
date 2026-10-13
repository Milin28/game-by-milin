
using UnityEngine;

public class ItemPickup : MonoBehaviour, IInteractable
{
    [Header("Item Settings")]
    public string itemName = "House key";
    [TextArea]
    public string pickupMessage = "You've got the house keys";

    [Header("Pickup Settings")]
    public bool destroyOnPickup = true;

    private bool isPickedUp = false;

    public void Interact()
    {
        if (isPickedUp)
            return;

        isPickedUp = true;

        Debug.Log(pickupMessage);

        if (destroyOnPickup)
        {
            Destroy(gameObject);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    public string GetInteractionText()
    {
        return "collect" + itemName;
    }
}
