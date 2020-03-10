using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace AITEST
{
    [CustomEditor(typeof(SO_Projectile))]
    public class SO_ProjectileEditor : Editor
    {
        SO_Projectile p;

        private void OnEnable()
        {
            p = (SO_Projectile)target;
        }

        public override void OnInspectorGUI()
        {
            //base.OnInspectorGUI();

            EditorGUILayout.BeginVertical();
            p.type = (ProjectileEnum)EditorGUILayout.EnumPopup("Type", p.type);

            //show certain variables given type
            switch (p.type)
            {
                case ProjectileEnum.Seeker:
                    p.velocity = EditorGUILayout.FloatField("Velocity", p.velocity);
                    p.lifeTime = EditorGUILayout.FloatField("lifeTime", p.lifeTime);
                    p.seekIntensity = EditorGUILayout.Slider("Seek Intensity", p.seekIntensity, 0f, 2f);
                    p.seekControl = EditorGUILayout.Slider("Seek Control", p.seekControl, 0f, 1f);
                    break;
                case ProjectileEnum.Physics:
                    break;
            }
       
            EditorGUILayout.EndVertical();
            EditorUtility.SetDirty(p);
        }
    }
}


