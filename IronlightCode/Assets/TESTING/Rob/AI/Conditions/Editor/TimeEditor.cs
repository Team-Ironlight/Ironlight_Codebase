using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(TimeCondition))]
public class TimeEditor : Editor
{
    TimeCondition t;

    private void OnEnable()
    {
        t = (TimeCondition)target;
    }

    private void OnSceneGUI()
    {
        
    }
}
