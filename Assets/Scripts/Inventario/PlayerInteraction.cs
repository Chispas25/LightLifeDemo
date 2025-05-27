using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour
{
    private ItemPickup nearbyPickup;
    private IInteractable nearbyInteractable;

    public GameObject interactionPopupImage;

    private void Start()
    {
        if (interactionPopupImage != null)
            interactionPopupImage.SetActive(false);
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        // Primero ejecuta diálogo (si hay)
        if (nearbyInteractable != null)
        {
            nearbyInteractable.Interact(gameObject);
        }

        // Luego intenta recoger ítem (si hay)
        if (nearbyPickup != null)
        {
            nearbyPickup.TryPickup();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Primero detectar interactuable (como diálogos)
        IInteractable interactable = collision.GetComponent<IInteractable>();
        if (interactable != null)
        {
            nearbyInteractable = interactable;
            ShowPopup(true);
            return;
        }

        // Luego detectar pickup si no había interactuable
        ItemPickup item = collision.GetComponent<ItemPickup>();
        if (item != null)
        {
            nearbyPickup = item;
            ShowPopup(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<ItemPickup>() == nearbyPickup)
        {
            nearbyPickup = null;
            ShowPopup(false);
        }

        if (collision.GetComponent<IInteractable>() == nearbyInteractable)
        {
            nearbyInteractable = null;
            ShowPopup(false);
        }
    }

    void ShowPopup(bool show)
    {
        if (interactionPopupImage != null)
            interactionPopupImage.SetActive(show);
    }
}
