using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ROFO
{
    public class MaterialChange : AChange
    {
        private Renderer rend;
        public Material deactive;
        public Material active;
        private Material currentMat;

        private void Start()
        {
            rend = GetComponent<Renderer>();
            deactive = rend.material;
            currentMat = deactive;

            GetMeshRenderers(transform);
        }

        //recursive call
        private void GetMeshRenderers(Transform parent)
        {
            Debug.Log("<color=red>GETTING MESHES</color>");
            foreach(Transform child in parent)
            {
                if(child.childCount > 0)
                {
                    GetMeshRenderers(child);
                }
                else
                {
                    if(GetComponent<MeshRenderer>())
                    {
                        GetComponent<MeshRenderer>().material = currentMat;
                    }
                }
            }
        }

        public override void Change()
        {
            if(currentMat == active)
            {
                currentMat = deactive;
            }
            else
            {
                currentMat = active;
            }

            rend.material = currentMat;
            GetMeshRenderers(transform);
        }
    }
}
