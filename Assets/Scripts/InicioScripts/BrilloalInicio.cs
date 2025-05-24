using UnityEngine;
using UnityEngine.UI;

public class GlobalBrillo : MonoBehaviour
{
    public Image panelBrillo;

    void Start()
    {
        float brillo = GameSettings.Instance.brillo;
        panelBrillo.color = new Color(0, 0, 0, brillo);
    }
}
