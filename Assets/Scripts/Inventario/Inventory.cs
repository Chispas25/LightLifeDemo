using UnityEngine;


public class Inventory : MonoBehaviour
{
    public InventoryItem[] slots = new InventoryItem[3];

    public bool AddItem(InventoryItem item)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i] == null)
            {
                slots[i] = item;
                Debug.Log("Objeto añadido al slot " + i);
                return true;
            }
        }

        Debug.Log("Inventario lleno");
        return false;
    }

    public void UseItem(int slotIndex)
    {
        if (slotIndex < 0 || slotIndex >= slots.Length) return;

        if (slots[slotIndex] != null)
        {
            slots[slotIndex].Use(gameObject);
            slots[slotIndex] = null;
        }
    }
}
