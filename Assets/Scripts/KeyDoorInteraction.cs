/*using UnityEngine;

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

        // Fuerza la puerta a estar cerrada al inicio
        if (animator != null)
        {
            animator.ResetTrigger("Open");
            animator.Play("Closed", 0, 0); // Reproduce animación de puerta cerrada
        }

        // Asegura que el collider físico esté activo al inicio
        Collider2D[] colliders = GetComponents<Collider2D>();
        foreach (var col in colliders)
        {
            if (!col.isTrigger)
            {
                col.enabled = true;
            }
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

        for (int i = 0; i < nearbyInventory.items.Length; i++)
        {
            InventoryItem item = nearbyInventory.GetItem(i);

            if (item is KeyItem key && key.doorID == this.doorID)
            {
                OpenDoor();
                nearbyInventory.RemoveItem(i); // Elimina la llave usada
                Debug.Log($"Usaste {key.itemName} para abrir la puerta {doorID}");
                return;
            }
        }

        Debug.Log("No tienes la llave correcta para esta puerta.");
    }

    private void OpenDoor()
    {
        isOpen = true;

        // Desactiva el collider físico para permitir pasar
        Collider2D[] colliders = GetComponents<Collider2D>();
        foreach (var col in colliders)
        {
            if (!col.isTrigger)
            {
                col.enabled = false;
            }
        }

        // Activa animación
        if (animator != null)
        {
            animator.SetTrigger("Open");
        }
        else
        {
            Debug.Log("Puerta abierta (sin animación)");
        }
    }
}*/


/*using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class KeyDoorInteraction : MonoBehaviour
{
    [Header("Configuración de la puerta")]
    public string doorID;
    public Animator animator;
    public bool isOpen = false;

    [Header("Detección de jugadores cerca")]
    private List<PlayerInventory> nearbyInventories = new List<PlayerInventory>();

    private void Start()
    {
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }

        if (animator != null)
        {
            animator.ResetTrigger("Open");
            animator.Play("cerrarautopuerta", 0, 0);
        }

        // Asegura que el collider esté activo
        Collider2D[] colliders = GetComponents<Collider2D>();
        foreach (var col in colliders)
        {
            if (!col.isTrigger)
            {
                col.enabled = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerInventory inventory = other.GetComponent<PlayerInventory>();
        if (inventory != null && !nearbyInventories.Contains(inventory))
        {
            nearbyInventories.Add(inventory);

            if (inventory.playerInput != null)
            {
                inventory.playerInput.actions["Interact"].performed += OnInteract;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        PlayerInventory inventory = other.GetComponent<PlayerInventory>();
        if (inventory != null && nearbyInventories.Contains(inventory))
        {
            if (inventory.playerInput != null)
            {
                inventory.playerInput.actions["Interact"].performed -= OnInteract;
            }

            nearbyInventories.Remove(inventory);
        }
    }

    private void OnInteract(InputAction.CallbackContext context)
    {
        foreach (var inventory in nearbyInventories)
        {
            if (inventory.playerInput != null &&
                inventory.playerInput.actions["Interact"] == context.action)
            {
                if (!isOpen)
                {
                    TryOpenDoorWithKey(inventory);
                }
                break;
            }
        }
    }

    private void TryOpenDoorWithKey(PlayerInventory inventory)
    {
        for (int i = 0; i < inventory.items.Length; i++)
        {
            InventoryItem item = inventory.GetItem(i);

            if (item is KeyItem key && key.doorID == this.doorID)
            {
                OpenDoor();
                inventory.RemoveItem(i);
                Debug.Log($"Usaste {key.itemName} para abrir la puerta {doorID}");
                return;
            }
        }

        Debug.Log("No tienes la llave correcta para esta puerta.");
    }

    private void OpenDoor()
    {
        isOpen = true;

        Collider2D[] colliders = GetComponents<Collider2D>();
        foreach (var col in colliders)
        {
            if (!col.isTrigger)
            {
                col.enabled = false;
            }
        }

        if (animator != null)
        {
            animator.Play("autopuerta");
        }
        else
        {
            Debug.Log("Puerta abierta (sin animación)");
        }
    }
}*/
