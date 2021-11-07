using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class LinearBoatMovement : MonoBehaviour
{
    public LinearMapping lm;

    private Rigidbody rb;

    void Start() {
        rb = GetComponent<Rigidbody>();    
    }

    // Update is called once per frame
    void Update()
    {
        move();
    }

    void move()
    {
        float dir = (lm.value -0.5f) * 10f;
        rb.AddRelativeForce(Vector3.right * dir);
    }
}
