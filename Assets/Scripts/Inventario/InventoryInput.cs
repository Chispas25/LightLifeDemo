using UnityEngine;

public class InventoryInput : MonoBehaviour
{
    private Inventory inventory;

    private void Start()
    {
        inventory = GetComponent<Inventory>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) inventory.UseItem(0);
        if (Input.GetKeyDown(KeyCode.Alpha2)) inventory.UseItem(1);
        if (Input.GetKeyDown(KeyCode.Alpha3)) inventory.UseItem(2);
    }
}
