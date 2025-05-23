using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class InventoryItem1 : ScriptableObject
{
    public string itemName;
    public Sprite icon;
    // Puedes añadir más propiedades como descripción, tipo, efectos, etc.
}