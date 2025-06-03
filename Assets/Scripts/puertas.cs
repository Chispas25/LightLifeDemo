/*using UnityEngine;
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

        // Asegurarse de que la puerta comience cerrada
        anim.SetBool("IsOpen", false);
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
}*/

using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class AutoDoor : MonoBehaviour
{
    private Animator anim;
    private AudioSource audioSource;
    private bool isOpen = false;
    private HashSet<PlayerInput> playersNearby = new HashSet<PlayerInput>();

    [Header("Sonidos de la puerta")]
    public AudioClip openSound;
    public AudioClip closeSound;

    private void Start()
    {
        anim = GetComponent<Animator>();
        if (anim == null)
        {
            Debug.LogError("No se encontró Animator en la puerta.");
        }

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogError("No se encontró AudioSource en la puerta.");
        }

        anim.SetBool("IsOpen", false);
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

        if (shouldOpen != isOpen)
        {
            anim.SetBool("IsOpen", shouldOpen);
            isOpen = shouldOpen;

            if (audioSource != null)
            {
                audioSource.clip = shouldOpen ? openSound : closeSound;
                audioSource.Play();
            }
        }

        Debug.Log("Puerta " + (shouldOpen ? "abierta" : "cerrada") + ". Jugadores cerca: " + playersNearby.Count);
    }
}

