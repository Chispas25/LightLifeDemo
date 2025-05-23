using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public InventoryItem[] items = new InventoryItem[3];
    private int currentIndex = 0;

    public bool AddItem(InventoryItem newItem)
    {
        // Comprueba si hay espacio
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i] == null)
            {
                items[i] = newItem;
                Debug.Log($"{gameObject.name} recogió: {newItem.itemName}");
                return true;
            }
        }

        Debug.Log($"{gameObject.name} no tiene espacio en el inventario.");
        return false;
    }

    public void RemoveItem(int index)
    {
        if (index >= 0 && index < items.Length)
        {
            items[index] = null;
        }
    }

    public InventoryItem GetItem(int index)
    {
        if (index >= 0 && index < items.Length)
        {
            return items[index];
        }

        return null;
    }
}
