 using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PlayerLightManager : MonoBehaviour
{
    [Header("Parámetros de luz dinámica")]
    public float maxConnectionDistance = 10f;
    public float minIntensity = 0.2f;
    public float maxIntensity = 1f;
    public float minRadius = 1f;
    public float maxRadius = 5f;

    [Header("Parámetros de aislamiento")]
    public float safeLightRadius = 5f;
    public LayerMask obstructionMask;

    [Header("Referencias del jugador")]
    public Light2D playerLight;
    public PlayerMovement movementScript;
    public List<Transform> safeLightSources;

    private bool isIsolated = false;
    private static List<PlayerLightManager> allPlayers;
    private PlayerHealthManager healthManager;

    void Awake()
    {
        if (allPlayers == null)
            allPlayers = new List<PlayerLightManager>();

        allPlayers.Add(this);
    }

    void OnDestroy()
    {
        allPlayers.Remove(this);
    }

    void Start()
    {
        healthManager = GetComponent<PlayerHealthManager>();
    }

    void Update()
    {
        if (healthManager != null && healthManager.IsDead())
        {
            Debug.Log("El jugador ha muerto");
            if (movementScript.enabled) movementScript.enabled = false;
            if (playerLight.enabled) playerLight.enabled = false;
            return;
        }

        float closestConnectedDistance = float.MaxValue;
        bool isConnected = false;

        foreach (var other in allPlayers)
        {
            if (other == this) continue;

            float distance = Vector2.Distance(transform.position, other.transform.position);
            if (distance <= maxConnectionDistance)
            {
                Vector2 dir = (other.transform.position - transform.position).normalized;
                RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, distance, obstructionMask);
                if (!hit)
                {
                    isConnected = true;
                    if (distance < closestConnectedDistance)
                        closestConnectedDistance = distance;
                }
            }
        }

        // Verificar salvaguardas (luces seguras)
        if (!isConnected)
        {
            foreach (Transform source in safeLightSources)
            {
                if (Vector2.Distance(transform.position, source.position) <= safeLightRadius)
                {
                    isConnected = true;
                    break;
                }
            }
        }

        // Cambiar estado de aislamiento
        if (isConnected && isIsolated)
        {
            movementScript.enabled = true;
            playerLight.enabled = true;
            isIsolated = false;
        }
        else if (!isConnected && !isIsolated)
        {
            movementScript.enabled = false;
            playerLight.enabled = false;
            isIsolated = true;

            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = Vector2.zero;
                rb.angularVelocity = 0f;
            }
        }

        // Calcular intensidad de luz SOLO si hay conexión
        if (isConnected && !isIsolated)
        {
            float t = Mathf.InverseLerp(0f, maxConnectionDistance, closestConnectedDistance);
            float currentIntensity = Mathf.Lerp(maxIntensity, minIntensity, t);
            playerLight.intensity = currentIntensity;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        foreach (Transform lightSource in safeLightSources)
        {
            Gizmos.DrawWireSphere(lightSource.position, safeLightRadius);
        }
    }
}
