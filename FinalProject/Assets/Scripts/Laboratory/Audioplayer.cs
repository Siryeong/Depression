using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Audioplayer : MonoBehaviour
{
    static public Audioplayer instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

}
