using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace ROFO
{
    [CustomEditor(typeof(ConditionContainer))]
    public class ConditionContainerEditor : Editor
    {
        ConditionContainer cc;

        private void OnEnable()
        {
            cc = (ConditionContainer)target;
        }

        public override void OnInspectorGUI()
        {
            //base.OnInspectorGUI();

            EditorGUILayout.BeginVertical();

            cc.rank = EditorGUILayout.IntField("Rank", cc.rank);
            cc.returnState = (State)EditorGUILayout.EnumPopup("ReturnState", cc.returnState);

            //for specific things that need more information, see below
            ExtraVariables();

            cc.numberOfConditions = EditorGUILayout.IntField("Conditions", cc.numberOfConditions);

            if (cc.conditions == null)
            {
                cc.conditions = new ICondition[0];
            }
            if (cc.numberOfConditions != cc.conditions.Length)
            {
                cc.conditions = new ICondition[cc.numberOfConditions];
            }

            for (int i = 0; i < cc.conditions.Length; i++)
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField((i + 1).ToString(), GUILayout.Width(20));
                cc.conditions[i] = (ICondition)EditorGUILayout.ObjectField(cc.conditions[i], typeof(ICondition), true);
                EditorGUILayout.EndHorizontal();
            }

            EditorGUILayout.EndVertical();

            //if(GUI.changed)
            //{
            //    EditorUtility.SetDirty(cc);
            //    EditorSceneManager.MarkSceneDirty(cc.gameObject.scene);
            //}
        }

        private void ExtraVariables()
        {
            //expose variables for certain states
            if (cc.returnState == State.Projectile)
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Launcher", GUILayout.Width(80));
                cc.launcher = (GameObject)EditorGUILayout.ObjectField(cc.launcher, typeof(GameObject), true);
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Projectile", GUILayout.Width(80));
                cc.projectile = (GameObject)EditorGUILayout.ObjectField(cc.projectile, typeof(GameObject), true);
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("LaunchTime", GUILayout.Width(80));
                cc.launchTime = EditorGUILayout.FloatField(cc.launchTime);
                EditorGUILayout.EndHorizontal();
            }
            //set a spot for grid to be passed in
            else if (cc.returnState == State.PF_GoTo ||
                    cc.returnState == State.PF_Wander)
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Grid", GUILayout.Width(80));
                cc.gameObjectHolder = (GameObject)EditorGUILayout.ObjectField(cc.gameObjectHolder, typeof(GameObject), true);
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Parent", GUILayout.Width(80));
                cc.gameObjectHolder2 = (GameObject)EditorGUILayout.ObjectField(cc.gameObjectHolder2, typeof(GameObject), true);
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Speed", GUILayout.Width(80));
                cc.launchTime = EditorGUILayout.FloatField(cc.launchTime);
                EditorGUILayout.EndHorizontal();
            }
        }
    }
}
