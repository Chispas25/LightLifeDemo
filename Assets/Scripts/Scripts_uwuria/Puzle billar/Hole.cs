using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hole : MonoBehaviour
{
    public PuzzleManager puzzleManager;

    void Start() {
    puzzleManager = FindObjectOfType<PuzzleManager>();
    if (puzzleManager == null) {
        Debug.LogError("PuzzleManager no encontrado en la escena.");
    } else {
        Debug.Log("PuzzleManager encontrado correctamente.");
    }
}

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Ball")) {
            other.GetComponent<Rigidbody2D>().simulated = false;

            if (puzzleManager != null) {
                puzzleManager.BolaMetida();
            }
        }
    }
}