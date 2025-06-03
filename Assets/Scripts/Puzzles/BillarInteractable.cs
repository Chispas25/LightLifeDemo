using UnityEngine;

public class BillarInteractable : MonoBehaviour, IInteractable
{
    public void Interact(GameObject interactor)
    {
        Debug.Log("El jugador interactuó con el puzzle del billar");
        PuzzleBillarManager.instance.Interactuar();
    }
}
