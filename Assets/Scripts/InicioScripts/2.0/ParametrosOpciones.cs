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
    }
}   