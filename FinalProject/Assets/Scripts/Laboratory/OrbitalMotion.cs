using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitalMotion : MonoBehaviour
{
    public GameObject target;
    public float speed = 0.05f;
    // Update is called once per frame
    void Update()
    {
        OrbitalProcess();
    }

    void OrbitalProcess()
    {
        transform.RotateAround(target.transform.position, Vector3.up, speed * Time.deltaTime);
    }
}
