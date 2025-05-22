using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PantallaInicio : MonoBehaviour
{
    // Start is called before the first frame update


    GameObject panelOpciones;


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
        SceneManager.LoadScene("plantabaja");

    }

    public void ExitGame()
    {

        Debug.Log("Salir");

        Application.Quit();

    }

    public void MostrarOpciones()
    {

        panelOpciones.SetActive(true);


    }


    public void OcultarOpciones()
    {
        panelOpciones.SetActive(false);

    }







}