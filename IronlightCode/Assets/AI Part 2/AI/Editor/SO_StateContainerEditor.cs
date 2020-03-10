using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace AITEST
{
    [CustomEditor(typeof(SO_StateContainer))]
    public class SO_StateContainerEditor : Editor
    {
        SO_StateContainer sc;

        private void OnEnable()
        {
            sc = (SO_StateContainer)target;
        }

        public override void OnInspectorGUI()
        {
            EditorGUILayout.BeginVertical();

            sc.name = EditorGUILayout.TextField("Name", sc.name);
            EditorGUILayout.Space();

            sc.numberOfStates = EditorGUILayout.IntField("Number of States", sc.numberOfStates);

            if (sc.soStates == null)
            {
                sc.soStates = new SO_State[1];
            }
            else if (sc.soStates.Length != sc.numberOfStates)
            {
                sc.soStates = new SO_State[sc.numberOfStates];
            }


            for (int i = 0; i < sc.numberOfStates; i++)
            {
                sc.soStates[i] = (SO_State)EditorGUILayout.ObjectField(sc.soStates[i], typeof(SO_State), true);
            }


            EditorGUILayout.Space();

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Number Of Transitions", GUILayout.Width(150));
            sc.numberOfTransitions = EditorGUILayout.IntField(sc.numberOfTransitions);
            EditorGUILayout.EndHorizontal();


            //ensure not null
            if (sc.conditionContainers == null)
            {
                sc.conditionContainers = new SO_ConditionContainer[sc.numberOfTransitions];
            }
            else if (sc.numberOfTransitions != sc.conditionContainers.Length)
            {
                sc.conditionContainers = new SO_ConditionContainer[sc.numberOfTransitions];
            }

            if(sc.stateContainers == null)
            {
                sc.stateContainers = new SO_StateContainer[sc.numberOfTransitions];
            }
            else if(sc.numberOfTransitions != sc.stateContainers.Length)
            {
                sc.stateContainers = new SO_StateContainer[sc.numberOfTransitions];
            }

            //inspector conditionContainers
            for (int i = 0; i < sc.numberOfTransitions; i++)
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Transition Conditions", GUILayout.Width(120));
                sc.conditionContainers[i] = (SO_ConditionContainer)EditorGUILayout.ObjectField(sc.conditionContainers[i], typeof(SO_ConditionContainer), true);
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Transition State", GUILayout.Width(120));
                sc.stateContainers[i] = (SO_StateContainer)EditorGUILayout.ObjectField(sc.stateContainers[i], typeof(SO_StateContainer), true);
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.Space();
            }


            EditorGUILayout.EndVertical();
            EditorUtility.SetDirty(sc);
        }
    }
}

