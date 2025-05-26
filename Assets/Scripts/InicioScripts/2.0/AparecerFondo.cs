using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AparecerFondo : MonoBehaviour
{

    public GameObject pauseScreen;
    private bool isPaused = false;



    // Start is called before the first frame update
    void Start()
    {

    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = !isPaused;
            PauseGame(isPaused);
        }
    }

public void PauseGame(bool status)
{
    pauseScreen.SetActive(status); // Activa/desactiva el fondo, título, botones, etc.

    if (status)
        Time.timeScale = 0f; // Pausar juego
    else
        Time.timeScale = 1f; // Reanudar juego
}




}
