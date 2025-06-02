using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class luz_proyeccion : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    // Jugadores en contacto
    private HashSet<Color> playersInContact = new HashSet<Color>();

    // Objeto que se "iluminará"
    public GameObject objectToIlluminate;
    private SpriteRenderer illuminationRenderer;

    // Colores de estado
    public Color targetColor = Color.green;      // Color combinado deseado
    public Color illuminatedColor = Color.white; // Color cuando se ilumina
    public Color normalColor = Color.gray;       // Color cuando está apagado

    // Iluminacion activa
    private bool alreadyActivated = false;
    public AudioClip fxwin;
    private AudioSource victory;

    // NUEVO: contador de jugadores iluminados correctamente
    private int correctIlluminatedCount = 0;

    // NUEVO: total esperado de jugadores (configurarlo según tu juego)
    public int totalPlayersToIlluminate = 2;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        victory = this.GetComponent<AudioSource>();

        if (objectToIlluminate != null)
        {
            illuminationRenderer = objectToIlluminate.GetComponent<SpriteRenderer>();
            if (illuminationRenderer != null)
            {
                illuminationRenderer.color = normalColor;
            }
        }
    }

    void Update()
    {
        if (alreadyActivated)
        {
            illuminationRenderer.color = illuminatedColor;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!string.IsNullOrEmpty(other.tag) && other.tag.StartsWith("Player"))
        {
            SpriteRenderer playerRenderer = other.GetComponent<SpriteRenderer>();
            if (playerRenderer != null)
            {
                playersInContact.Add(playerRenderer.color);
                UpdateColor();
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (!string.IsNullOrEmpty(other.tag) && other.tag.StartsWith("Player"))
        {
            SpriteRenderer playerRenderer = other.GetComponent<SpriteRenderer>();
            if (playerRenderer != null)
            {
                playersInContact.Remove(playerRenderer.color);
                UpdateColor();
            }
        }
    }

    void UpdateColor()
    {
        // Combina colores de todos los jugadores en contacto
        Color combinedColor = CombineColors(playersInContact);
        spriteRenderer.color = combinedColor;

        if (illuminationRenderer != null)
        {
            if (ApproximatelyEqual(combinedColor, targetColor))
            {
                illuminationRenderer.color = illuminatedColor;
                if (!alreadyActivated)
                {
                    alreadyActivated = true;
                    victory.PlayOneShot(fxwin);
                    correctIlluminatedCount++;
                    
                    // Chequea si todos están iluminados
                    if (correctIlluminatedCount >= totalPlayersToIlluminate)
                    {
                        Debug.Log("¡Todos los jugadores están iluminados correctamente! Objetivo completado.");
                    }
                }
            }
        }
    }

    Color CombineColors(HashSet<Color> colors)
    {
        Color combined = new Color(0, 0, 0);
        foreach (Color c in colors)
        {
            combined += c;
        }

        // Asegurarse de que los valores están entre 0 y 1
        return new Color(
            Mathf.Clamp01(combined.r),
            Mathf.Clamp01(combined.g),
            Mathf.Clamp01(combined.b)
        );
    }

    bool ApproximatelyEqual(Color a, Color b, float tolerance = 0.01f)
    {
        return Mathf.Abs(a.r - b.r) < tolerance &&
               Mathf.Abs(a.g - b.g) < tolerance &&
               Mathf.Abs(a.b - b.b) < tolerance;
    }
}

