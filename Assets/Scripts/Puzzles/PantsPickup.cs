using UnityEngine;

public class PantsPickup : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerPuzzleState playerState = other.GetComponent<PlayerPuzzleState>();
            if (playerState != null)
            {
                playerState.hasPants = true;
                Destroy(gameObject); // Elimina el pantalón del mundo
            }
        }
    }
}
