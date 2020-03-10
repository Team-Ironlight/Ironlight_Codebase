using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace AITEST
{
    [CustomEditor(typeof(SO_ConditionContainer))]
    public class SO_ConditionContainerEditor : Editor
    {
        SO_ConditionContainer cc;

        private void OnEnable()
        {
            cc = (SO_ConditionContainer)target;
        }

        public override void OnInspectorGUI()
        {
            EditorGUILayout.BeginVertical();

            cc.name = EditorGUILayout.TextField("Name", cc.name);

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Number Of Conditions", GUILayout.Width(150));
            cc.numberOfConditions = EditorGUILayout.IntField(cc.numberOfConditions);
            EditorGUILayout.EndHorizontal();
            
            EditorGUILayout.LabelField("Conditions");

            if (cc.conditionEnums == null)
            {
                cc.conditionEnums = new ConditionEnum[cc.numberOfConditions];
                cc.variables = new float[cc.numberOfConditions * 3];
            }
            else if(cc.numberOfConditions != cc.conditionEnums.Length)
            {
                cc.conditionEnums = new ConditionEnum[cc.numberOfConditions];
                cc.variables = new float[cc.numberOfConditions * 3];
            }



            //reset count
            cc.numberOfVariables = 0;

            for (int i = 0; i < cc.conditionEnums.Length; i++)
            {
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Condition", GUILayout.Width(80));
                cc.conditionEnums[i] = (ConditionEnum)EditorGUILayout.EnumPopup(cc.conditionEnums[i]);
                EditorGUILayout.EndHorizontal();

                switch (cc.conditionEnums[i])
                {
                    case ConditionEnum.RangeIn:
                        cc.variables[cc.numberOfVariables] = EditorGUILayout.FloatField("RangeIn", cc.variables[cc.numberOfVariables]);
                        cc.numberOfVariables++;
                        break;
                    case ConditionEnum.RangeOut:
                        cc.variables[cc.numberOfVariables] = EditorGUILayout.FloatField("RangeOut", cc.variables[cc.numberOfVariables]);
                        cc.numberOfVariables++;
                        break;
                    case ConditionEnum.ViewIn:
                        cc.variables[cc.numberOfVariables] = EditorGUILayout.Slider("ViewIn", cc.variables[cc.numberOfVariables], -1, 1);
                        cc.numberOfVariables++;
                        break;
                    case ConditionEnum.ViewOut:
                        cc.variables[cc.numberOfVariables] = EditorGUILayout.Slider("ViewOut", cc.variables[cc.numberOfVariables], -1 , 1);
                        cc.numberOfVariables++;
                        break;
                    case ConditionEnum.TimeMin:
                        cc.variables[cc.numberOfVariables] = EditorGUILayout.FloatField("TimeMin", cc.variables[cc.numberOfVariables]);
                        cc.numberOfVariables++;
                        break;
                    case ConditionEnum.TimeMax:
                        cc.variables[cc.numberOfVariables] = EditorGUILayout.FloatField("TimeMax", cc.variables[cc.numberOfVariables]);
                        cc.numberOfVariables++;
                        break;
                }
            }

            //make it stay?
            EditorGUILayout.EndVertical();
            EditorUtility.SetDirty(cc);
        }
    }
}


