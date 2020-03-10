using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace AITEST
{
    [CustomEditor(typeof(SO_State))]
    public class SO_StateEditor : Editor
    {
        SO_State s;        

        private void OnEnable()
        {
            s = (SO_State)target;
        }

        public override void OnInspectorGUI()
        {
            EditorGUILayout.BeginVertical();

            EditorGUILayout.BeginHorizontal();
            s.state = (StateEnum)EditorGUILayout.EnumPopup("State", s.state);
            EditorGUILayout.EndHorizontal();

            //inspector state variables
            Variables();

            EditorGUILayout.EndVertical();
            EditorUtility.SetDirty(s);
        }


        //switch for state type and variables
        private void Variables()
        {
            //check array isn't null
            if (s.variables == null)
            {
                s.variables = new float[1];
            }

            switch (s.state)
            {
                case StateEnum.None:
                    break;
                case StateEnum.Alert:
                    if(s.variables.Length != 4)
                    {
                        s.variables = new float[4];
                    }

                    s.variables[0] = EditorGUILayout.FloatField("SpeedMin", s.variables[0]);
                    s.variables[1] = EditorGUILayout.FloatField("SpeedMax", s.variables[1]);
                    s.variables[2] = EditorGUILayout.FloatField("RotMin", s.variables[2]);
                    s.variables[3] = EditorGUILayout.FloatField("RotMax", s.variables[3]);

                    break;
                case StateEnum.FocusOn:
                    if (s.variables.Length != 1)
                    {
                        s.variables = new float[1];
                    }

                    s.variables[0] = EditorGUILayout.Slider("RotationSpeed", s.variables[0], 0f, 5f);

                    break;
                case StateEnum.Projectile:
                    s.projectile = (GameObject)EditorGUILayout.ObjectField("Projectile", s.projectile, typeof(GameObject), true);
                    s.soProjectile = (SO_Projectile)EditorGUILayout.ObjectField("SO_Projectile", s.soProjectile, typeof(SO_Projectile), true);
                    s.variables[0] = EditorGUILayout.FloatField("Timer", s.variables[0]);
                    s.variables[1] = EditorGUILayout.FloatField("Delay", s.variables[1]);
                    s.launcher = (Transform)EditorGUILayout.ObjectField("Launcher", s.launcher, typeof(Transform), true);
                    break;
                case StateEnum.MoveForward:
                    s.variables[0] = EditorGUILayout.FloatField("Speed", s.variables[0]);
                    break;
            }
        }
    }
}


