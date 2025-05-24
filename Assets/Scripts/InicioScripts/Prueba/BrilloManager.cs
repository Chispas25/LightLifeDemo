using UnityEngine;
using UnityEngine.UI;

public class Brillito : MonoBehaviour
{
    public Image panelBrillo;
    public Slider slider;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        float savedBrillo = PlayerPrefs.GetFloat("brillo", 0.9f);
        slider.value = savedBrillo;
        AplicarBrillo(savedBrillo);
    }

    public void CambiarBrillo(float valor)
    {
        PlayerPrefs.SetFloat("brillo", valor);
        AplicarBrillo(valor);
    }

    private void AplicarBrillo(float valor)
    {
        if (panelBrillo != null)
        {
            Color c = panelBrillo.color;
            c.a = valor;
            panelBrillo.color = c;
        }
    }
}