using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] private float Speed = 1f;
    [SerializeField] private GameObject bomb = null;

    void Start() 
    {
        Destroy(bomb, 7f);
    }

    // Update is called once per frame
    void Update()
    {
        Fire();
    }

    void Fire()
    {
        bomb.transform.Translate(Vector3.forward * Time.deltaTime * Speed);
    }

    void OnCollisionEnter(Collision other) {
        if(other.gameObject.tag == "Boat")
            Destroy(bomb);
        else 
            Stop();
    }

    public void Stop()
    {
        Speed = 0;
        Destroy(bomb, 2f);
    }
}
