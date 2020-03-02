using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

namespace ROFO
{
    [CustomEditor(typeof(RangeCondition))]
    public class RangeEditor : Editor
    {
        RangeCondition r;

        private void OnEnable()
        {
            r = (RangeCondition)target;
        }

        private void OnSceneGUI()
        {
            Handles.DrawWireDisc(r.transform.position, Vector3.up, r.range);
        }
    }
}
