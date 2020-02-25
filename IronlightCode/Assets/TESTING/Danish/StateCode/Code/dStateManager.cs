using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Danish.Tools;
using Danish.Components;
using Sharmout.attacks;


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
        public bool currentlyJumping = false;

        [Header("Combat Variables")]
        public bool isAttacking = false;
        public bool launchOrb = false;
        public bool launchBeam = false;
        public bool launchBlast = false;


        public GameObject obj;
        public Transform objTransform;

        public Animator animator = null;
        public Rigidbody rigidbody = null;
        public dObjectPooler pooler = null;
        public Transform Muzzle = null;
        public Transform CameraHolder = null;

        dTraversalMachine TraversalMachine = null;
        dCombatMachine CombatMachine = null;

        // Temporary Combat Component Reference
        public R_OrbAttack rOrb = null;
        public R_BeamAttack rBeam = null;
        public R_BlastAttack rBlast = null;


        // Temporary Traversal Component Reference
        public dJumpComponent dJump = null;
        public dDashComponent dDash = null;
        public dMoveComponent dMove = null;
        public dMoveComponent dFloat = null;

        public dPhysicsComponent dPhysics = null;

        public void Init(GameObject parentObj, Rigidbody parentRigid, dObjectPooler parentPooler, Animator parentAnimator, Transform parentCamera, Transform parentMuzzle)
        {
            //Debug.Log("Initialize State Manager");


            obj = parentObj;
            objTransform = parentObj.transform;

            rigidbody = parentRigid;

            pooler = parentPooler;

            animator = parentAnimator;

            CameraHolder = parentCamera;

            Muzzle = parentMuzzle;

            dJump = new dJumpComponent();
            dDash = new dDashComponent();
            dMove = new dMoveComponent();
            dFloat = new dMoveComponent();

            rOrb = new R_OrbAttack();
            rBeam = new R_BeamAttack();
            rBlast = new R_BlastAttack();

            dPhysics = new dPhysicsComponent();

            InitializeTraversalMachine();
            InitializeCombatMachine();
            
        }

        public void Tick()
        {
            TraversalMachine.Tick();
            CombatMachine.Tick();

            //Debug.Log("ticking State Manager");
        }

        public void FixedTick()
        {
            TraversalMachine.FixedTick();
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
                {typeof(dDashState), new dDashState(_stateManager:this) }
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
                {typeof(dOrbState), new dOrbState(_stateManager:this) },
                {typeof(dBeamState), new dBeamState(_stateManager:this) },
                {typeof(dBlastState), new dBlastState(_stateManager:this) }
            };

            CombatMachine.SetStates(states);
        }
    }
}