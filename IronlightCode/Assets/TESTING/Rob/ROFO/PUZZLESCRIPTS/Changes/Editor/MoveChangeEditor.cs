using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace ROFO
{ 
[CustomEditor(typeof(MoveChange))]
public class MoveChangeEditor : Editor
{
    MoveChange m;

    private void OnEnable()
    {
        m = (MoveChange)target;
    }

        private void OnSceneGUI()
        {
            if (m.positions.Length >= 2)
            {
                GUIStyle style = new GUIStyle();
                style.normal.textColor = Color.green;

                //fontsize relative to camera distance
                float cameraDistance = (Camera.main.transform.position - m.positions[0]).magnitude;
                style.fontSize = (int)cameraDistance * 5;

                for (int i = 0; i < m.positions.Length; i++)
                {
                    Handles.Label(m.positions[i], "P" + i, style);

                    //prevents showing a P0 since that will be gameobject base position
                    //still needs to set the position inspector
                    if (i == 0)
                    {
                        m.positions[i] = m.transform.position;
                    }
                    else
                    {
                        m.positions[i] = Handles.PositionHandle(m.positions[i], m.transform.rotation);
                        Handles.DrawWireCube(m.positions[i], m.transform.localScale);
                    }
                }
            }
        }          
    }
}
