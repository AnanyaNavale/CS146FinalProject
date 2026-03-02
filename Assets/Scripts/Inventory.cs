using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<string> items = new List<string>();

    public void AddItem(string item)
    {
        items.Add(item);
        Debug.Log("Inventory now has: " + item);
    }

    public bool HasItem(string item)
    {
        return items.Contains(item);
    }
}
