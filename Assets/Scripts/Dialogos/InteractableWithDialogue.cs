using System.Collections.Generic;
using UnityEngine;

public class InteractableWithDialogue : MonoBehaviour, IInteractable
{
    [TextArea]
    public List<string> tutorialLines;

    private bool hasBeenInteracted = false;

    public void Interact(GameObject interactor)
    {
        if (!hasBeenInteracted)
        {
            hasBeenInteracted = true;

            // Dispara el diálogo la primera vez
            DialogueManager.Instance.StartDialogue(tutorialLines, true, () =>
            {
                // ✅ Al finalizar el diálogo, intenta recoger el ítem si tiene uno
                ItemPickup pickup = GetComponent<ItemPickup>();
                if (pickup != null)
                {
                    pickup.TryPickupFromOutside(interactor);
                }
            });
        }
        else
        {
            Debug.Log("Ya interactuaste con esto.");
        }
    }
}
