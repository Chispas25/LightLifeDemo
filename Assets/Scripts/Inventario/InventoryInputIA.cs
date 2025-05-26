using UnityEngine;
using UnityEngine.InputSystem;

public class InventoryInputIA : MonoBehaviour
{
    private PlayerInventory inventory;

    public InventoryUI inventoryUI;
    public GameObject inventoryUIPanel; // Asignar en el Inspector (Canvas del inventario)

    private bool inventoryVisible = false;

    private void Awake()
    {
        inventory = GetComponent<PlayerInventory>();

        // Vincula la UI desde aquí si no se ha asignado manualmente
        if (inventoryUI == null)
        {
            inventoryUI = GetComponentInChildren<InventoryUI>();
        }

        // Conecta también desde el inventario si hace falta
        if (inventory != null)
        {
            inventory.inventoryUI = inventoryUI;
        }
    }

    public void OnInventoryNext(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            inventory.NextSlot();
            inventoryUI?.SetCurrentSlot(inventory.CurrentIndex); // <- Actualiza la UI
        }
    }

    public void OnInventoryUse(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            inventory.UseCurrentItem();
            inventoryUI?.UpdateInventoryUI(); // <- Refresca la UI tras usar el ítem
        }
    }

    public void OnInventoryToggle(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            inventoryVisible = !inventoryVisible;
            inventoryUIPanel.SetActive(inventoryVisible);

            if (inventoryVisible)
            {
                inventoryUI.UpdateInventoryUI();
            }
        }
    }
}
