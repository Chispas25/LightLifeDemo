using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorOpciones1 : MonoBehaviour
{


    public Controladoropciones panelOpciones;

    // Start is called before the first frame update
    void Start()
    {
        panelOpciones = GameObject.FindGameObjectWithTag("opciones").GetComponent<Controladoropciones>();

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            MostrarOpciones();
        }

    }


    public void MostrarOpciones()
    {
        panelOpciones.pantallaOpciones.SetActive(true);
    }


}
