using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugText1 : MonoBehaviour
{
    private string MessageYou = "";
    public GUIStyle myGUIStyle;

    void OnGUI()
    {
        // Add here any debug text that might be helpful for you
        GUI.Label(new Rect(9, 30, 100, 20), "INFO 2 - AP means ATTACK + PATROL := " + MessageYou, myGUIStyle);
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        MessageYou = "Red Capsule, WireSphere Red + Yellow.";
    }
}
