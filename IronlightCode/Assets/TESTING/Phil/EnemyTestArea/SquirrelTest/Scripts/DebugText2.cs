using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugText2 : MonoBehaviour
{
    private string MessageYou = "";
    public GUIStyle myGUIStyle;

    void OnGUI()
    {
        // Add here any debug text that might be helpful for you
        GUI.Label(new Rect(10, 50, 100, 20), "INFO 3 - AW means ATTACK + WANDER := " + MessageYou, myGUIStyle);
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        MessageYou = "Green Capsule, WireSphere Green.";
    }
}
