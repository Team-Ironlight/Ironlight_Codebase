using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ROFO
{
    public class CameraShake : MonoBehaviour
    {
        public Transform player;
        public Vector3 offset;
        [SerializeField] private float noiseMag = 0f;
        public float noiseSensitivity = 1f;
        public float noiseDecreaseSpeed = 1f;
        private Vector3 explosionNoise;


        private void Update()
        {
            //test
            if (Input.GetMouseButtonUp(1))
            {
                Explode(0.4f);
            }

            if (noiseMag > 0f)
            {
                transform.position = player.transform.position + offset + Calculate();
            }
            else
            {
                transform.position = player.transform.position + offset;
            }
        }

        //add amount direct input
        public void Explode(float amount)
        {
            noiseMag += amount;
        }

        //add amount by position
        public void Explode(Vector3 pos)
        {
            noiseMag += noiseSensitivity / (player.position - pos).magnitude;
        }

        //calculate noise, formula could be improved
        public Vector3 Calculate()
        {
            //add residual noise
            explosionNoise = Vector3.up * (Mathf.PerlinNoise(Mathf.Sin(Time.time * noiseMag), Mathf.Sin(Time.time * noiseMag)) - 0.5f) +
                             Vector3.right * (Mathf.PerlinNoise(Mathf.Cos(Time.time * noiseMag), Mathf.Cos(Time.time * noiseMag)) - 0.5f);

            //gradually reduce noise
            noiseMag -= Time.deltaTime * noiseDecreaseSpeed;

            if (noiseMag < 0f)
            {
                noiseMag = 0f;
            }

            return explosionNoise;
        }
    }
}
