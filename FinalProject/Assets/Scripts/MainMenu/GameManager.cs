using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static string Stage = "FirstStage";
    private static string Level = "Easy";

    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        #region Singleton
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        #endregion
    }

    public void SetStage(string stage)
    {
        Stage = stage;
        Debug.Log("SetStage: " + Stage);
    }

    public string GetStage()
    {
        return Stage;
    }

    public void SetLevel(string level)
    {
        Level = level;
        Debug.Log("SetLevel: " + Level);
    }

    public string GetLevel()
    {
        return Level;
    }
}
