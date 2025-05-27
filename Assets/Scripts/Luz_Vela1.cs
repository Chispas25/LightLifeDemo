using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Luz_Vela1 : MonoBehaviour
{
    public Light2D luzVela;
    public CircleCollider2D col;

    private bool encendida = false;
    private GameObject jugadorCerca; 

    void Start()
    {  
        luzVela = GetComponent<Light2D>();
        luzVela.enabled = false;
    }

    void Update()
    {
        if (jugadorCerca != null && Input.GetKeyDown(KeyCode.E) && !encendida)
        {
            luzVela.enabled = true;
            encendida = true;

            // Registrar como punto seguro en todos los PlayerLightManager
            PlayerLightManager.RegisterSafeLight(this.transform);

            Debug.Log("¡Vela encendida y registrada como punto seguro!");

            // Opcional: lógica para contar cuántas velas están encendidas
            //VelaPuzzleManager.Instance.RegistrarVelaEncendida();
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
            jugadorCerca = null;
        }
    }
}
