
using UnityEngine;

public class DialogueTrigger : MonoBehaviour, IInteractable
{
    public string speaker = "Mom";

    [TextArea(2, 5)]
    public string[] lines =
    {
        "You've made it home, right?",
        "Make sure to lock the door securely tonight.",
        "If someone comes, don't open the door yet."
    };

    private bool hasPlayed;

    public void Interact()
    {
        Debug.Log("DialogueTrigger: Interact called");

        if (hasPlayed)
        {
            Debug.Log("Dialogue already played");
            return;
        }

        if (DialogueManager.Instance == null)
        {
            Debug.LogError("DialogueManager.Instance is NULL!");
            return;
        }

        if (DialogueManager.Instance.IsDialogueActive)
        {
            Debug.Log("Dialogue is already active");
            return;
        }

        if (lines == null || lines.Length == 0)
        {
            Debug.LogError("Dialogue lines are empty!");
            return;
        }

        DialogueManager.Instance.StartDialogue(speaker, lines);
        hasPlayed = true;

        Debug.Log("Dialogue started");
    }

    public string GetInteractionText()
    {
        return "Read";
    }
}
