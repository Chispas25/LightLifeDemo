using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class AcompañadoAislado : MonoBehaviour
{

    public static bool amAislado = false;

    Vector3 posIn;

    Light2D luzP1;

    GameObject posPlayer2;
    // Start is called before the first frame update
    void Start()
    {
        
        
        

        

        posIn = this.transform.position;

        posPlayer2 = GameObject.FindGameObjectWithTag("Player 2");
    }

    // Update is called once per frame
    void Update()
    {
        // Esto es para saber la distancia entre jugadores
        float distancia = Vector3.Distance(transform.position, posPlayer2.transform.position);


        //mecanica Aislado, falta acompañado y añadir las luces y tambien programar la obtencion de estos items
        if (distancia > 10f || distancia < -10f)
        {

            amAislado = true;

            




        }
        else
        {

            amAislado=false;
        }
    }
}
