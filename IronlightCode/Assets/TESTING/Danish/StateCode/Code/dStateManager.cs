using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Danish.Tools;


namespace Danish.StateCode
{
    //[System.Serializable]
    public class dStateManager
    {
        [Header("Movement Variables")]
        public Vector2 moveVector;
        public bool isMoving = false;

        [Header("Dash Variables")]
        public Vector2 dashVector;
        public bool isDashing = false;

        [Header("Jump Variables")]
        public bool jump = false;

        [Header("Combat Variables")]
        public bool isAttacking = false;


        public GameObject obj;
        public Transform objTransform;

        public Animator animator = null;
        public Rigidbody rigidbody = null;
        public dObjectPooler pooler = null;
        public Transform CameraHolder = null;
        dTraversalMachine TraversalMachine = null;
        dCombatMachine CombatMachine = null;

        public void Init(GameObject parentObj, Rigidbody parentRigid, dObjectPooler parentPooler, Animator parentAnimator, Transform parentCamera)
        {
            Debug.Log("Initialize State Manager");


            obj = parentObj;
            objTransform = parentObj.transform;

            rigidbody = parentRigid;

            pooler = parentPooler;

            animator = parentAnimator;

            CameraHolder = parentCamera;

            InitializeTraversalMachine();
            InitializeCombatMachine();
            
        }

        public void Tick()
        {
            TraversalMachine.Tick();
            CombatMachine.Tick();

            Debug.Log("ticking State Manager");
        }



        void InitializeTraversalMachine()
        {
            if(TraversalMachine == null)
            {
                TraversalMachine = new dTraversalMachine();
            }

            var states = new Dictionary<Type, dTraversalBaseState>()
            {
                {typeof(dMoveState), new dMoveState(_stateManager:this) },
                {typeof(dIdleState), new dIdleState(_stateManager:this) },
                {typeof(dJumpState), new dJumpState(_stateManager:this) },
                {typeof(dRising), new dRising(_stateManager:this) },
                {typeof(dFalling), new dFalling(_stateManager:this) }
            };

            TraversalMachine.SetStates(states);
        }

        void InitializeCombatMachine()
        {
            if (CombatMachine == null)
            {
                CombatMachine = new dCombatMachine();
            }

            var states = new Dictionary<Type, dCombatBaseState>()
            {
                {typeof(dReadyState), new dReadyState(_stateManager:this) },
                {typeof(dLaunchState), new dLaunchState(_stateManager:this) }
            };

            CombatMachine.SetStates(states);
        }
    }
}