
using UnityEngine;

public class DoorInteractable : MonoBehaviour, IInteractable
{
    [Header("Door Settings")]
    public Transform door;
    public Vector3 openRotation = new Vector3(0f, 90f, 0f);
    public float rotationSpeed = 180f;
    public bool isOpen = false;

    private Quaternion closedRotation;
    private Quaternion targetRotation;

    void Start()
    {
        if (door == null)
            door = transform;

        closedRotation = door.localRotation;
        targetRotation = closedRotation;
    }

    void Update()
    {
        door.localRotation = Quaternion.RotateTowards(
            door.localRotation,
            targetRotation,
            rotationSpeed * Time.deltaTime
        );
    }

    public void Interact()
    {
        isOpen = !isOpen;

        targetRotation = isOpen
            ? closedRotation * Quaternion.Euler(openRotation)
            : closedRotation;
    }

    public string GetInteractionText()
    {
        return isOpen ? "Close" : "OPEN";
    }
}
