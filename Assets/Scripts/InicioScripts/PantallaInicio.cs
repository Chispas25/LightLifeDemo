using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PantallaInicio : MonoBehaviour
{

    public GameObject panelOpciones;

    public GameObject panelInicio;



    // Start is called before the first frame update
    void Start()
    {

        panelOpciones = GameObject.Find("PanelOpciones");
        panelOpciones.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {

    }



    public void StartGame()
    {

        Debug.Log("Jugar");
        SceneManager.LoadScene("2plantabaja Antonio");

    }

    public void ExitGame()
    {

        Debug.Log("Salir");

        Application.Quit();

    }

    public void MostrarOpciones()
    {

        panelOpciones.SetActive(true);

        panelInicio.SetActive(false);  // Oculta el panel del menú principal
        panelOpciones.SetActive(true); // Muestra el panel de opciones

    }


    public void OcultarOpciones()
    {
        panelOpciones.SetActive(false);  // Oculta el panel de opciones
        panelInicio.SetActive(true);   // Muestra el panel del menú principal

    }








}