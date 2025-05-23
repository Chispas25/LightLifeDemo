using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public InventoryItem item;

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerInventory inventory = other.GetComponent<PlayerInventory>();
        if (inventory != null)
        {
            bool pickedUp = inventory.AddItem(item);
            if (pickedUp)
            {
                Destroy(gameObject);
            }
        }
    }
}
