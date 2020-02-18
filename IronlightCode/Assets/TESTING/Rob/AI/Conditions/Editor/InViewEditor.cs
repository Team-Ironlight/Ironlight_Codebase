using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;


[CustomEditor(typeof(InViewCondition))]
public class InViewEditor : Editor
{
    InViewCondition iv;

    private void OnEnable()
    {
        iv = (InViewCondition)target;        
    }

    private void OnSceneGUI()
    {
        Quaternion q = iv.transform.rotation;
        q *= Quaternion.AngleAxis(-1 * iv.viewAngle / 2f, Vector3.up);
        Vector3 targetForward = q * Vector3.forward;
        Handles.DrawSolidArc(iv.transform.position, Vector3.up, targetForward, iv.viewAngle, 5f);
    }
}
