using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;



public class FondoBotones : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    public Image fondo;


    // Start is called before the first frame update
    void Start()
    {
        if (fondo != null)
            fondo.enabled = false; // Oculta el fondo al inicio

    }

    // Update is called once per frame
    void Update()
    {

    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        if (fondo != null)
            fondo.enabled = true; // Muestra la imagen
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (fondo != null)
            fondo.enabled = false; // Oculta la imagen
    }


   void OnDisable()
    {
        if (fondo != null)
            fondo.enabled = false; // Oculta fondo si el botón o panel se desactiva
    }


}
