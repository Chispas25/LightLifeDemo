using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class ParametrosOpciones : MonoBehaviour
{

    public UnityEngine.UI.Image bright;


    // Start is called before the first frame update
    void Start()
    {
        float brightness = PlayerPrefs.GetFloat("bright", 0.5f);
        bright.color = new Color(bright.color.r, bright.color.g, bright.color.b, brightness);

        // Aplicar volumen
        float volume = PlayerPrefs.GetFloat("volumeAudio", 0.5f);
        AudioListener.volume = volume;

        // Aplicar calidad
        int quality = PlayerPrefs.GetInt("numeroDeCalidad", 3);
        QualitySettings.SetQualityLevel(quality);

        // Aplicar resolución
        int resolutionIndex = PlayerPrefs.GetInt("numeroResolucion", 0);
        Resolution[] resolutions = Screen.resolutions;
        if (resolutionIndex >= 0 && resolutionIndex < resolutions.Length)
        {
            Resolution resolution = resolutions[resolutionIndex];
            Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        }
    }
}   