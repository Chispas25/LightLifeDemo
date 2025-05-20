using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneTransitionManager : MonoBehaviour
{
    public string sceneToLoad;         // Nombre de la escena a cargar (ej: "PrimeraPlanta")
    public string sceneToUnload;       // Nombre de la escena actual a descargar (ej: "PlantaBaja")
    public Transform playerSpawnPoint; // Punto donde aparecerá el jugador

    public GameObject player;          // Referencia al jugador (puede vincularse en editor)

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(TransitionScene());
        }
    }

    private IEnumerator TransitionScene()
    {
        // Cargar escena nueva aditivamente
        AsyncOperation loadOp = SceneManager.LoadSceneAsync(sceneToLoad, LoadSceneMode.Additive);
        while (!loadOp.isDone) yield return null;

        // Mover jugador al nuevo punto
        player.transform.position = playerSpawnPoint.position;

        // Descargar escena anterior si se desea liberar memoria
        if (!string.IsNullOrEmpty(sceneToUnload))
        {
            AsyncOperation unloadOp = SceneManager.UnloadSceneAsync(sceneToUnload);
            while (!unloadOp.isDone) yield return null;
        }
    }
}
