using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Coin : MonoBehaviour
{
    public UnityEvent _getCoin;

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Boat")
        {
            Debug.Log("get coin");
            GetCoin();
            Destroy(gameObject);
        }
    }

    private void GetCoin()
    {
        _getCoin.Invoke();
    }
}
