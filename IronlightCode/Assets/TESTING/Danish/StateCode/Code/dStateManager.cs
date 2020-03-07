using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Danish.Tools;
using Danish.Components;
using Danish.Components.SO;
using Sharmout.attacks;
using Sharmout.SO;


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


        [Header("Aim Down Sights Variables")]
        public bool ADS = false;

		[Header("Power Scroll Variables")]
		public float scrollValue;
        public bool scrollUp = false;
        public bool scrollDown = false;

        public GameObject obj;
        public Transform objTransform;

        public Animator animator = null;
        public Rigidbody rigidbody = null;
        public dObjectPooler pooler = null;
        public LineRenderer Line = null;
        public Transform Muzzle = null;
        public Transform CameraHolder = null;

        public GameObject CanvasObj = null;

        dTraversalMachine TraversalMachine = null;
        dCombatMachine CombatMachine = null;

        dComponentHolder dComponent = null;

        // Temporary Combat Component Reference
        public R_OrbAttack rOrb = null;
        public R_BeamAttack rBeam = null;
        public R_BlastAttack rBlast = null;


        // Temporary Traversal Component Reference
        public dJumpComponent dJump = null;

        public dJump_SO dJumpSO = null;

        public dDashComponent dDash = null;
        public dMoveComponent dMove = null;
        public dMoveComponent dFloat = null;
        public dMoveComponent dAimMove = null;

        public dPhysicsComponent dPhysics = null;
		public dPowerWheel dPower = null;
		public dUIUpdater d_UIUpdater = null;

        // Temporary Stat SO references for Attacks
        public BeamSO beamStats = null;
        public BlastSO blastStats = null;
        public OrbSO orbStats = null;

        // Temporary Reference for a crosshair component
        public dCrosshairComponent dCrosshair = null;

        public void Init(GameObject parentObj, Rigidbody parentRigid, dObjectPooler parentPooler, Animator parentAnimator, Transform parentCamera, Transform parentMuzzle, dComponentHolder parentComponentHolder)
        {
            //Debug.Log("Initialize State Manager");


            obj = parentObj;
            objTransform = parentObj.transform;

            rigidbody = parentRigid;

            pooler = parentPooler;

            animator = parentAnimator;

            CameraHolder = parentCamera;

            Muzzle = parentMuzzle;

            dComponent = parentComponentHolder;

            dJump = new dJumpComponent();
            dDash = new dDashComponent();
            dMove = new dMoveComponent();
            dFloat = new dMoveComponent();
            dAimMove = new dMoveComponent();

            rOrb = new R_OrbAttack();
            rBeam = new R_BeamAttack();
            rBlast = new R_BlastAttack();

            dPhysics = new dPhysicsComponent();
			dPower = new dPowerWheel();
			d_UIUpdater = new dUIUpdater();

            dCrosshair = new dCrosshairComponent();

            InitializeTraversalMachine();
            InitializeCombatMachine();
        }

        public void AttackStatInit(OrbSO _orbS, BeamSO _beamS, BlastSO _blastS, GameObject _canvas)
        {
            orbStats = _orbS;
            beamStats = _beamS;
            blastStats = _blastS;

            CanvasObj = _canvas;
        }

        public void InitializeComponents(Danish.Components.dComponentHolder _componentHolder)
        {
            //dJumpSO = _componentHolder.FindInDictionary("TinyJump");
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
                {typeof(dDashState), new dDashState(_stateManager:this) },
                {typeof(dAimState), new dAimState(_stateManager:this) }
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