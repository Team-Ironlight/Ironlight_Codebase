using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace ROFO
{
    [CustomEditor(typeof(CustomCollider))]
    public class CustomColliderEditor : Editor
    {
        CustomCollider cc;
        //float sizeMod = 5f;

        Vector3 directionH;
        Vector3 directionW;

        //set target of the this editor script to script
        private void OnEnable()
        {
            cc = (CustomCollider)target;
        }

        //runs base inspector GUI 
        public override void OnInspectorGUI()
        {
            //base.OnInspectorGUI();

            //TODO
            //create just a layout where only certain things are shown based on handle type

            EditorGUILayout.BeginVertical();
            cc.handleOn = EditorGUILayout.Toggle("Handle On", cc.handleOn);
            cc.isTrigger = EditorGUILayout.Toggle("Is Trigger", cc.isTrigger);
            cc.currentCollider = (ColliderType)EditorGUILayout.EnumPopup("Collider Type", cc.currentCollider);
            cc.offset = EditorGUILayout.Vector3Field("Offset", cc.offset);
            EditorGUILayout.EndVertical();
            EditorGUILayout.Space();

            if (cc.handleOn)
            {
                //sphere is 0, box is 1, capsule is 2
                if ((int)cc.currentCollider == 0)
                {
                    SphereInspector();
                }
                else if ((int)cc.currentCollider == 1)
                {
                    BoxInspector();
                }
                else if ((int)cc.currentCollider == 2)
                {
                    CapsuleInspector();
                }
            }
        }

        private void SphereInspector()
        {
            EditorGUILayout.BeginVertical();

            cc.radiusSize = EditorGUILayout.FloatField("Radius Size", cc.radiusSize);

            EditorGUILayout.EndVertical();
        }

        private void BoxInspector()
        {
            EditorGUILayout.BeginVertical();

            cc.length = EditorGUILayout.FloatField("Length", cc.length);
            cc.width = EditorGUILayout.FloatField("Width", cc.width);
            cc.depth = EditorGUILayout.FloatField("Depth", cc.depth);

            EditorGUILayout.EndVertical();
        }

        private void CapsuleInspector()
        {
            EditorGUILayout.BeginVertical();

            cc.radius = EditorGUILayout.FloatField("Radius", cc.radius);
            cc.height = EditorGUILayout.FloatField("Height", cc.height);
            cc.direction = (CapsuleDirection)EditorGUILayout.EnumPopup("Direction", cc.direction);

            EditorGUILayout.EndVertical();
        }


        private void OnSceneGUI()
        {
            if (cc.handleOn)
            {
                //sphere is 0, box is 1, capsule is 2
                if ((int)cc.currentCollider == 0)
                {
                    DrawSphere();
                }
                else if ((int)cc.currentCollider == 1)
                {
                    DrawRectangle();
                }
                else if ((int)cc.currentCollider == 2)
                {
                    DrawCapsule();
                }
            }
        }

        //alt for box
        private void DrawWireCube()
        {
            Vector3 rectangle = new Vector3(cc.width, cc.depth, cc.length);
            Handles.color = Color.red;
            //Handles.matrix = LB.transform.localToWorldMatrix;
            Handles.DrawWireCube(cc.transform.position + cc.offset, rectangle);
        }

        //draws sphere
        private void DrawSphere()
        {
            // help us visualize exactly where the location is
            cc.radiusSize = Handles.RadiusHandle(cc.transform.rotation, cc.transform.position + cc.offset, cc.radiusSize);
        }

        //draws rectangle that can resize and rotate
        private void DrawRectangle()
        {
            //get forward and right vectors
            Vector3 forward = cc.transform.forward;
            Vector3 right = cc.transform.right;

            //make size handles for length and width
            cc.length = Handles.ScaleValueHandle(cc.length, cc.transform.position + forward * cc.length / 2,
                                                 Quaternion.LookRotation(cc.transform.forward), cc.handleSize,
                                                 Handles.ArrowHandleCap, 1f);
            cc.length = Handles.ScaleValueHandle(cc.length, cc.transform.position + -forward * cc.length / 2,
                                                 Quaternion.LookRotation(-cc.transform.forward), cc.handleSize,
                                                 Handles.ArrowHandleCap, 1f);

            cc.width = Handles.ScaleValueHandle(cc.width, cc.transform.position + right * cc.width / 2,
                                                 Quaternion.LookRotation(cc.transform.right), cc.handleSize,
                                                 Handles.ArrowHandleCap, 1f);
            cc.width = Handles.ScaleValueHandle(cc.width, cc.transform.position + -right * cc.width / 2,
                                                 Quaternion.LookRotation(-cc.transform.right), cc.handleSize,
                                                 Handles.ArrowHandleCap, 1f);

            //calculate vector points to make rectangle
            Vector3 topRight = cc.transform.position + (forward * cc.length / 2) + (right * cc.width / 2);
            Vector3 bottomRight = cc.transform.position - (forward * cc.length / 2) + (right * cc.width / 2);
            Vector3 bottomLeft = cc.transform.position - (forward * cc.length / 2) - (right * cc.width / 2);
            Vector3 topLeft = cc.transform.position + (forward * cc.length / 2) - (right * cc.width / 2);

            //put them into vector array
            Vector3[] verts = new Vector3[]
            {
                topRight, bottomRight, bottomLeft, topLeft
            };

            //draw rectangle
            Handles.DrawSolidRectangleWithOutline(verts, Color.clear, Color.red);
        }

        //draw capsule
        private void DrawCapsule()
        {
            //draw according to axis
            //Xaxis
            if (cc.direction == 0)
            {
                directionH = cc.transform.right;
                directionW = cc.transform.forward;
            }
            else if ((int)cc.direction == 1)
            {
                directionH = cc.transform.up;
                directionW = cc.transform.forward;
            }
            else if ((int)cc.direction == 2)
            {
                directionH = cc.transform.forward;
                directionW = cc.transform.up;
            }

            //center
            


            //using arrow controls
            cc.height = Handles.ScaleValueHandle(cc.height, cc.transform.position + cc.height / 2 * directionH,
                                                 Quaternion.LookRotation(directionH),
                                                 cc.handleSize, Handles.ArrowHandleCap, 0.1f);

            cc.height = Handles.ScaleValueHandle(cc.height, cc.transform.position + cc.height / 2 * -directionH,
                                                 Quaternion.LookRotation(-directionH),
                                                 cc.handleSize, Handles.ArrowHandleCap, 0.1f);

            cc.radius = Handles.ScaleValueHandle(cc.radius, cc.transform.position + cc.radius * directionW,
                                                 Quaternion.LookRotation(directionW),
                                                 cc.handleSize, Handles.ArrowHandleCap, 0.1f);

            cc.radius = Handles.ScaleValueHandle(cc.radius, cc.transform.position + cc.radius * -directionW,
                                                 Quaternion.LookRotation(-directionW),
                                                 cc.handleSize, Handles.ArrowHandleCap, 0.1f);

            //using sphere controls
            Handles.RadiusHandle(cc.transform.rotation, cc.transform.position +
                                            (directionH * (cc.height / 2 - cc.radius)), cc.radius);
            Handles.RadiusHandle(cc.transform.rotation, cc.transform.position +
                                            (-directionH * (cc.height / 2 - cc.radius)), cc.radius);

            //Get remaining Height, if greater than 0 need more spheres
            float remainingHeight = cc.height - (cc.radius * 2);

            if (remainingHeight > 0)
            {
                //get number of extra spheres
                int extraSpheres = (int)Mathf.Ceil((cc.height - cc.radius * 2) / (cc.radius * 2));

                //determine spacing
                float offset = remainingHeight / extraSpheres;

                //get starting point
                Vector3 startingPoint = cc.transform.position - ((cc.height / 2) * (directionH)) + (cc.radius * directionH);

                //set spheres by spacing
                for (int i = 1; i <= extraSpheres; i++)
                {
                    Handles.RadiusHandle(cc.transform.rotation, startingPoint + (i * offset * directionH), cc.radius);
                }
            }
        }
    }
}
