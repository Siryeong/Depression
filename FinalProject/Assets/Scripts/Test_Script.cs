using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class Test_Script : MonoBehaviour
{
    /** serialized private **/
    [SerializeField] private TMPro.TMP_Text text;
    [SerializeField] private GameObject start_button;
    [SerializeField] private GameObject[] throw_objects;
    [SerializeField] private GameObject Boat;

    /** public **/
    public ScriptData script_data;

    /** private **/
    int index = 0;

    void Start()
    {
        LoadScript();
        displayScript();
        Invoke("ShowSkeleton", 1f); // interactable ui
    }

    void LoadScript()
    {
        string path = Path.Combine(Application.dataPath, "tutorial_script.json");
        string jsonData = File.ReadAllText(path);
        script_data = JsonUtility.FromJson<ScriptData>(jsonData);
    }

    void ShowSkeleton()
    {
        SkeletonUI.ShowHint(Player.instance.hands[1], 0);
    }

    public void displayScript()
    {
        text.text = script_data.tutorial[index];

        ControllerButtonHints.HideAllTextHints(Player.instance.hands[1]);
        start_button.SetActive(false);
        foreach (GameObject item in throw_objects)
        {
            item.SetActive(false);
        }

        if (index == 3) SkeletonUI.ShowHint(Player.instance.hands[1], 9); // snap turn
        if (index == 4) SkeletonUI.ShowHint(Player.instance.hands[1], 1); // teleport
        if (index == 6)
        {
            SkeletonUI.ShowHint(Player.instance.hands[1], 3); // trigger or grip
            foreach (GameObject item in throw_objects)
            {
                item.SetActive(true);
            }
        }

        if (script_data.tutorial[index] == "배 운전하기") Boat.SetActive(true);
        if (index == 8) SkeletonUI.ShowHint(Player.instance.hands[1], 1);
        
        if(index == script_data.tutorial.Length-1) start_button.SetActive(true);
    }

    public void NextScript()
    {
        if (index < script_data.tutorial.Length) index++;
        displayScript();
    }

    public void PrevScript()
    {
        if (index > 0) index--;
        displayScript();
    }

    public void SkipScript()
    {
        index = script_data.tutorial.Length - 1;
        displayScript();
    }

}
