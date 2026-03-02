using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public string itemName = "Key";

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Picked up " + itemName);
            Inventory inv = other.GetComponent<Inventory>();
            inv.AddItem(itemName);
            Destroy(gameObject);
        }
    }
}
