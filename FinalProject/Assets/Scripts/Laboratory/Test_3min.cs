using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Test_3min : MonoBehaviour
{
    public Text txt = null;
    public float sec = 180f;

    void OnEnable() {
        Invoke("samboonTimmer", sec);
        StartCoroutine(timePannel());
    }

    void samboonTimmer()
    {
        gameObject.SetActive(false);
    }

    IEnumerator timePannel()
    {
        float s = sec;
        while(s > 0){
            s -= .1f;
            txt.text = s + "sec left";
            yield return new WaitForSeconds(0.1f);
        }
        yield return null;
    }
}
