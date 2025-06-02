using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneradorSensor : MonoBehaviour
{
    public GeneradorK generador;

    private void Start()
    {
        // Encuentra el generador principal en la escena
        generador = FindObjectOfType<GeneradorK>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            generador.AumentarJugadores();
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            generador.DisminuirJugadores();
        }
    }
}
