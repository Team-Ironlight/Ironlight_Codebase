using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ScaleChange))]
public class ScaleChangeEditor : Editor
{
    ScaleChange sc;

    private void OnEnable()
    {
        sc = (ScaleChange)target;
    }

    private void OnSceneGUI()
    {
        if(sc.scales.Length >= 2)
        {
            GUIStyle style = new GUIStyle();
            style.normal.textColor = Color.cyan;

            //fontsize relative to camera distance
            float cameraDistance = (Camera.main.transform.position - sc.scales[0]).magnitude;
            style.fontSize = (int)cameraDistance;
            

            for (int i = 0; i < sc.scales.Length; i++)
            {
                Vector3 spot = new Vector3(0, sc.scales[i].y / 2f, 0f);
                Handles.Label(sc.transform.position + spot, "S" + i, style);

                //prevents showing a P0 since that will be gameobject base position
                //still needs to set the position inspector
                if (i == 0)
                {
                    sc.scales[i] = sc.transform.localScale;
                }
                else
                {
                    sc.scales[i] = Handles.ScaleHandle(sc.scales[i], sc.transform.position, sc.transform.rotation, sc.gizmosSize);
                    Handles.DrawWireCube(sc.transform.position, sc.scales[i]);
                }
            }
        }
    }

}
