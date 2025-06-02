using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour
{
    public Transform objetivoHoyo;
    public float fuerzaDisparo = 5f;
    private Rigidbody2D rb;
    private bool disparada = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        
    }
    

    public void Disparar()
    {
        if (disparada || objetivoHoyo == null)
        {
            Debug.LogWarning($"Bola {name} no se puede disparar: disparada={disparada}, objetivoHoyo={(objetivoHoyo == null ? "NULL" : objetivoHoyo.name)}");
            return;
        }

        disparada = true;

        Vector2 direccion = (objetivoHoyo.position - transform.position).normalized;
        rb.velocity = direccion * fuerzaDisparo;

        Debug.Log($"Bola {name} disparada hacia {objetivoHoyo.name} con dirección {direccion}");
    }

    void Update()
    {
        if (disparada && Vector2.Distance(transform.position, objetivoHoyo.position) < 0.5f)
        {
            Destroy(gameObject);
        }
        if (Input.GetKeyDown(KeyCode.P))
    {
        rb.velocity = Vector2.right * 5f;
        Debug.Log("Prueba de movimiento");
    }
    }
}
