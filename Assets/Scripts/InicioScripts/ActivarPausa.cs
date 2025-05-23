using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorPausa : MonoBehaviour
{
    public GameObject panelOpciones;
    private bool estaPausado = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            estaPausado = !estaPausado;
            panelOpciones.SetActive(estaPausado);

            Time.timeScale = estaPausado ? 0 : 1;
        }
    }

    public void ReanudarJuego()
    {
        estaPausado = false;
        panelOpciones.SetActive(false);
        Time.timeScale = 1;
    }
}