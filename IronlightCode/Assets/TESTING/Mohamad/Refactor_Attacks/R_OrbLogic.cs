using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sharmout.attacks
{
    public class R_OrbLogic : MonoBehaviour
    {
        public float _iSpeed = 5;
        public float _iDamageAmount = 0;
        public float _DisableTimer = 3;
        //public Transform transform;
        
        // Start Function
        void Init(GameObject Bullet, Vector3 pDir)
        {
            //give direction
            //transform = Bullet.transform;

            //transform.forward = pDir;
        }

        private void OnEnable()
        {
            Debug.Log("Bitch is enabled");
            StartCoroutine("BuletDisble");
        }

        private void OnDisable()
        {
            Debug.Log("Bitch is disabled and fucked");
        }

        // Update Function
        void Update()
        {
            
            transform.position += transform.forward * _iSpeed * Time.deltaTime;
        }

        private IEnumerator BuletDisble()
        {
            yield return new WaitForSeconds(_DisableTimer);
            gameObject.SetActive(false);
        }
    }
    
}