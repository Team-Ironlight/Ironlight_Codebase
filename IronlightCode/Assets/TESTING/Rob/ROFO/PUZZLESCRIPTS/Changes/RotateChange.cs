using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ROFO
{
    public class RotateChange : AChangeTransform
    {
        public Vector3[] rotations = { Vector3.zero };
        public float rotTime = 1f;
        public int letterSize = 3;
        private Quaternion targetRot;
        public float smoothRot;
        //interface method, calls move
        public override void Change()
        {
            Debug.Log("Change");
            bool check = ChangeLogic();

            if (check)
            {
                Debug.Log("Not moving, start move");
                c = null;
                c = StartCoroutine(Work());
            }
        }

        //move gameobject to position stored in next array position
        protected override IEnumerator Work()
        {
            SetIsMoving(true);
            //keeps index in array bounds
            int indexStart = indexPos;
            int indexNext = (indexPos + 1 + rotations.Length) % rotations.Length;
            indexPos = indexNext;

            //targetRot *= Quaternion.AngleAxis(45, Vector3.up);

            //bool checkRot = false;
            //if (Mathf.Abs(rotations[indexStart].y - rotations[indexNext].y) > 180)
            //{
            //    print("Recognize this");
            //    checkRot = true;
            //    if (rotations[indexStart].y > rotations[indexNext].y)
            //    {
            //        rotations[indexStart].y -= 360;
            //        print("RecognizeMe Biatch");

            //    }
            //    else
            //    {
            //        rotations[indexNext].y -= 360;
            //        print("hogwash");
            //    }
            //}
            //else
            //{
            //    checkRot = false;
            //}

            //get the desired rotation
            Quaternion startRot = transform.rotation;
            Quaternion nextRot = Quaternion.Euler(rotations[indexNext]);

            float count = 0.0f;

            while (transform.rotation.eulerAngles != nextRot.eulerAngles)
            {
                count += Time.deltaTime;

                transform.rotation = Quaternion.Euler(Vector3.Slerp(startRot.eulerAngles,nextRot.eulerAngles,count / rotTime));
                //transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg);
                yield return null;
            }

            //ensure in correct position and nothing weird is up
            transform.rotation = Quaternion.Euler(rotations[indexNext]);
            //transform.parent.rotation = Quaternion.Lerp(transform.parent.rotation, targetRot, 10 * smoothRot * Time.deltaTime);
            SetIsMoving(false);
        }

        protected override void Setup()
        {
            transform.rotation = Quaternion.Euler(rotations[startPos]);
            //rotations[0] = new Vector3(transform.rotation.eulerAngles.x,
            //                           transform.rotation.eulerAngles.y,
            //                           transform.rotation.eulerAngles.z);

            //set index pos for iterating through array to current position index
            indexPos = startPos;
        }

        private void Start()
        {
            Setup();
        }

        //test
        private void Update()
        {
            if (Input.GetKeyUp(KeyCode.P))
            {
                Change();
            }
        }
    }
}
