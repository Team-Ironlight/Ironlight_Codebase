using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ROFO
{
    public class MoveChange : AChangeTransform
    {
        public Vector3[] positions = { Vector3.zero };

        public AudioSource sound;
        public AudioClip soundToPlay;
        public float volume;
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
            sound.PlayOneShot(soundToPlay, volume);
            int indexStart = indexPos;
            int indexNext = (indexPos + 1 + positions.Length) % positions.Length;
            indexPos = indexNext;

            //get distance from start to finish to get accurate movement speed
            float distance = (positions[indexNext] - positions[indexStart]).magnitude;
            float count = 0.0f;

            while (transform.position != positions[indexNext])
            {
                count += Time.deltaTime;
                transform.position = Vector3.Lerp(positions[indexStart], positions[indexNext], count * speed / distance);
                yield return null;
            }

            //ensure in correct position and nothing weird is up
            transform.position = positions[indexNext];
            //indexPos = indexNext;
            SetIsMoving(false);
        }

        protected override void Setup()
        {
            //set gameobjects position to position in array indicated
            transform.position = positions[startPos];

            //set index pos for iterating through array to current position index
            indexPos = startPos;
        }

        private void Start()
        {
            Setup();
        }


        //test
        //private void Update()
        //{
        //    if(Input.GetKeyUp(KeyCode.P))
        //    {
        //        Change();
        //    }
        //}
    }
}
