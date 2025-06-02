using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZonaBillarInteract : MonoBehaviour, IInteractable
{
    public void Interact(GameObject jugador)
    {
        PuzzleBillarManager.instance.Interactuar();
    }

    public bool JugadorPuedeInteractuar()
    {
        return true; // o cualquier condición extra si quieres
    }
}
