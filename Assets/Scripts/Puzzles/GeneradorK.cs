using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.Universal;

public class GeneradorK : MonoBehaviour
{
    private float energia = 0f;
    public float cargaVelocidad = 1f;
    private bool cargaActiva = true;
    private int jugadoresPresentes = 0;

    public Animator generador_anim;
    public Image barraEnergia;

    [Header("Puzzle completado")]
    public GameObject llave;
    public List<Light2D> lucesALiberar;

    private bool completado = false;

    void Start()
    {
        if (llave != null)
            llave.SetActive(false);

        foreach (var luz in lucesALiberar)
        {
            if (luz != null)
                luz.enabled = false;
        }
    }

    void Update()
    {
        if (completado)
            return;

        if (cargaActiva && jugadoresPresentes > 0)
        {
            energia += cargaVelocidad * jugadoresPresentes * Time.deltaTime;
            energia = Mathf.Clamp(energia, 0f, 100f);

            if (energia >= 100f)
            {
                energia = 100f;
                cargaActiva = false;
                ActivarPuzzle();
            }
        }
        else if (jugadoresPresentes == 0 && energia > 0)
        {
            energia -= cargaVelocidad * Time.deltaTime;
            energia = Mathf.Clamp(energia, 0f, 100f);
        }

        if (barraEnergia != null)
            barraEnergia.fillAmount = energia / 100f;

        Debug.Log("Energía: " + energia + " | Jugadores presentes: " + jugadoresPresentes);
    }

    private void ActivarPuzzle()
    {
        completado = true;

        Debug.Log("¡Generador completado! Activando luz y llave.");

        if (llave != null)
            llave.SetActive(true);

        foreach (var luz in lucesALiberar)
        {
            if (luz != null)
            {
                luz.enabled = true;
                PlayerLightManager.RegisterSafeLight(luz.transform);
            }
        }
    }

    // Métodos públicos que los sensores llamarán
    public void AumentarJugadores()
    {
        jugadoresPresentes++;
        if (generador_anim != null)
            generador_anim.SetBool("Activacion", true);
    }

    public void DisminuirJugadores()
    {
        jugadoresPresentes = Mathf.Max(0, jugadoresPresentes - 1);
    }
}
