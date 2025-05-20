using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "Inventory/Item")]
public class InventoryItem : ScriptableObject
{
    public string itemName;
    public Sprite icon;

    public virtual void Use(GameObject user)
    {
        Debug.Log("Usando el objeto: " + itemName);
    }
}
