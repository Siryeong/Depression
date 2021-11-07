using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlingShot : MonoBehaviour
{
    [SerializeField] private float force = 1f;
    private Rigidbody rb;
    private Vector3 initial_posistion;

    void Start() {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(Vector3.up);
    }

    public void pickedUP()
    {
        initial_posistion = transform.position;
    }

    public void shot()
    {
        rb.AddForce((initial_posistion - transform.position) * force * 1000);
    }
}
