using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Valve.VR.InteractionSystem;

public class StageClearManager : MonoBehaviour
{
    [SerializeField] private float time_over = 180f;

    public UnityEvent clear_event;
    public UnityEvent gameOver_event;

    void Start()
    {
        // init
        gameObject.SetActive(true);
        StartCoroutine(GameOverTimmer());
    }

    void OnTriggerEnter(Collider other) {
        // game clear flow
        if(other.gameObject.tag == "Boat")
        {
            Debug.Log("Clear");
            stopAll();
            clear_event.Invoke();
        }
    }

    IEnumerator GameOverTimmer()
    {
        // timmer start
        while(time_over > 0){
            time_over -= 0.1f;
            yield return new WaitForSeconds(0.1f);
        }

        // gameover flow
        gameOver_event.Invoke();
        stopAll();
    }

    public void stopAll()
    {
        Player.instance.transform.SetParent(null);
        StopAllCoroutines();
    }

}
