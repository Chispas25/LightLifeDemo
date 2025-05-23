using UnityEngine;
using UnityEngine.UI;

public class LifeBarUI : MonoBehaviour
{
    public Image barraInterna;
    public float visibleDuration = 2f;

    private float hideTimer = 0f;
    private CanvasGroup canvasGroup;
    private int maxLives = 3;

    void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        if (barraInterna == null)
            Debug.LogError("Barra interna no asignada.");
    }

    public void SetMaxLives(int lives)
    {
        maxLives = lives;
        UpdateBar(maxLives); // Mostrar la barra completa al inicio
    }

    public void UpdateBar(int currentLives)
    {
        if (canvasGroup == null) return;

        // Mostrar la barra y reiniciar el temporizador
        canvasGroup.alpha = 1f;
        hideTimer = visibleDuration;

        // Escala la barra proporcionalmente
        float lifeRatio = Mathf.Clamp01((float)currentLives / maxLives);
        barraInterna.rectTransform.localScale = new Vector3(lifeRatio, 1f, 1f);

        // Si ya no hay vida, oculta inmediatamente
        if (currentLives <= 0)
        {
            canvasGroup.alpha = 0f;
        }
    }

    void Update()
    {
        if (canvasGroup.alpha > 0f)
        {
            hideTimer -= Time.deltaTime;
            if (hideTimer <= 0f)
            {
                canvasGroup.alpha = 0f;
            }
        }
    }
}
