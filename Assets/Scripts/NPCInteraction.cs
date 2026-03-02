using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInteraction : MonoBehaviour
{
    [Header("Dialogue Settings")]
    [TextArea] public string dialogue = "Hello, adventurer! Here's a clue for you.";
    public float displayTime = 3f;  // How long dialogue stays visible
    public bool oneTimeClue = false; // Set true if clue should only appear once

    private bool playerInRange = false;
    private bool dialogueVisible = false;
    private bool hasGivenClue = false;
    private float timer = 0f;

    void Update()
    {
        // If player is in range and dialogue not visible, show it
        if (playerInRange && !dialogueVisible && (!oneTimeClue || !hasGivenClue))
        {
            ShowDialogue();
        }

        // Countdown to hide dialogue automatically
        if (dialogueVisible)
        {
            timer -= Time.deltaTime;
            if (timer <= 0f)
            {
                HideDialogue();
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            HideDialogue();
        }
    }

    void ShowDialogue()
    {
        Debug.Log(dialogue); // Replace with UI pop-up later
        dialogueVisible = true;
        timer = displayTime;
        hasGivenClue = true;
    }

    void HideDialogue()
    {
        if (dialogueVisible)
        {
            Debug.Log("Dialogue hidden"); // Replace with UI hide
            dialogueVisible = false;
        }
    }
}
