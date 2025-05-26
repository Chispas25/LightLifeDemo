using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;

public class StairTeleportBidirectional : MonoBehaviour
{
    public StairTeleportBidirectional otherStair; // La otra escalera

    private HashSet<GameObject> playersNearby = new HashSet<GameObject>();
    private Dictionary<GameObject, float> playerCooldowns = new Dictionary<GameObject, float>();

    public float teleportCooldown = 1.0f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<PlayerInput>() != null && !playersNearby.Contains(other.gameObject))
        {
            GameObject player = other.gameObject;

            // Si está en cooldown, no teletransportar
            if (playerCooldowns.ContainsKey(player) && Time.time < playerCooldowns[player])
                return;

            playersNearby.Add(player);
            TryTeleport();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (playersNearby.Contains(other.gameObject))
        {
            playersNearby.Remove(other.gameObject);
        }
    }

    private void TryTeleport()
    {
        if (playersNearby.Count < 2) return;

        List<GameObject> playersToTeleport = new List<GameObject>(playersNearby);

        foreach (GameObject player in playersToTeleport)
        {
            player.transform.position = otherStair.transform.position;

            // Iniciar cooldown en ambas escaleras
            playerCooldowns[player] = Time.time + teleportCooldown;
            otherStair.playerCooldowns[player] = Time.time + teleportCooldown;
        }

        playersNearby.Clear();
        otherStair.playersNearby.Clear();
    }
}
