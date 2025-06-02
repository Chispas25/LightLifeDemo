using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generador : MonoBehaviour
{
    public static float energia = 0f;
    public float cargaVelocidad = 1f;
    public static bool cargaActiva = true;
    public static int jugadoresPresentes = 0;

    public Animator generador_anim; // Asigna este desde el Inspector en Unity

    void Start()
    {
        // Solo si quieres obtenerlo desde el mismo GameObject
         //generador_anim = GetComponent<Animator>();

        // Si ya lo arrastras desde el Inspector, no hace falta hacer nada aquí
    }

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
            }
        }
        else if (jugadoresPresentes == 0 && energia > 0)
        {
            energia -= cargaVelocidad * Time.deltaTime;
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
            generador_anim.SetBool("Activacion", true);

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