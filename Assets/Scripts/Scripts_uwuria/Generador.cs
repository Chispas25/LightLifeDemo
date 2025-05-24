using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generador : MonoBehaviour
{
public float energia = 0f;
    public float cargaVelocidad = 10f;
    public bool cargaActiva = true;
    private bool jugadorPresente = false;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!cargaActiva && energia >= 100f)
        return;

        
        if (cargaActiva && jugadorPresente)
        {
            energia += cargaVelocidad * Time.deltaTime;
            energia = Mathf.Clamp(energia, 0f, 100f);

            if (energia >= 100f)
            {
                cargaActiva = false; // ¡Ya está cargado completamente!
                energia = 100f;      // Por si se pasó levemente


                // Trigger evento completado
            }
        }
        else if (!jugadorPresente && energia > 0)
        {
            energia -= cargaVelocidad * Time.deltaTime;
            energia = Mathf.Clamp(energia, 0f, 100f);
        }
                    

                    


                Debug.Log("Energía: " + energia);

    }
    
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player 1")) jugadorPresente = true;

    }

      void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Player 1")) jugadorPresente = false;
    } 
}
