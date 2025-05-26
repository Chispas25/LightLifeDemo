using UnityEngine;

[CreateAssetMenu(fileName = "NewKeyItem", menuName = "Inventory/Key Item")]
public class KeyItem : InventoryItem
{
    public string doorID; // ID que debe coincidir con la puerta

    public override void Use(GameObject user)
    {
        // El comportamiento se maneja en el script de la puerta
        Debug.Log("Este ítem debe usarse cerca de una puerta con ID: " + doorID);
    }
}
