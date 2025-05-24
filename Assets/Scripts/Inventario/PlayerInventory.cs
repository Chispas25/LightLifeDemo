using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public InventoryItem[] items = new InventoryItem[3];
    private int currentIndex = 0;

    public InventoryUI inventoryUI;

    public int CurrentIndex => currentIndex;

    public bool AddItem(InventoryItem newItem)
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i] == null)
            {
                items[i] = newItem;
                Debug.Log($"{gameObject.name} recogió: {newItem.itemName}");

                inventoryUI?.UpdateInventoryUI(); // Actualiza la UI después de recoger

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
            inventoryUI?.UpdateInventoryUI(); // Refresca la UI al remover ítem
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

    public void NextSlot()
    {
        currentIndex = (currentIndex + 1) % items.Length;
        Debug.Log($"Slot activo de {gameObject.name}: {currentIndex + 1}");
    }

    public void UseCurrentItem()
    {
        InventoryItem item = GetItem(currentIndex);
        if (item != null)
        {
            Debug.Log($"{gameObject.name} usa: {item.itemName}");
            // Aquí aplicarías efectos concretos del ítem
            RemoveItem(currentIndex);
        }
        else
        {
            Debug.Log($"{gameObject.name} no tiene ítem en el slot {currentIndex + 1}");
        }
    }
}
