using UnityEngine;

[CreateAssetMenu(fileName = "NewKeyItem", menuName = "Inventory/Key Item")]
public class KeyItem : InventoryItem
{
    public string doorID;

    public override void Use(GameObject user)
    {
        Debug.Log("Este ítem debe usarse cerca de una puerta con ID: " + doorID);
    }
}
