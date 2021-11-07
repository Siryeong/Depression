using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Valve.VR;

public class SteamVRInputManager : MonoBehaviour
{

    public SteamVR_Action_Boolean menu_button;
    public UnityEvent menu_button_down;

    // Update is called once per frame
    void Update()
    {
        
        if(menu_button.GetStateDown(SteamVR_Input_Sources.Any)){
            if(Time.timeScale == 1) Time.timeScale = 0;
            else Time.timeScale = 1;
        }
        
    }
}
