using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ROFO
{
    //scale movement
    public class ScaleChange : AChangeTransform
    {
        public Vector3[] scales = { Vector3.one };

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

        protected override IEnumerator Work()
        {
            SetIsMoving(true);
            //keeps index in array bounds
            int startIndex = indexPos;
            int indexNext = (indexPos + 1 + scales.Length) % scales.Length;
            Debug.Log("Index Next: " + indexNext);

            indexPos = indexNext;

            float count = 0.0f;

            while (transform.localScale != scales[indexNext])
            {
                count += Time.deltaTime;
                transform.localScale = Vector3.Lerp(scales[startIndex], scales[indexNext], count);
                yield return null;
            }

            //ensure in correct position and nothing weird is up
            transform.localScale = scales[indexNext];
            SetIsMoving(false);
        }

        protected override void Setup()
        {
            //set initial scale to first scale size
            scales[0] = transform.localScale;
            Debug.Log("Transform LocalScale: " + transform.localScale);

            //set position
            transform.localScale = scales[startPos];
            indexPos = startPos;
        }

        private void Start()
        {
            Setup();
        }
    }
}
