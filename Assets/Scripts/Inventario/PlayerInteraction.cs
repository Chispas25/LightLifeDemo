using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour
{
    private ItemPickup nearbyPickup;

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.performed && nearbyPickup != null)
        {
            nearbyPickup.TryPickup();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ItemPickup item = collision.GetComponent<ItemPickup>();
        if (item != null)
        {
            nearbyPickup = item;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<ItemPickup>() == nearbyPickup)
        {
            nearbyPickup = null;
        }
    }
}
