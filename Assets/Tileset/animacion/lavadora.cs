





/*using UnityEngine;
using System.Collections;
public class lavadora : MonoBehaviour
{
    public Animator animator;
    public GameObject itemToGive;
    public void Interact()
    {
        StartCoroutine(StartWashing());
    }
    private IEnumerator StartWashing()
    {
        animator.SetTrigger("Wash");
        yield return new WaitForSeconds(2.5f);
        itemToGive.SetActive(true);
    }

}*/



      /*using UnityEngine;
using UnityEngine.InputSystem;

public class WashingMachinePuzzle : MonoBehaviour
{
    private Animator anim;
    private bool activated = false;

    [Header("Llave")]
    public GameObject llave;
    public GameObject promptUI;

    private PlayerInput currentPlayer; // Tracks the player nearby
    private InputAction interactAction;

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
        if (!activated && currentPlayer != null && interactAction != null && interactAction.triggered)
        {
            TriggerInteraction();
        }
    }

    private void TriggerInteraction()
    {
        activated = true;
        anim.SetTrigger("Start");

        if (llave != null)
            llave.SetActive(true);

        if (promptUI != null)
            promptUI.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var player = other.GetComponent<PlayerInput>();
        if (player != null && !activated)
        {
            currentPlayer = player;
            interactAction = player.actions["Interact"]; // Must match InputActions name
            interactAction.Enable();

            if (promptUI != null)
                promptUI.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        var player = other.GetComponent<PlayerInput>();
        if (player == currentPlayer)
        {
            interactAction.Disable();
            currentPlayer = null;
            interactAction = null;

            if (promptUI != null)
                promptUI.SetActive(false);
        }
    }
}*/

/*using UnityEngine;
using UnityEngine.InputSystem;

public class WashingMachinePuzzle : MonoBehaviour
{
    private Animator anim;
    private bool activated = false;

    [Header("Llave")]
    public GameObject llave;
    public GameObject promptUI;

    [Header("Sonido")]
    public AudioSource washingSound; // Asigna esto desde el inspector

    private PlayerInput currentPlayer;
    private InputAction interactAction;

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
        if (!activated && currentPlayer != null && interactAction != null && interactAction.triggered)
        {
            TriggerInteraction();
        }
    }

    private void TriggerInteraction()
    {
        activated = true;
        anim.SetTrigger("Start");

        if (washingSound != null)
            washingSound.Play(); // Reproduce el sonido

        StartCoroutine(ActivateKeyAfterAnimation());
    }

    private System.Collections.IEnumerator ActivateKeyAfterAnimation()
    {
        yield return new WaitForSeconds(2f); // Ajusta al tiempo de la animación
        if (llave != null)
            llave.SetActive(true);

        if (promptUI != null)
            promptUI.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var player = other.GetComponent<PlayerInput>();
        if (player != null && !activated)
        {
            currentPlayer = player;
            interactAction = player.actions["Interact"];
            interactAction.Enable();

            if (promptUI != null)
                promptUI.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        var player = other.GetComponent<PlayerInput>();
        if (player == currentPlayer)
        {
            interactAction.Disable();
            currentPlayer = null;
            interactAction = null;

            if (promptUI != null)
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
}



