using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallShooter : MonoBehaviour
{
    public float maxForce = 5f;
    private Rigidbody2D rb;
    private Vector2 startDrag, endDrag;
    private bool isDragging = false;

    void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update() {
        /*if (Input.GetMouseButtonDown(0)) {
           // startDrag = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            isDragging = true;
        }*/

        if (Input.GetMouseButtonUp(0) && isDragging) {
            endDrag = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 force = (startDrag - endDrag);
            rb.AddForce(Vector2.ClampMagnitude(force, maxForce) * 100f);
            isDragging = false;
        }
    }

    void FixedUpdate() {
    if (rb.velocity.magnitude < 0.05f) {
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0f;
    }
}
}

