using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Danish
{
    [RequireComponent
        (typeof(Rigidbody), 
        typeof(Tools.dObjectPooler), 
        typeof(StateCode.dInputHandler)
        
    )]
    public class StateController : MonoBehaviour
    {
        public Vector3 playerVelocity = Vector3.zero;

        [Header("Movement Speeds")]
        public float forwardSpeed = 1f;
        public float backwardSpeed = 1f;
        public float straffeSpeed = 1f;
        public float generalSpeed = 5f;

        public Rigidbody parentRigidbody = null;
        public Animator parentAnimator = null;
        public Tools.dObjectPooler parentPooler = null;
        public StateCode.dInputHandler parentInput = null;
        [SerializeField]
        public StateCode.dStateManager parentManager = null;
        public Transform parentCamera = null;

        private void Reset()
        {
            parentRigidbody = GetComponent<Rigidbody>();
            parentRigidbody.useGravity = false;
            parentRigidbody.constraints = RigidbodyConstraints.FreezeRotation;

            parentPooler = GetComponent<Tools.dObjectPooler>();

            parentInput = GetComponent<StateCode.dInputHandler>();
        }

        private void Awake()
        {
            parentManager = parentInput.Init();

            parentManager?.Init(gameObject, parentRigidbody, parentPooler, parentAnimator, parentCamera);
        }

        void Start()
        {

        }

        void Update()
        {
            parentManager.Tick();

            playerVelocity = parentRigidbody.velocity;
        }

        private void FixedUpdate()
        {
            parentManager.FixedTick();
        }


        void UpdateFixedValues()
        {

        }
    }
}