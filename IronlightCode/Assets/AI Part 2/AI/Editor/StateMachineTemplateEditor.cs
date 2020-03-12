using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace AITEST
{
    [CustomEditor(typeof(StateMachineTemplate))]
    public class StateMachineTemplateEditor : Editor
    {
        StateMachineTemplate sm;

        private void OnEnable()
        {
            sm = (StateMachineTemplate)target;
        }

        public override void OnInspectorGUI()
        {
            V2();
        }

        //version 2 with SO_StateContainers
        private void V2()
        {
            EditorGUILayout.BeginVertical();
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Default State", GUILayout.Width(100));
            sm.startState = (SO_StateContainer)EditorGUILayout.ObjectField(sm.startState, typeof(SO_StateContainer), true);
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Number of States", GUILayout.Width(150));
            sm.numberOfContainers = EditorGUILayout.IntField(sm.numberOfContainers);
            EditorGUILayout.EndHorizontal();

            if (sm.stateContainers == null)
            {
                sm.stateContainers = new SO_StateContainer[1];
            }
            else if (sm.numberOfContainers != sm.stateContainers.Length)
            {
                sm.stateContainers = new SO_StateContainer[sm.numberOfContainers];
            }

            for (int i = 0; i < sm.stateContainers.Length; i++)
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("States", GUILayout.Width(80));
                sm.stateContainers[i] = (SO_StateContainer)EditorGUILayout.ObjectField(sm.stateContainers[i], typeof(SO_StateContainer), true);
                EditorGUILayout.EndHorizontal();
            }

            EditorGUILayout.EndVertical();
            EditorUtility.SetDirty(sm);
        }
    }
}


