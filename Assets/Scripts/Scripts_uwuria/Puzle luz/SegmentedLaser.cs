using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class SegmentedLaser : MonoBehaviour
{
    public Transform puntoInicio;
    public Transform puntoFinal;

    public float cambioColorDistancia;

    public Color colorAntes = Color.black;
    private Color colorDespues = Color.blue;

    private LineRenderer lineRenderer;
    private List<SpriteRenderer> objetosDentro = new List<SpriteRenderer>();
    private bool jugadorDentro = false;

    void Start()
    {
        // Buscar la primera bola para definir la posición del cambio
        Transform[] todosLosObjetos = GameObject.FindObjectsOfType<Transform>();
        foreach (Transform t in todosLosObjetos)
        {
            if (t.name.StartsWith("Bola"))
            {
                cambioColorDistancia = t.position.x;
                break;
            }
        }

        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 3;

        if (puntoInicio == null)
            puntoInicio = GameObject.Find("Inicio")?.transform;

        if (puntoFinal == null)
            puntoFinal = this.transform;

        lineRenderer.useWorldSpace = true;
        lineRenderer.widthMultiplier = 0.1f;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));

        ActualizarLinea(false);
    }

    void Update()
    {
        // Actualizar estado si hay objetos dentro
        if (objetosDentro.Count > 0)
        {
            jugadorDentro = true;
            colorDespues = CalcularColorPromedio();
        }
        else
        {
            jugadorDentro = false;
        }

        ActualizarLinea(jugadorDentro);
    }

    void ActualizarLinea(bool jugadorActivo)
    {
        Vector3 inicio = puntoInicio.position;
        Vector3 fin = puntoFinal.position;
        Vector3 direccion = (fin - inicio).normalized;
        Vector3 puntoCambio = inicio + direccion * cambioColorDistancia;

        lineRenderer.SetPosition(0, inicio);
        //lineRenderer.SetPosition(1, puntoCambio);
        lineRenderer.SetPosition(2, fin);

        Gradient gradient = new Gradient();

        if (jugadorActivo)
        {
            gradient.SetKeys(
                new GradientColorKey[] {
                    new GradientColorKey(colorAntes, 0f),
                    new GradientColorKey(colorAntes, 0.499f),
                    new GradientColorKey(colorDespues, 0.5f),
                    new GradientColorKey(colorDespues, 1f),
                },
                new GradientAlphaKey[] {
                    new GradientAlphaKey(1f, 0f),
                    new GradientAlphaKey(1f, 1f),
                }
            );
        }
        else
        {
            gradient.SetKeys(
                new GradientColorKey[] {
                    new GradientColorKey(colorAntes, 0f),
                    new GradientColorKey(colorAntes, 1f),
                },
                new GradientAlphaKey[] {
                    new GradientAlphaKey(1f, 0f),
                    new GradientAlphaKey(1f, 1f),
                }
            );
        }

        lineRenderer.colorGradient = gradient;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name.StartsWith("Bola"))
        {
            SpriteRenderer sr = other.GetComponent<SpriteRenderer>();
            if (sr != null && !objetosDentro.Contains(sr))
            {
                objetosDentro.Add(sr);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.name.StartsWith("Bola"))
        {
            SpriteRenderer sr = other.GetComponent<SpriteRenderer>();
            if (sr != null && objetosDentro.Contains(sr))
            {
                objetosDentro.Remove(sr);
            }
        }
    }

    private Color CalcularColorPromedio()
    {
        if (objetosDentro.Count == 0)
            return Color.blue;

        float r = 0f, g = 0f, b = 0f;

        foreach (SpriteRenderer sr in objetosDentro)
        {
            r += sr.color.r;
            g += sr.color.g;
            b += sr.color.b;
        }

        int count = objetosDentro.Count;
        return new Color(r / count, g / count, b / count, 1f);
    }
}

