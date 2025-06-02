using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PuzzleBillarManager : MonoBehaviour
{
    public static PuzzleBillarManager instance;

    [Header("Bolas en orden")]
    public List<BallScript> bolasEnOrden;

    [Header("Cooldown entre disparos")]
    public float cooldownEntreDisparos = 2f;
    private bool puedeDisparar = true;

    [Header("Puzzle completado")]
    public GameObject llave;
    

    private int indiceActual = 0;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        if (llave != null)
            llave.SetActive(false);

        
    }

    public void Interactuar()
    {
        if (!puedeDisparar || indiceActual >= bolasEnOrden.Count)
            return;

        BallScript bola = bolasEnOrden[indiceActual];

        if (bola != null)
        {
            puedeDisparar = false;
            bola.Disparar();
            StartCoroutine(ResetearCooldown());
        }
    }

    IEnumerator ResetearCooldown()
    {
        yield return new WaitUntil(() => bolasEnOrden[indiceActual] == null); // espera a que se destruya
        indiceActual++;
        yield return new WaitForSeconds(cooldownEntreDisparos);
        puedeDisparar = true;

        if (indiceActual >= bolasEnOrden.Count)
            PuzzleCompletado();
    }

    void PuzzleCompletado()
    {
        Debug.Log("¡Puzzle del billar completado!");
        if (llave != null)
            llave.SetActive(true);

        
    }
}
