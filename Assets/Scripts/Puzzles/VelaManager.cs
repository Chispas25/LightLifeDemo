using UnityEngine;

public class VelaManager : MonoBehaviour
{
    public static VelaManager Instance;

    public Vela[] candles;
    public GameObject rewardObject; // Objeto que se activa cuando todas las velas están encendidas

    private void Awake()
    {
        Instance = this;
    }

    public void CheckAllCandles()
    {
        foreach (var candle in candles)
        {
            if (!candle.isLit)
                return;
        }

        Debug.Log("¡Todas las velas encendidas!");
        if (rewardObject != null)
            rewardObject.SetActive(true);
    }
}
