using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Luz_Vela : MonoBehaviour
{
    public Light2D luzVela;
    public CircleCollider2D col;

    private int velas = 0;
    private GameObject jugadorCerca; 

    // Start is called before the first frame update
    void Start()
    {  
        luzVela = GetComponent<Light2D>();
        luzVela.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (jugadorCerca != null && Input.GetKeyDown(KeyCode.E))
        {
            luzVela.enabled = true;
            velas = velas + 1;

            if (velas == 5)
            {
                Debug.Log("Conseguiste llave");
            }
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name.StartsWith("Bola"))
        {
            jugadorCerca = col.gameObject;
            Debug.Log("Jugador bola detectado");
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject == jugadorCerca)
        {
            jugadorCerca = null; // El jugador se fue
        }
    }
}
