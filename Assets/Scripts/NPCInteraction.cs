using UnityEngine;
using TMPro;

public class NPCInteraction : MonoBehaviour
{
    [Header("UI Elements")]
    public GameObject interactionPrompt;
    public GameObject interactionCardCanvas;
    public TMP_Text npcDialogueText;

    // --- NEW: References to your UI Buttons ---
    [Header("Evidence Buttons")]
    public GameObject casebookButton;
    public GameObject rubbingButton;
    public GameObject clockButton;

    [Header("Cameras")]
    public Camera mainCamera;
    public Camera dialogueCamera;

    [Header("Dialogue Lines")]
    [TextArea(2, 4)] public string greeting = "Hello, detective. Did you feel the ground shake?";
    [TextArea(2, 4)] public string answerOne = "Professor Vance? Nothing! I was entirely focused on my acoustic survey.";
    [TextArea(2, 4)] public string answerTwo = "I was halfway up the stairs inside the main clock tower when I heard glass shatter.";
    [TextArea(2, 4)] public string evidenceCasebook = "Wait, a 1906 photo of the rubble? ...Fine! The clock tower collapsed in the quake.";
    [TextArea(2, 4)] public string evidenceRubbing = "A charcoal rubbing of a plaque? What does that have to do with my acoustic survey?";
    [TextArea(2, 4)] public string evidenceClock = "A broken gear? I study resonant frequencies, detective, not antique clock repair.";

    private bool isPlayerInRange = false;
    private bool isTalking = false;

    void Start()
    {
        if (interactionPrompt != null) interactionPrompt.SetActive(false);
        if (interactionCardCanvas != null) interactionCardCanvas.SetActive(false);
        if (dialogueCamera != null) dialogueCamera.gameObject.SetActive(false);
    }

    void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E) && !isTalking)
        {
            StartConversation();
        }
    }

    void StartConversation()
    {
        isTalking = true;
        mainCamera.gameObject.SetActive(false);
        dialogueCamera.gameObject.SetActive(true);
        interactionCardCanvas.SetActive(true);
        npcDialogueText.text = greeting; // Uses your custom greeting!

        // --- NEW: Check the player's inventory and turn buttons on/off ---
        // Find the player and get their backpack
        // --- NEW: Check the player's inventory and turn buttons on/off ---
        // Find the player and get their backpack
        PlayerInventory inv = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventory>();

        if (inv != null)
        {
            // If the player has the item, the button turns on. If not, it turns off!
            if (casebookButton != null) casebookButton.SetActive(inv.hasCasebook);
            if (rubbingButton != null) rubbingButton.SetActive(inv.hasRubbing);
            if (clockButton != null) clockButton.SetActive(inv.hasClock);
        }
    }

    public void EndConversation()
    {
        isTalking = false;
        dialogueCamera.gameObject.SetActive(false);
        mainCamera.gameObject.SetActive(true);
        interactionCardCanvas.SetActive(false);
        npcDialogueText.text = "Press E to talk";
    }

    // --- UPDATED BUTTON METHODS ---
    public void AskQuestionOne() { npcDialogueText.text = answerOne; }
    public void AskQuestionTwo() { npcDialogueText.text = answerTwo; }
    public void PresentCasebook() { npcDialogueText.text = evidenceCasebook; }
    public void PresentRubbing() { npcDialogueText.text = evidenceRubbing; }
    public void PresentClock() { npcDialogueText.text = evidenceClock; }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isTalking)
        {
            isPlayerInRange = true;
            interactionPrompt.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
            interactionPrompt.SetActive(false);
        }
    }
}