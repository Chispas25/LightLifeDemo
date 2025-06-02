using System.Collections.Generic;
using UnityEngine;

public class InteractableWithDialogue : MonoBehaviour, IInteractable
{
    [Header("Texto del tutorial (se muestra solo una vez por grupo)")]
    [TextArea]
    public List<string> tutorialLines;

    [Header("ID único compartido entre objetos del mismo grupo")]
    public string dialogueID = "default_tutorial"; // Ej: "puzzle_nevera", "item_manzana"

    public void Interact(GameObject interactor)
    {
        if (!TutorialDialogueRegistry.HasBeenShown(dialogueID))
        {
            TutorialDialogueRegistry.MarkAsShown(dialogueID);

            DialogueManager.Instance.StartDialogue(tutorialLines, true, () =>
            {
                TryPickup(interactor);
            });
        }
        else
        {
            TryPickup(interactor); // Permitir recogida si ya se mostró el diálogo
        }
    }

    private void TryPickup(GameObject interactor)
    {
        ItemPickup pickup = GetComponent<ItemPickup>();
        if (pickup != null)
        {
            pickup.TryPickupFromOutside(interactor);
        }
    }
}
