using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    private Rigidbody rb;
    private float speed = 1.5f;
    
    private void Start () {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate() {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontalInput, 0f, verticalInput);
        rb.linearVelocity = direction * speed;
    }
}
