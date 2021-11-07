using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaselineSceneManager : MonoBehaviour
{
    [SerializeField] private GameObject loadNextLevel = null;
    [SerializeField] private TMPro.TMP_Text text = null;
    
    private float time_left = 0f;
    private bool isRun = false;

    // Start is called before the first frame update
    void Start()
    {
        text.text = "hello!";
        time_left = 60f;
    }

    // Update is called once per frame
    void Update()
    {
        if(isRun)
        {
            text.text = string.Format("{0:N} sec left", time_left);
            time_left -= Time.deltaTime;
            if(time_left < 0) isRun = false;
        }
    }

    public void RunTimmer()
    {
        isRun = true;
        time_left = 60f;
    }

    void EndBaselineSetting()
    {
        LinkConnection.Instance.SetBaseline();
        text.text = "Fin.";
        loadNextLevel.SetActive(true);
    }

    

}
