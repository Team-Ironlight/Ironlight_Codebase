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
        public Transform parentObj;

        Coroutine currentCo = null;
        
        // Start Function, not used right now
        void Init()
        {
            
        }

        // Start a coroutine to disable the bullet after a set amount of time when the object is enabled
        private void OnEnable()
        {             
            currentCo = StartCoroutine("BuletDisble");
        }

        // Stop the coroutine when object is disabled
        private void OnDisable()
        {
            StopCoroutine(currentCo);
            currentCo = null;
        }

        void Update()
        {
            // move gameobject forward by a set speed
            transform.position += transform.forward * _iSpeed * Time.deltaTime;
        }

        // wait for a set amount of time and then set the gameobject to inactive
        private IEnumerator BuletDisble()
        {
            yield return new WaitForSeconds(_DisableTimer);
            gameObject.SetActive(false);
        }
    }
    
}