using UnityEngine;
using UnityEngine.Rendering.Universal;

public class EnergyGenerator : MonoBehaviour
{
    public float energy = 0f;
    public float maxEnergy = 100f;
    public float chargeRate = 10f; // por segundo por jugador
    public float decayRate = 2f;   // opcional: descarga lenta

    public GameObject objectToActivate; // Luz o sistema a encender
    public bool isCharged = false;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (isCharged) return;

        if (other.CompareTag("Player"))
        {
            Light2D playerLight = other.GetComponentInChildren<Light2D>();
            if (playerLight != null && playerLight.intensity > 0.1f)
            {
                energy += chargeRate * Time.deltaTime;
                energy = Mathf.Clamp(energy, 0, maxEnergy);

                if (energy >= maxEnergy)
                {
                    ActivateSystem();
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // Si ningún jugador está en el rango, podrías iniciar descarga
        // (dependiendo de diseño)
    }

    private void Update()
    {
        if (!isCharged && energy > 0f)
        {
            energy -= decayRate * Time.deltaTime;
            energy = Mathf.Clamp(energy, 0, maxEnergy);
        }
    }

    private void ActivateSystem()
    {
        isCharged = true;
        if (objectToActivate != null)
            objectToActivate.SetActive(true);

        Debug.Log("Generador completamente cargado.");
    }
}
