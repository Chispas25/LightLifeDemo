using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public InventoryItem item;

    private bool playerInRange = false;
    private PlayerInventory playerInventory;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Asegúrate de que los jugadores tienen la tag "Player"
        {
            playerInventory = other.GetComponent<PlayerInventory>();
            if (playerInventory != null)
                playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && other.GetComponent<PlayerInventory>() == playerInventory)
        {
            playerInRange = false;
            playerInventory = null;
        }
    }

    public void TryPickup()
    {
        if (playerInRange && playerInventory != null)
        {
            bool pickedUp = playerInventory.AddItem(item);
            if (pickedUp)
            {
                Destroy(gameObject);
            }
        }
    }
}
