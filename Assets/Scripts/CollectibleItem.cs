using UnityEngine;
using TMPro; // Needed to update the text bubble

public class CollectibleItem : MonoBehaviour
{
    public enum ItemType { Casebook, Rubbing, Clock }

    [Header("Item Settings")]
    public ItemType whatItemIsThis;

    [Tooltip("The name of the item (e.g., 'Broken Gear')")]
    public string itemName;

    [Tooltip("A short description of what it is.")]
    [TextArea(2, 4)]
    public string itemDescription;

    [Header("UI Elements")]
    public GameObject pickupPrompt; // The bubble that pops up
    public TMP_Text promptText; // The text inside the bubble

    private bool isPlayerInRange = false;

    void Start()
    {
        // Hide the prompt when the game starts
        if (pickupPrompt != null)
        {
            pickupPrompt.SetActive(false);
        }

        // Combine the name and description into the bubble
        if (promptText != null)
        {
            promptText.text = "<b>" + itemName + "</b>\n<size=80%>" + itemDescription + "</size>\n\n<i>Press E to pick up</i>";
        }
    }

    void Update()
    {
        // If the player is standing on it AND presses E
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            CollectItem();
        }
    }

    void CollectItem()
    {
        // Find the player's backpack
        PlayerInventory inv = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventory>();

        if (inv != null)
        {
            // Give them the correct item
            if (whatItemIsThis == ItemType.Casebook) inv.hasCasebook = true;
            if (whatItemIsThis == ItemType.Rubbing) inv.hasRubbing = true;
            if (whatItemIsThis == ItemType.Clock) inv.hasClock = true;

            // Destroy the item (and its UI bubble) from the floor
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
            if (pickupPrompt != null) pickupPrompt.SetActive(true); // Show bubble
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
            if (pickupPrompt != null) pickupPrompt.SetActive(false); // Hide bubble
        }
    }
}