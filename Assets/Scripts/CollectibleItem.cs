using UnityEngine;
using TMPro;

public class CollectibleItem : MonoBehaviour
{
    public enum ItemType { Casebook, Rubbing, Clock }

    [Header("Item Settings")]
    public ItemType whatItemIsThis;
    public string itemName;
    [TextArea(2, 4)]
    public string itemDescription;

    [Header("UI Elements")]
    public GameObject pickupPrompt;
    public TMP_Text promptText;

    [Header("Cameras")]
    public Camera itemCamera; // The new zoomed-in camera
    private Camera mainCam;   // The script will find your main camera automatically!

    private bool isPlayerInRange = false;
    private bool isInspecting = false; // Tracks if we are currently zoomed in

    void Start()
    {
        mainCam = Camera.main; // Grabs the scene's Main Camera automatically

        if (itemCamera != null) itemCamera.gameObject.SetActive(false);
        if (pickupPrompt != null) pickupPrompt.SetActive(false);

        UpdatePromptText("Press E to inspect");
    }

    void Update()
    {
        // If the player is in range and presses E...
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            if (!isInspecting)
            {
                InspectItem(); // First press: Zoom in!
            }
            else
            {
                CollectItem(); // Second press: Take it!
            }
        }
    }

    void InspectItem()
    {
        isInspecting = true;

        // Turn off the main camera and turn on the zoomed camera
        if (mainCam != null) mainCam.gameObject.SetActive(false);
        if (itemCamera != null) itemCamera.gameObject.SetActive(true);

        // Update the bubble to show the lore and the new instruction
        UpdatePromptText("<b>" + itemName + "</b>\n<size=80%>" + itemDescription + "</size>\n\n<i>Press E to take item</i>");
    }

    void CollectItem()
    {
        // Turn the main camera back on before destroying the item
        if (mainCam != null) mainCam.gameObject.SetActive(true);

        PlayerInventory inv = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventory>();
        if (inv != null)
        {
            if (whatItemIsThis == ItemType.Casebook) inv.hasCasebook = true;
            if (whatItemIsThis == ItemType.Rubbing) inv.hasRubbing = true;
            if (whatItemIsThis == ItemType.Clock) inv.hasClock = true;

            Destroy(gameObject); // Vanish!
        }
    }

    void UpdatePromptText(string text)
    {
        if (promptText != null) promptText.text = text;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !isInspecting)
        {
            isPlayerInRange = true;
            if (pickupPrompt != null) pickupPrompt.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
            if (pickupPrompt != null) pickupPrompt.SetActive(false);
        }
    }
}