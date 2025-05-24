using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rayo_Luz : MonoBehaviour
{

    Vector3 posicion;
    GameObject Inicio;

    // Start is called before the first frame update
    void Start()
    {
        posicion = transform.position;
        Inicio = GameObject.Find ("Inicio");
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawLine(transform.position, Inicio.transform.position,Color.white,2f);
    }
}
