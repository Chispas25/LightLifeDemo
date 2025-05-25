using UnityEngine;

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
}
