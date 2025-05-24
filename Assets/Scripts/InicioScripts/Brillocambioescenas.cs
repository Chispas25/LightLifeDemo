using UnityEngine;
using UnityEngine.UI;

public class InicioEscena : MonoBehaviour
{
    public Image panelBrilloEnEscena;
    public Slider sliderBrilloOpcional; // si tienes uno visible

    void Start()
    {
      /*  if (BrilloManager.instancia != null)
        {
            BrilloManager.instancia.ActualizarReferencias(panelBrilloEnEscena, sliderBrilloOpcional);
        }*/
    }
}