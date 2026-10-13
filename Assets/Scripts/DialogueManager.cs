
using UnityEngine;
using TMPro;
using System.Collections;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    [Header("UI")]
    public GameObject dialoguePanel;
    public TMP_Text speakerText;
    public TMP_Text dialogueText;

    [Header("Settings")]
    public KeyCode nextKey = KeyCode.Space;
    public float typingSpeed = 0.03f;

    private string[] dialogueLines;
    private int currentLineIndex;
    private Coroutine typingCoroutine;
    private bool isTyping;
    private bool isDialogueActive;

    public bool IsDialogueActive => isDialogueActive;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        Debug.Log("DialogueManager initialized");
    }

    void Start()
    {
        if (dialoguePanel == null)
        {
            Debug.LogError("Dialogue Panel is not assigned!");
            return;
        }

        dialoguePanel.SetActive(false);
    }

    void Update()
    {
        if (!isDialogueActive)
            return;

        if (Input.GetKeyDown(nextKey))
        {
            if (isTyping)
                ShowFullLine();
            else
                ShowNextLine();
        }
    }

    public void StartDialogue(string speaker, string[] lines)
    {
        if (lines == null || lines.Length == 0)
        {
            Debug.LogError("Dialogue lines are empty!");
            return;
        }

        if (dialoguePanel == null ||
            speakerText == null ||
            dialogueText == null)
        {
            Debug.LogError("Dialogue UI references are missing!");
            return;
        }

        dialogueLines = lines;
        currentLineIndex = 0;
        isDialogueActive = true;

        dialoguePanel.SetActive(true);
        speakerText.text = speaker;

        Debug.Log("Dialogue panel active: " + dialoguePanel.activeInHierarchy);

        ShowCurrentLine();
    }

    void ShowCurrentLine()
    {
        if (typingCoroutine != null)
            StopCoroutine(typingCoroutine);

        typingCoroutine = StartCoroutine(
            TypeLine(dialogueLines[currentLineIndex])
        );
    }

    IEnumerator TypeLine(string line)
    {
        isTyping = true;
        dialogueText.text = "";

        foreach (char character in line)
        {
            dialogueText.text += character;
            yield return new WaitForSecondsRealtime(typingSpeed);
        }

        isTyping = false;
        typingCoroutine = null;

        Debug.Log("Dialogue text displayed: " + dialogueText.text);
    }

    void ShowFullLine()
    {
        if (typingCoroutine != null)
            StopCoroutine(typingCoroutine);

        dialogueText.text = dialogueLines[currentLineIndex];
        isTyping = false;
        typingCoroutine = null;
    }

    void ShowNextLine()
    {
        currentLineIndex++;

        if (currentLineIndex >= dialogueLines.Length)
        {
            EndDialogue();
            return;
        }

        speakerText.text = Instance.speakerText.text;
        ShowCurrentLine();
    }

    public void EndDialogue()
    {
        if (typingCoroutine != null)
            StopCoroutine(typingCoroutine);

        typingCoroutine = null;
        isTyping = false;
        isDialogueActive = false;

        if (dialoguePanel != null)
            dialoguePanel.SetActive(false);
    }
}
