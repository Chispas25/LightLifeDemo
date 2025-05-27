using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallReactivator : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody2D rb;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    void OnMouseDown() {
        if (!rb.simulated) {
            rb.simulated = true;
            Debug.Log("Bola reactivada por el jugador.");
        }
    }
}
