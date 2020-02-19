using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace ROFO
{ 
[CustomEditor(typeof(RotateChange))]
public class RotateChangeEditor : Editor
{
    RotateChange rc;

    private void OnEnable()
    {
        rc = (RotateChange)target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
    }

        private void OnSceneGUI()
        {
            GUIStyle style = new GUIStyle();

            //fontsize relative to camera distance
            float cameraDistance = (rc.transform.position - Camera.main.transform.position).magnitude;
            style.fontSize = (int)cameraDistance * 5;

            if (rc.rotations.Length >= 1)
            {
                for (int i = 0; i < rc.rotations.Length; i++)
                {
                    if (i == 0)
                    {
                        Handles.color = Color.black;
                        style.normal.textColor = Color.black;
                    }
                    else
                    {
                        Handles.color = Color.red;
                        style.normal.textColor = Color.red;
                    }

                    Quaternion original = rc.transform.rotation;
                    //Quaternion temp = rc.transform.rotation * Quaternion.Euler(rc.rotations[i]);
                    Quaternion temp = Quaternion.Euler(rc.rotations[i]);

                    rc.transform.rotation = temp;
                    Vector3 tempForward = rc.transform.forward;
                    rc.transform.rotation = original;

                    Handles.DrawLine(rc.transform.position,
                                     rc.transform.position + tempForward * rc.gizmosSize);

                    Handles.Label(rc.transform.position + tempForward, "R" + i, style);



                    //if (i == 0)
                    //{
                    //    Handles.color = Color.black;
                    //    Handles.DrawLine(rc.transform.position,
                    //                     rc.transform.position + rc.transform.forward * rc.gizmosSize);
                    //}
                    //else
                    //{
                    //    Handles.color = Color.red;
                    //    Quaternion original = rc.transform.rotation;
                    //    //Quaternion temp = rc.transform.rotation * Quaternion.Euler(rc.rotations[i]);
                    //    Quaternion temp = Quaternion.Euler(rc.rotations[i]);

                    //    rc.transform.rotation = temp;
                    //    Vector3 tempForward = rc.transform.forward;
                    //    rc.transform.rotation = original;

                    //    Handles.DrawLine(rc.transform.position,
                    //                     rc.transform.position + tempForward * rc.gizmosSize);

                    //    Handles.Label(rc.transform.position + tempForward * rc.gizmosSize, "R" + i, style);
                    //}                
                }
            }
        }
    }
}
