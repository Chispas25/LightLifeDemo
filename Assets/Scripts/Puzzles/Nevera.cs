using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class Nevera : MonoBehaviour
{
    private bool activated = false;

    [Header("Llaves")]
    public GameObject llavePrefab;
    public Transform[] spawnPoints; // Asigna 4 posiciones en el inspector

    [Header("UI")]
    public GameObject promptUI;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip washingSound;

    private List<PlayerInput> nearbyPlayers = new List<PlayerInput>();

    private void Start()
    {
        if (promptUI != null)
            promptUI.SetActive(false);
    }

    private void Update()
    {
        if (!activated)
        {
            foreach (var player in nearbyPlayers)
            {
                var interactAction = player.actions["Interact"];
                if (interactAction != null && interactAction.triggered)
                {
                    ActivateMachine();
                    break;
                }
            }
        }
    }

    private void ActivateMachine()
    {
        activated = true;

        if (promptUI != null)
            promptUI.SetActive(false);

        // Reproducir sonido
        if (audioSource != null && washingSound != null)
            audioSource.PlayOneShot(washingSound);

        // Instanciar las 4 llaves
        if (llavePrefab != null && spawnPoints != null)
        {
            foreach (var point in spawnPoints)
            {
                Instantiate(llavePrefab, point.position, Quaternion.identity);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var player = other.GetComponent<PlayerInput>();
        if (player != null && !nearbyPlayers.Contains(player))
        {
            nearbyPlayers.Add(player);
            if (!activated && promptUI != null)
                promptUI.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        var player = other.GetComponent<PlayerInput>();
        if (player != null && nearbyPlayers.Contains(player))
        {
            nearbyPlayers.Remove(player);
            if (nearbyPlayers.Count == 0 && promptUI != null)
                promptUI.SetActive(false);
        }
    }
}
