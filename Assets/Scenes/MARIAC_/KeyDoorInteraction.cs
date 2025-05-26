using UnityEngine;

// Este script combina el comportamiento de la puerta, la detección del jugador y el uso del ítem
public class KeyDoorInteraction : MonoBehaviour
{
    [Header("Configuración de la puerta")]
    public string doorID;                       // ID única que debe coincidir con la llave
    public Animator animator;                   // Animator con animación de apertura
    public bool isOpen = false;

    [Header("Detección del jugador")]
    private PlayerInventory nearbyInventory;    // Inventario del jugador cercano

    private void Start()
    {
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerInventory inventory = other.GetComponent<PlayerInventory>();
        if (inventory != null)
        {
            nearbyInventory = inventory;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<PlayerInventory>() == nearbyInventory)
        {
            nearbyInventory = null;
        }
    }

    private void Update()
    {
        if (nearbyInventory != null && Input.GetKeyDown(KeyCode.E))
        {
            TryOpenDoorWithKey();
        }
    }

    private void TryOpenDoorWithKey()
    {
        if (isOpen) return;

        // Recorre todos los slots del inventario
        for (int i = 0; i < nearbyInventory.items.Length; i++)
        {
            InventoryItem item = nearbyInventory.GetItem(i);

            if (item is KeyItem key && key.doorID == this.doorID)
            {
                OpenDoor();
                nearbyInventory.RemoveItem(i); // Remueve la llave del inventario
                Debug.Log($"Usaste {key.itemName} para abrir la puerta {doorID}");
                return;
            }
        }

        Debug.Log("No tienes la llave correcta para esta puerta.");
    }

    private void OpenDoor()
    {
        isOpen = true;
        if (animator != null)
        {
            animator.SetTrigger("Open");
        }
        else
        {
            Debug.Log("Puerta abierta (sin animación)");
        }
    }
}
