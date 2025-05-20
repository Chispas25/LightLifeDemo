using UnityEngine;
using System.Collections;

public class WashingMachine : MonoBehaviour
{
    public GameObject rewardObject; // Lo que se entrega al final
    public float washTime = 2f; // Tiempo de animación o espera
    private bool isUsed = false;

    void OnTriggerStay2D(Collider2D other)
    {
        if (isUsed) return;

        if (other.CompareTag("Player") && Input.GetKeyDown(KeyCode.E))
        {
            PlayerPuzzleState playerState = other.GetComponent<PlayerPuzzleState>();
            if (playerState != null && playerState.hasPants)
            {
                playerState.hasPants = false;
                StartCoroutine(StartWashing());
            }
        }
    }

    private IEnumerator StartWashing()
    {
        isUsed = true;
        Debug.Log("Lavando pantalón...");

        // Aquí puedes activar animación o cambiar sprite
        yield return new WaitForSeconds(washTime);

        if (rewardObject != null)
            rewardObject.SetActive(true); // O instanciar

        Debug.Log("Objeto entregado por la lavadora.");
    }
}
