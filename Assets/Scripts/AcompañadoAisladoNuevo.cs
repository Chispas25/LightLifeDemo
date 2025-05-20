//c
 using System.Collections.Generic;
 using UnityEngine;
 using UnityEngine.Rendering.Universal;
public class PlayerIsolationController : MonoBehaviour
{
    public float maxConnectionDistance = 10f;
    public float safeLightRadius = 5f;
    public Light2D playerLight;
    public PlayerMovement movementScript;
    public List<Transform> otherPlayers;
    public List<Transform> safeLightSources; // Fuentes de luz fijas como velas
    public LayerMask obstructionMask; // Capa para muros, paredes, etc.

    private bool isIsolated = false;
    void Update()
    {
        bool isConnected = false;
        // Verificar conexión con otros jugadores
        foreach (Transform other in otherPlayers)
        {
            float distance = Vector2.Distance(transform.position, other.position);
            if (distance <= maxConnectionDistance)
            {
                Vector2 direction = (other.position - transform.position).normalized;
                RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, distance,obstructionMask);
                
                if (!hit)
                {
                    isConnected = true;
                    break;
                }
            }
        }
        // Verificar conexión con fuentes de luz seguras
        if (!isConnected)
        {
            foreach (Transform lightSource in safeLightSources)
            {
                float distance = Vector2.Distance(transform.position, lightSource.position);
                if (distance <= safeLightRadius)
                {
                    isConnected = true;
                    break;
                }
            }
        }
        if (isConnected && isIsolated)
        {
            // Reconectar
            movementScript.enabled = true;
            playerLight.enabled = true;
            isIsolated = false;
        }
        else if (!isConnected && !isIsolated)
        {
            // Aislar
            movementScript.enabled = false;

            playerLight.enabled = false;
            isIsolated = true;
        }
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        foreach (Transform other in otherPlayers)
        {
            Gizmos.DrawLine(transform.position, other.position);
        }
        Gizmos.color = Color.cyan;
        foreach (Transform lightSource in safeLightSources)
        {
            Gizmos.DrawWireSphere(lightSource.position, safeLightRadius);

            
        }
    }

}

