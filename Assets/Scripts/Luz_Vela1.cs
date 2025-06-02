using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;

public class Luz_Vela1 : MonoBehaviour
{
    public Light2D luzVela;
    private bool encendida = false;

    // Para saber quién está cerca
    private Dictionary<GameObject, InputAction> jugadoresCerca = new Dictionary<GameObject, InputAction>();

    void Start()
    {
        luzVela = GetComponent<Light2D>();
        luzVela.enabled = false;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        // Detectar si el objeto tiene PlayerInput
        PlayerInput input = col.GetComponent<PlayerInput>();
        if (input != null && !encendida)
        {
            // Obtener acción "Interact"
            InputAction interactAction = input.actions["Interact"];
            if (!jugadoresCerca.ContainsKey(col.gameObject))
            {
                interactAction.performed += ctx => EncenderVela(col.gameObject);
                jugadoresCerca.Add(col.gameObject, interactAction);
                Debug.Log($"Jugador {col.gameObject.name} está cerca de la vela.");
            }
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (jugadoresCerca.TryGetValue(col.gameObject, out InputAction action))
        {
            action.performed -= ctx => EncenderVela(col.gameObject);
            jugadoresCerca.Remove(col.gameObject);
            Debug.Log($"Jugador {col.gameObject.name} se alejó de la vela.");
        }
    }

    private void EncenderVela(GameObject jugador)
    {
        if (encendida) return;

        luzVela.enabled = true;
        encendida = true;

        // Registrar como punto seguro
        PlayerLightManager.RegisterSafeLight(this.transform);

        Debug.Log($"¡{jugador.name} encendió la vela!");

        // Si usas un puzzle de velas, aquí puedes notificarlo:
        // VelaPuzzleManager.Instance.RegistrarVelaEncendida();

        // Cancelar todas las suscripciones activas (ya no se puede interactuar)
        foreach (var kvp in jugadoresCerca)
        {
            kvp.Value.performed -= ctx => EncenderVela(kvp.Key);
        }
        jugadoresCerca.Clear();
    }
}
