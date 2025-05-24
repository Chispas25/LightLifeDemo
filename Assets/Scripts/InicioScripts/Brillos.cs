using UnityEngine;
using UnityEngine.UI;

public class GameSettings : MonoBehaviour
{
    public static GameSettings Instance;

    [Header("Brillo")]
    public Slider slider; // Asignar desde el Inspector
    public Image panelBrillo; // Asignar desde el Inspector
    public float sliderValue = 0.5f;
    public float brillo = 0.5f;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(this.gameObject);
        LoadSettings();
    }

    void Start()
    {
        sliderValue = brillo;
        if (slider != null)
            slider.value = brillo;

        if (panelBrillo != null)
            panelBrillo.color = new Color(panelBrillo.color.r, panelBrillo.color.g, panelBrillo.color.b, brillo);
    }

    public void SaveSettings()
    {
        PlayerPrefs.SetFloat("brillo", brillo);
        PlayerPrefs.Save();
    }

    public void LoadSettings()
    {
        brillo = PlayerPrefs.GetFloat("brillo", 0.5f);
    }

    public void ChangeSlider(float valor)
    {
        sliderValue = valor;
        brillo = valor;
        SaveSettings();

        if (panelBrillo != null)
            panelBrillo.color = new Color(panelBrillo.color.r, panelBrillo.color.g, panelBrillo.color.b, valor);
    }
}