using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.Extras;
using UnityEngine.Events;

public class StagePointer : MonoBehaviour
{
    [SerializeField] private GameObject stage = null;
    [SerializeField] private GameObject loadNextScene = null;
    [SerializeField] private GameObject light = null;

    public UnityEvent OnPointerClick;
    public UnityEvent OnPointerIn;
    public UnityEvent OnPointerOut;

    // Start is called before the first frame update
    private void Start()
    {
        SteamVR_LaserPointer.PointerClick += PointerClick;
        SteamVR_LaserPointer.PointerIn += PointerIn;
        SteamVR_LaserPointer.PointerOut += PointerOut;
    }

    public void PointerClick(object sender, PointerEventArgs e)
    {
        if (!e.target.gameObject.Equals(this.gameObject)) return;

        if (stage != null)
        {
            Debug.Log("PointerClick");
            loadNextScene.SetActive(true);
            if (OnPointerClick != null) OnPointerClick.Invoke();
        }
    }

    public void PointerIn(object sender, PointerEventArgs e)
    {
        if (e.target == null) return;
        if (!e.target.gameObject.Equals(this.gameObject)) return;

        if (stage != null)
        {
            Debug.Log("PointerIn");
            light.SetActive(true);
            if (OnPointerIn != null) OnPointerIn.Invoke();
        }
    }

    public void PointerOut(object sender, PointerEventArgs e)
    {
        if (e.target == null) return;
        if (!e.target.gameObject.Equals(this.gameObject)) return;

        if (stage != null)
        {
            Debug.Log("PointerOut");
            light.SetActive(false);
            if (OnPointerOut != null) OnPointerOut.Invoke();
        }
    }
}
