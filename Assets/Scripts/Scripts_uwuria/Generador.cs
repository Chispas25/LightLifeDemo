using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generador : MonoBehaviour
{
    public float energia = 0f;
    public float cargaVelocidad = 10f; // Velocidad base por jugador
    public bool cargaActiva = true;
    private int jugadoresPresentes = 0;  // Contador de jugadores dentro de la zona

    void Update()
    {
        if (!cargaActiva && energia >= 100f)
            return;

        if (cargaActiva && jugadoresPresentes > 0)
        {
            energia += cargaVelocidad * jugadoresPresentes * Time.deltaTime;
            energia = Mathf.Clamp(energia, 0f, 100f);

            if (energia >= 100f)
            {
                cargaActiva = false;
                energia = 100f;

                // Aquí puedes lanzar evento o llamar función que avise que la carga se completó
            }
        }
        else if (jugadoresPresentes == 0 && energia > 0)
        {
            energia -= cargaVelocidad * Time.deltaTime; // descarga solo a velocidad base
            energia = Mathf.Clamp(energia, 0f, 100f);
        }

        Debug.Log("Energía: " + energia + " | Jugadores presentes: " + jugadoresPresentes);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag.StartsWith("Player"))
        {
            jugadoresPresentes++;
            Debug.Log("Jugador entró. Jugadores presentes: " + jugadoresPresentes);
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag.StartsWith("Player"))
        {
            jugadoresPresentes = Mathf.Max(0, jugadoresPresentes - 1);
            Debug.Log("Jugador salió. Jugadores presentes: " + jugadoresPresentes);
        }
    }
}
