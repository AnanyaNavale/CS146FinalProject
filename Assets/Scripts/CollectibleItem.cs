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
    public Camera itemCamera;
    private Camera mainCam;

    [Header("Sound Effects")]
    public AudioClip inspectSound;  // Sound for first E press (inspect)
    public AudioClip collectSound;  // Sound for second E press (collect)
    private AudioSource audioSource;

    private bool isPlayerInRange = false;
    private bool isInspecting = false;

    void Start()
    {
        mainCam = Camera.main;
        if (itemCamera != null) itemCamera.gameObject.SetActive(false);
        if (pickupPrompt != null) pickupPrompt.SetActive(false);
        UpdatePromptText("Press E to inspect");

        // Add an AudioSource component automatically at runtime
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.spatialBlend = 0f; // 2D sound
        audioSource.playOnAwake = false;
    }

    void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            if (!isInspecting)
            {
                InspectItem();
            }
            else
            {
                CollectItem();
            }
        }
    }

    void InspectItem()
    {
        isInspecting = true;

        // Play inspect sound at 60% volume
        if (inspectSound != null) audioSource.PlayOneShot(inspectSound, 0.6f);

        if (mainCam != null) mainCam.gameObject.SetActive(false);
        if (itemCamera != null) itemCamera.gameObject.SetActive(true);
        UpdatePromptText("<b>" + itemName + "</b>\n<size=80%>" + itemDescription + "</size>\n\n<i>Press E to take item</i>");
    }

    void CollectItem()
    {
        // Fix the camera FIRST before anything else
        if (itemCamera != null) itemCamera.gameObject.SetActive(false);
        if (mainCam != null) mainCam.gameObject.SetActive(true);

        // Reset state
        isInspecting = false;
        isPlayerInRange = false;
        if (pickupPrompt != null) pickupPrompt.SetActive(false);

        PlayerInventory inv = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInventory>();
        if (inv != null)
        {
            if (whatItemIsThis == ItemType.Casebook) inv.hasCasebook = true;
            if (whatItemIsThis == ItemType.Rubbing) inv.hasRubbing = true;
            if (whatItemIsThis == ItemType.Clock) inv.hasClock = true;
        }

        // Hide and disable the object immediately
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;

        // Play sound then destroy after clip finishes
        if (collectSound != null)
        {
            audioSource.PlayOneShot(collectSound, 0.6f);
            Destroy(gameObject, collectSound.length);
        }
        else
        {
            Destroy(gameObject);
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