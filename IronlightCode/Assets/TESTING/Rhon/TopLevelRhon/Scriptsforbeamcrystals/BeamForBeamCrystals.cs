using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ROFO
{
    public class BeamForBeamCrystals : MonoBehaviour
    {
        public bool BeamNeeded = false;
        public RotateChange RC;
        public GameObject line1;
        public GameObject line2;
        public GameObject line3;
        public GameObject line4;
     
        // Start is called before the first frame update
        void Start()
        {
            line1.gameObject.SetActive(false);
            line2.gameObject.SetActive(false);
            line3.gameObject.SetActive(false);
            line4.gameObject.SetActive(false);
        }

        // Update is called once per frame
        void Update()
        {
            if (!RC.hasCorrectPosition && BeamNeeded)
            {
                line1.gameObject.SetActive(true);
                line2.gameObject.SetActive(true);
                line3.gameObject.SetActive(true);
                line4.gameObject.SetActive(true);
            }
        }
    }

}
