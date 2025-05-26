using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class MenuNavegacion : MonoBehaviour
{
    public List<Button> botones; // Asigna tus botones en orden en el inspector
    public GameObject panelOpciones;
    public GameObject panelInicio;

    private int indiceSeleccionado = 0;

    void Start()
    {
        if (botones.Count > 0)
        {
            EventSystem.current.SetSelectedGameObject(botones[indiceSeleccionado].gameObject);
        }
    }

    void Update()
    {
        // Navegación hacia abajo
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            indiceSeleccionado++;
            if (indiceSeleccionado >= botones.Count) indiceSeleccionado = 0;
            ActualizarSeleccion();
        }

        // Navegación hacia arriba
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            indiceSeleccionado--;
            if (indiceSeleccionado < 0) indiceSeleccionado = botones.Count - 1;
            ActualizarSeleccion();
        }

        // Seleccionar con tecla Enter (la del teclado principal, no la del pad numérico)
        if (Input.GetKeyDown(KeyCode.Return))
        {
            botones[indiceSeleccionado].onClick.Invoke();
        }

        // Esc para salir
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (panelOpciones.activeSelf)
            {
                panelOpciones.SetActive(false);
                panelInicio.SetActive(true);
                indiceSeleccionado = 0;
                ActualizarSeleccion();
            }
        }
    }

    void ActualizarSeleccion()
    {
        EventSystem.current.SetSelectedGameObject(botones[indiceSeleccionado].gameObject);
    }

    







}