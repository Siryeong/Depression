using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Valve.VR;

public class MeditationManager : MonoBehaviour
{
    [SerializeField] private GameObject loadNextScene = null;
    private SteamVR_LoadLevel loadLevel;


    private void Start()
    {
        Debug.Log("MeditationManager started");
        loadLevel = loadNextScene.GetComponent<SteamVR_LoadLevel>();
        loadLevel.SetStageName(GameManager.Instance.GetStage());
    }

    private void OnMeditationEnd() {
        Debug.Log("Meditation Scene Ends");
        loadNextScene.SetActive(true);
        LinkConnection.Instance.SetBaseline();
    }
}
