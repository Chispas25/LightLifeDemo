using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class AutoDoor : MonoBehaviour
{
    private Animator anim;
    private HashSet<PlayerInput> playersNearby = new HashSet<PlayerInput>();

    private void Start()
    {
        anim = GetComponent<Animator>();
        if (anim == null)
        {
            Debug.LogError("No se encontró Animator en la puerta.");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerInput player = other.GetComponent<PlayerInput>();
        if (player != null)
        {
            playersNearby.Add(player);
            UpdateDoorState();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        PlayerInput player = other.GetComponent<PlayerInput>();
        if (player != null && playersNearby.Contains(player))
        {
            playersNearby.Remove(player);
            UpdateDoorState();
        }
    }

    private void UpdateDoorState()
    {
        bool shouldOpen = playersNearby.Count > 0;
        anim.SetBool("IsOpen", shouldOpen);
        Debug.Log("Puerta " + (shouldOpen ? "abierta" : "cerrada") + ". Jugadores cerca: " + playersNearby.Count);
    }
}

