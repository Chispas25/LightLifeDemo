using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneradorSensor : MonoBehaviour
{
    public GeneradorK1 generador;

    private void Start()
    {
        // Encuentra el generador principal en la escena
        generador = FindObjectOfType<GeneradorK1>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            generador.AumentarJugadores();
            Debug.Log("He aumentado el numero de jugadores");
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            generador.DisminuirJugadores();
            Debug.Log("He disminuido el numero de jugadores");
        }
    }
}
