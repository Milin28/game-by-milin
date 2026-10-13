
using UnityEngine;
using TMPro;

public class InteractionSystem : MonoBehaviour
{
    [Header("Interaction Settings")]
    public Camera playerCamera;
    public float interactionDistance = 3f;
    public KeyCode interactionKey = KeyCode.E;
    public LayerMask interactionLayers = ~0;

    [Header("UI")]
    public TMP_Text interactionText;

    private IInteractable currentInteractable;

    void Update()
    {
        CheckForInteractable();

        if (currentInteractable != null &&
            Input.GetKeyDown(interactionKey))
        {
            currentInteractable.Interact();
        }
    }

    void CheckForInteractable()
    {
        currentInteractable = null;

        if (playerCamera == null)
        {
            SetInteractionText("");
            return;
        }

        Ray ray = new Ray(
            playerCamera.transform.position,
            playerCamera.transform.forward
        );

        if (Physics.Raycast(
            ray,
            out RaycastHit hit,
            interactionDistance,
            interactionLayers,
            QueryTriggerInteraction.Ignore))
        {
            IInteractable interactable =
                hit.collider.GetComponentInParent<IInteractable>();

            if (interactable != null)
            {
                currentInteractable = interactable;
                SetInteractionText(
                    "[E] " + interactable.GetInteractionText()
                );
                return;
            }
        }

        SetInteractionText("");
    }

    void SetInteractionText(string message)
    {
        if (interactionText != null)
        {
            interactionText.text = message;
            interactionText.enabled = !string.IsNullOrEmpty(message);
        }
    }
}
