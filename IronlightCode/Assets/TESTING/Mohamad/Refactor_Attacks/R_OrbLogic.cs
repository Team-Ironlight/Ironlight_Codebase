using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sharmout.SO;

namespace Sharmout.attacks
{
    public class R_OrbLogic : MonoBehaviour
    {
        public float _iSpeed = 5;
        public float _iDamageAmount = 0;
        public float _DisableTimer = 5;

        Coroutine disableCO = null;
        Coroutine moveCo = null;

        Vector3 moveDirection = Vector3.zero;

        public void Init(Vector3 startPos, Vector3 _direction, OrbSO stats)
        {
            _iSpeed = stats.TraveliSpeed;
            _iDamageAmount = stats.DamageAmount;
            _DisableTimer = stats.DisableTimer;

            transform.position = startPos;

            moveDirection = _direction;

            // Start a coroutine to disable the bullet after a set amount of time when the object is enabled
            disableCO = StartCoroutine(BuletDisble());


            moveCo = StartCoroutine(OrbMovement());
        }


        public void Tick()
        {
            
        }

        IEnumerator OrbMovement()
        {
            while (true)
            {
                // move gameobject forward by a set speed
                Vector3 moveVector = moveDirection * _iSpeed * Time.deltaTime;

                transform.position += moveVector;

                yield return null; 
            }
        }


        // wait for a set amount of time and then set the gameobject to inactive
        private IEnumerator BuletDisble()
        {
            yield return new WaitForSeconds(_DisableTimer);

            if (moveCo != null)
            {
                StopCoroutine(moveCo);
                moveCo = null;
            }

            gameObject.SetActive(false);
            StopCoroutine(disableCO);
            disableCO = null;
            Debug.Log(disableCO);
        }




        private void OnCollisionEnter(Collision collision)
        {
            if(collision.gameObject.tag == "Enemy")
            {

            }
        }
    }
    
}