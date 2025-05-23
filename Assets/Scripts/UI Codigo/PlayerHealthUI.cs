using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHealthUI : MonoBehaviour
{
    public PlayerHealthManager playerHealth;
    public Image healthBarFill;
    public CanvasGroup canvasGroup;

    public float fadeDelay = 2f;
    public float fadeDuration = 1f;

    private Coroutine fadeCoroutine;

    void Awake()
    {
        // Si no está asignado, busca el CanvasGroup en el mismo GameObject
        if (canvasGroup == null)
        {
            canvasGroup = GetComponent<CanvasGroup>();

            // Si no lo encuentra, busca en los hijos
            if (canvasGroup == null)
                canvasGroup = GetComponentInChildren<CanvasGroup>();
        }

        // Opcional: si no se encuentra, lanza una advertencia
        if (canvasGroup == null)
            Debug.LogWarning("No se encontró CanvasGroup en el GameObject o sus hijos.");
    }

    void Update()
    {
        if (playerHealth != null && healthBarFill != null)
        {
            healthBarFill.fillAmount = playerHealth.GetCurrentLives() / 3f;
        }
    }

    public void ShowDamageUI()
    {
        Debug.Log("Mostrando UI de daño"); // <--- Añade este log
        if (fadeCoroutine != null)
            StopCoroutine(fadeCoroutine);

        canvasGroup.alpha = 1f;
        fadeCoroutine = StartCoroutine(FadeOutAfterDelay());
    }

    IEnumerator FadeOutAfterDelay()
    {
        yield return new WaitForSeconds(fadeDelay);

        float elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(1f, 0f, elapsed / fadeDuration);
            yield return null;
        }
        canvasGroup.alpha = 0f;
    }
}
