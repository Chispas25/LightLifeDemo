using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Vela : MonoBehaviour
{
    public bool isLit = false;
    public GameObject flameEffect;

    private void Start()
    {
        SetLit(false);
    }

    public void TryLightCandle(Transform player)
    {
        if (isLit) return;

        Light2D playerLight = player.GetComponentInChildren<Light2D>();
        if (playerLight != null && playerLight.intensity > 0.1f)
        {
            SetLit(true);
            VelaManager.Instance.CheckAllCandles();
        }
    }

    public void SetLit(bool state)
    {
        isLit = state;
        if (flameEffect != null)
            flameEffect.SetActive(state);
    }

    //Añadir este metodo a los jugadores o a un sistema de interaccion

    /*private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Candle") && Input.GetKeyDown(KeyCode.E))
        {
        Candle candle = other.GetComponent<Candle>();
        if (candle != null)
            candle.TryLightCandle(transform);
        }
    }*/


}
