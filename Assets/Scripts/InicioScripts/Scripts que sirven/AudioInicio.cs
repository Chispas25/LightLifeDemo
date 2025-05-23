using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class AudioInicio : MonoBehaviour
{
    public Slider slider;
    public float sliderAudio;

    public GameObject imagenMute;      // Imagen o botón de mute
    public GameObject imagenSonido;    // Imagen de sonido activo

    private bool estaMuteado = false;
    private float volumenAntesDeMutear = 0.5f;

    void Start()
    {
        float volumenGuardado = PlayerPrefs.GetFloat("volumenAudio", 0.5f);
        slider.value = volumenGuardado;
        volumenAntesDeMutear = volumenGuardado;

        AudioListener.volume = volumenGuardado;
        estaMuteado = volumenGuardado == 0;
        ActualizarImagenes();
    }


    // Update is called once per frame
    void Update()
    {

    }


    // Llamado automáticamente por el slider al cambiar
    public void ChangeSlider(float valor)
    {
        sliderAudio = valor;
        AudioListener.volume = valor;
        PlayerPrefs.SetFloat("volumenAudio", valor);

        estaMuteado = valor == 0;

        if (!estaMuteado)
        {
            volumenAntesDeMutear = valor;
        }

        ActualizarImagenes();
    }

    // Llamado por el botón o imagen de mute
    public void AlternarMute()
    {
        estaMuteado = !estaMuteado;

        if (estaMuteado)
        {
            volumenAntesDeMutear = slider.value > 0 ? slider.value : volumenAntesDeMutear;
            slider.value = 0f;
        }
        else
        {
            slider.value = volumenAntesDeMutear;
        }

        AudioListener.volume = slider.value;
        PlayerPrefs.SetFloat("volumenAudio", slider.value);
        ActualizarImagenes();
    }

    void ActualizarImagenes()
    {
        if (imagenMute != null) imagenMute.SetActive(estaMuteado);
        if (imagenSonido != null) imagenSonido.SetActive(!estaMuteado);
    }
}
