using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class Test_RestartButton : MonoBehaviour
{
    void Start() {
        // init
        //gameObject.SetActive(false);
    }

    public void DisplayPanel()
    {
        gameObject.SetActive(true);
        transform.position = Player.instance.headCollider.gameObject.transform.position;
        transform.rotation = Player.instance.headCollider.gameObject.transform.rotation;
        transform.Translate(Vector3.forward*0.4f, Space.Self);
    }

    public void PauseGame()
    {
        gameObject.SetActive(true);
        transform.position = Player.instance.headCollider.gameObject.transform.position;
        transform.rotation = Player.instance.headCollider.gameObject.transform.rotation;
        transform.Translate(Vector3.forward*0.4f, Space.Self);
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1f;
    }
}
