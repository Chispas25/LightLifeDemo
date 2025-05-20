using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public Inventory inventory;
    public UnityEngine.UI.Image[] slotImages;

    private void Update()
    {
        for (int i = 0; i < slotImages.Length; i++)
        {
            if (inventory.slots[i] != null)
                slotImages[i].sprite = inventory.slots[i].icon;
            else
                slotImages[i].sprite = null;
        }
    }
}
