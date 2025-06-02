using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class InventoryItem : ScriptableObject
{
    public string itemName;
    public Sprite icon;

    // Usar el ítem. El GameObject es quien lo usa.
    public virtual bool Use(GameObject user)
    {
        Debug.Log($"Usando item base: {itemName}, sin efecto.");
        return true; // Por defecto se consume
    }
}