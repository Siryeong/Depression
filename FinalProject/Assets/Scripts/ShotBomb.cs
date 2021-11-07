using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotBomb : MonoBehaviour
{
    [SerializeField] private GameObject prefab = null;
    [SerializeField] private GameObject[] Bullets_UI = null;
    [SerializeField] private int count = 0;

    public void shotBomb()
    {
        if(count == 0) return;
        GameObject spawn = GameObject.Instantiate<GameObject>(prefab);
        spawn.transform.position = transform.position;
        spawn.transform.rotation = transform.rotation;
        Bullets_UI[--count].SetActive(false);
    }

    public void getBomb()
    {
        if(count >= 3) return;
        Bullets_UI[count++].SetActive(true);
    }
}
