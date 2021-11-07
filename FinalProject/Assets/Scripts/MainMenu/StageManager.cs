using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class StageManager : MonoBehaviour
{
    [SerializeField] private GameObject activatingLevel = null;
    [SerializeField] private GameObject loadNextScene = null;

    public void OnStageSelected(string stage)
    {
        Debug.Log("Stage Selected");
        GameManager.Instance.SetStage(stage);
        activatingLevel.SetActive(true);
    }

    public void OnLevelSelected(string level)
    {
        Debug.Log("Level Clicked");
        GameManager.Instance.SetLevel(level);
        loadNextScene.SetActive(true);
        activatingLevel.SetActive(false);
    }
}
