using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using Valve.VR.Extras;

public class LaserPointerInteraction : MonoBehaviour
{
    public Image button;
    //public GameObject button;
    public Color NormalColor = Color.white;
    public Color HighlitedColor = Color.gray;
    public Color PressedColor = Color.black;

    public UnityEvent OnPointerClick;
    public UnityEvent OnPointerIn;
    public UnityEvent OnPointerOut;

    MeshRenderer m_Renderer;
    Color m_OriginalColor;

    // Start is called before the first frame update
    void Start()
    {
        if(button == null){
           button = gameObject.GetComponent<Image>();
        }

        SteamVR_LaserPointer.PointerClick += PointerClick;
        SteamVR_LaserPointer.PointerIn += PointerIn;
        SteamVR_LaserPointer.PointerOut += PointerOut;
        SteamVR_LaserPointer.PointerDown += PointerDown;
        SteamVR_LaserPointer.PointerUp += PointerUp;

        //m_Renderer = GetComponent<MeshRenderer>();
        //m_OriginalColor = m_Renderer.material.color;
    }

    public void PointerClick(object sender, PointerEventArgs e)
    {
        if (!e.target.gameObject.Equals(this.gameObject)) return;

        if (button != null)
        {
            Debug.Log("PointerClick");
            button.color = PressedColor;
            if (OnPointerClick != null) OnPointerClick.Invoke();
        }
    }

    public void PointerIn(object sender, PointerEventArgs e)
    {
        if (e.target == null) return;
        if (!e.target.gameObject.Equals(this.gameObject)) return;

        if (button != null)
        {
            button.color = HighlitedColor;
            if (OnPointerIn != null) OnPointerIn.Invoke();
        }
    }

    public void PointerOut(object sender, PointerEventArgs e)
    {
        if (e.target == null) return;
        if (!e.target.gameObject.Equals(this.gameObject)) return;

        if (button != null)
        {
            button.color = NormalColor;
            if (OnPointerOut != null) OnPointerOut.Invoke();
        }
    }

    public void PointerDown(object sender, PointerEventArgs e)
    {
        if (e.target == null) return;
        if (!e.target.gameObject.Equals(this.gameObject)) return;

        if (button != null)
        {
            button.color = PressedColor;
            if (OnPointerOut != null) OnPointerOut.Invoke();
        }
    }

    public void PointerUp(object sender, PointerEventArgs e)
    {
        if (e.target == null) return;
        if (!e.target.gameObject.Equals(this.gameObject)) return;

        if (button != null)
        {
            button.color = HighlitedColor;
            if (OnPointerOut != null) OnPointerOut.Invoke();
        }
    }

}
