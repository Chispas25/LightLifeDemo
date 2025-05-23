using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public Slider slider;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        float volumen = PlayerPrefs.GetFloat("volumen", 0.5f);
        slider.value = volumen;
        AudioListener.volume = volumen;
    }

    public void CambiarVolumen(float valor)
    {
        PlayerPrefs.SetFloat("volumen", valor);
        AudioListener.volume = valor;
    }
}