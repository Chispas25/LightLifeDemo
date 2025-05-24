using UnityEngine;
using UnityEngine.UI;

public class AjustesManager : MonoBehaviour
{
    public static AjustesManager instancia;

    public float volumen = 1f;
    public float brillo = 0.9f;

    void Awake()
    {
        if (instancia != null && instancia != this)
        {
            Destroy(gameObject);
            return;
        }

        instancia = this;
        DontDestroyOnLoad(gameObject);

        // Cargar ajustes guardados
        volumen = PlayerPrefs.GetFloat("volumen", 1f);
        brillo = PlayerPrefs.GetFloat("brillo", 0.9f);
    }

    public void CambiarVolumen(float v)
    {
        volumen = v;
        PlayerPrefs.SetFloat("volumen", v);
        AudioListener.volume = v;
    }

    public void CambiarBrillo(float b, Image panelBrillo)
    {
        brillo = b;
        PlayerPrefs.SetFloat("brillo", b);

        if (panelBrillo != null)
        {
            Color c = panelBrillo.color;
            c.a = b;
            panelBrillo.color = c;
        }
    }
}
