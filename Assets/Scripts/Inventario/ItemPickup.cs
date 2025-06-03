/*using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public InventoryItem item;

    private PlayerInventory playerInventory;
    private bool canPickup = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        playerInventory = other.GetComponent<PlayerInventory>();
        if (playerInventory != null)
        {
            canPickup = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<PlayerInventory>() == playerInventory)
        {
            canPickup = false;
            playerInventory = null;
        }
    }

    public void TryPickup()
    {
        if (canPickup && playerInventory != null)
        {
            bool pickedUp = playerInventory.AddItem(item);
            if (pickedUp)
            {
                Destroy(gameObject);
            }
        }
    }

    // NUEVO: permite pickup desde fuera (como después del diálogo)
    public void TryPickupFromOutside(GameObject interactor)
    {
        var inventory = interactor.GetComponent<PlayerInventory>();
        if (inventory != null)
        {
            bool pickedUp = inventory.AddItem(item);
            if (pickedUp)
            {
                Destroy(gameObject);
            }
        }
    }
}*/


using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public InventoryItem item;

    [Header("Sonido")]
    public AudioClip pickupSound;

    private PlayerInventory playerInventory;
    private bool canPickup = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        playerInventory = other.GetComponent<PlayerInventory>();
        if (playerInventory != null)
        {
            canPickup = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<PlayerInventory>() == playerInventory)
        {
            canPickup = false;
            playerInventory = null;
        }
    }

    public void TryPickup()
    {
        if (canPickup && playerInventory != null)
        {
            if (playerInventory.AddItem(item))
            {
                PlayPickupSound();
                Destroy(gameObject);
            }
        }
    }

    public void TryPickupFromOutside(GameObject interactor)
    {
        var inventory = interactor.GetComponent<PlayerInventory>();
        if (inventory != null)
        {
            if (inventory.AddItem(item))
            {
                PlayPickupSound();
                Destroy(gameObject);
            }
        }
    }

    private void PlayPickupSound()
    {
        if (pickupSound != null)
        {
            AudioSource.PlayClipAtPoint(pickupSound, transform.position);
        }
    }
}

