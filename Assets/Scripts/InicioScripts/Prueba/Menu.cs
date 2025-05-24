using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public GameObject panelInicio;
    public GameObject panelOpciones;

    void Start()
    {
        panelInicio.SetActive(true);
        panelOpciones.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            bool opcionesActivas = panelOpciones.activeSelf;
            panelOpciones.SetActive(!opcionesActivas);
            panelInicio.SetActive(opcionesActivas);
        }
    }

    public void Jugar(string escenaJuego)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(escenaJuego);
    }

    public void Salir()
    {
        Application.Quit();
    }

    public void MostrarOpciones()
    {
        panelInicio.SetActive(false);
        panelOpciones.SetActive(true);
    }

    public void VolverAlInicio()
    {
        panelOpciones.SetActive(false);
        panelInicio.SetActive(true);
    }
}
