using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace AITEST
{
    [CustomEditor(typeof(AnimatorStateMachine))]
    public class AnimatorEditor : Editor
    {
        AnimatorStateMachine asm;

        private void OnEnable()
        {
            asm = (AnimatorStateMachine)target;
        }

        public override void OnInspectorGUI()
        {
            //base.OnInspectorGUI();
            EditorGUILayout.BeginVertical();
            asm.anim = (Animator)EditorGUILayout.ObjectField("Animator", asm.anim, typeof(Animator), true);
            asm.stateMachine = (GameObject)EditorGUILayout.ObjectField("State Machine", asm.stateMachine, typeof(GameObject), true);
            EditorGUILayout.Space();

            asm.numberOfAnimations = EditorGUILayout.IntField("NumberOfAnimations", asm.numberOfAnimations);

            //ensure all arrays are of same length
            if(asm.numberOfAnimations != asm.states.Length ||
               asm.numberOfAnimations != asm.bools.Length ||
               asm.numberOfAnimations != asm.animatorVariables.Length)
            {
                asm.states = new SO_StateContainer[asm.numberOfAnimations];
                asm.bools = new bool[asm.numberOfAnimations];
                asm.animatorVariables = new string[asm.numberOfAnimations];
            }

            for (int i = 0; i < asm.numberOfAnimations; i++)
            {
                EditorGUILayout.Space();
                asm.states[i] = (SO_StateContainer)EditorGUILayout.ObjectField("State", asm.states[i], typeof(SO_StateContainer), true);
                asm.animatorVariables[i] = EditorGUILayout.TextField("Animator Variable", asm.animatorVariables[i]);
                asm.bools[i] = EditorGUILayout.Toggle("Variable Value", asm.bools[i]);
            }

            EditorGUILayout.EndVertical();
            EditorUtility.SetDirty(asm);
        }
    }
}

