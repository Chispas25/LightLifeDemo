

/*using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;

public class WashingMachinePuzzle : MonoBehaviour
{
    private Animator anim;
    private bool activated = false;

    [Header("Llave")]
    public GameObject llave;
    public GameObject promptUI;

    [Header("Audio")]
    public AudioSource audioSource;       
    public AudioClip washingSound;         

    private List<PlayerInput> nearbyPlayers = new List<PlayerInput>();

    private void Start()
    {
        anim = GetComponent<Animator>();

        if (llave != null)
            llave.SetActive(false);

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
                    StartCoroutine(StartWashingSequence());
                    break;
                }
            }
        }
    }

    private IEnumerator StartWashingSequence()
    {
        activated = true;

        if (promptUI != null)
            promptUI.SetActive(false);

        // Reproducir sonido (si está asignado)
        if (audioSource != null && washingSound != null)
            audioSource.PlayOneShot(washingSound);

        // Activar animación
        anim.SetTrigger("Lava");

        
        yield return new WaitForSeconds(8f); // Ajusta este valor al total real de tu animación

        // Activar la llave
        if (llave != null)
            llave.SetActive(true);
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
}*/

using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;

public class WashingMachinePuzzle : MonoBehaviour
{
    private Animator anim;
    private bool activated = false;

    [Header("Llave")]
    public GameObject llave;
    public GameObject promptUI;

    [Header("Audio")]
    public AudioClip washingSound;

    private GameObject tempAudioObject;
    private AudioSource tempAudioSource;

    private List<PlayerInput> nearbyPlayers = new List<PlayerInput>();

    private void Start()
    {
        anim = GetComponent<Animator>();

        if (llave != null)
            llave.SetActive(false);

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
                    StartCoroutine(StartWashingSequence());
                    break;
                }
            }
        }
    }

    private IEnumerator StartWashingSequence()
    {
        activated = true;

        if (promptUI != null)
            promptUI.SetActive(false);

        // Activar animación
        anim.SetTrigger("Lava");

        // Iniciar sonido de lavadora
        StartWashingSound();

        // Esperar la duración de la animación
        yield return new WaitForSeconds(8f);

        // Detener sonido
        StopWashingSound();

        // Mostrar llave
        if (llave != null)
            llave.SetActive(true);
    }

    private void StartWashingSound()
    {
        if (washingSound == null)
            return;

        tempAudioObject = new GameObject("TempWashingSound");
        tempAudioObject.transform.position = transform.position;

        tempAudioSource = tempAudioObject.AddComponent<AudioSource>();
        tempAudioSource.clip = washingSound;
        tempAudioSource.loop = true;
        tempAudioSource.volume = 1f;
        tempAudioSource.spatialBlend = 0f; // Sonido 2D
        tempAudioSource.Play();
    }

    private void StopWashingSound()
    {
        if (tempAudioSource != null)
            tempAudioSource.Stop();

        if (tempAudioObject != null)
            Destroy(tempAudioObject);
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
