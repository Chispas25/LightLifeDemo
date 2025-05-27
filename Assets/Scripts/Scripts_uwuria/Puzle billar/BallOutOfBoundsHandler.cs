using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallOutOfBoundsHandler : MonoBehaviour
{
    // Start is called before the first frame update
    public float minX = -8f, maxX = 8f, minY = -5f, maxY = 5f;

    void Update() {
        GameObject[] balls = GameObject.FindGameObjectsWithTag("Billar");
        foreach (GameObject ball in balls) {
            Vector2 pos = ball.transform.position;
            if (pos.x < minX || pos.x > maxX || pos.y < minY || pos.y > maxY) {
                Rigidbody2D rb = ball.GetComponent<Rigidbody2D>();
                if (rb != null && rb.simulated) {
                    rb.velocity = Vector2.zero;
                    rb.angularVelocity = 0f;
                    rb.simulated = false; // Detener física
                    Debug.Log("Bola detenida fuera de la mesa.");
                }
            }
        }
    }
}
