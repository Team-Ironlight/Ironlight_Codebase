using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ROFO
{
    //Rob
    //Allow a material to swap amoungst different textures at a uniform speed
    public class TextureSwap : MonoBehaviour
    {
        [SerializeField] Texture[] textures = new Texture[3];
        [Range(0.01f, 1f)] [SerializeField] private float textureSpeed = 1f;
        [SerializeField] private float minTimeRange = 0f;
        [SerializeField] private float maxTimeRange = 1f;

        private Material mat;
        private Renderer rend;

        private Coroutine currentCoroutine;


        // Start is called before the first frame update
        void Start()
        {
            rend = GetComponent<Renderer>();

            //create new array to house textures??
            //textures = new Texture[arraySize];

            //start timer coroutine
            StartCoroutines();
        }



        IEnumerator Timer(float time)
        {
            Debug.Log("Start Timer");
            Debug.Log(time);
            //start timer
            float x = 0f;
            while (x < time)
            {
                yield return null;

                x += Time.deltaTime;
            }

            //call enumerator to cycle through textures
            currentCoroutine = StartCoroutine(SwapTextures());
        }

        IEnumerator SwapTextures()
        {
            Debug.Log("Start SwapTextures");
            int i = 0;
            while (i < textures.Length)
            {
                //Debug.Log(i);

                rend.material.mainTexture = textures[i];

                yield return new WaitForSecondsRealtime(textureSpeed);
                i++;
            }

            //reset to first texture
            rend.material.mainTexture = textures[0];

            StartCoroutines();
        }

        private void StartCoroutines()
        {
            //get next random time
            float r = Random.Range(minTimeRange, maxTimeRange);

            //start timer coroutine with value passed in
            currentCoroutine = StartCoroutine(Timer(r));
        }
    }
}
