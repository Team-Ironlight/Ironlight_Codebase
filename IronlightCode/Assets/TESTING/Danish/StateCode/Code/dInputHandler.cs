using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;
using Danish.Tools;

namespace Danish.StateCode
{
    public class dInputHandler : MonoBehaviour
    {
        TestDanish_Controller_Input controls;
        public dStateManager _stateManager = null;


        // Temporary references for testing
        //public Rigidbody _rigidbody;
        //public dObjectPooler _pooler;
        //public Animator _animator;



        [Header("Values to Update")]
        public Vector2 _MoveVector = Vector2.zero;
        //public bool _isMoving = false;
        public Vector2 _DashVector = Vector2.zero;
        public bool _isDashing = false;
        public bool _isJumping = false;
        public bool _isAttacking = false;



        private void Update()
        {
            //_stateManager.Tick();
        }

        private void FixedUpdate()
        {
            UpdateStateValues();
        }


        public dStateManager Init()
        {
            controls = new TestDanish_Controller_Input();

            if (_stateManager == null)
            {
                _stateManager = new dStateManager();
                //Debug.Log("INIT THE MANAGER");
            }
            return _stateManager;
        }


        void UpdateStateValues()
        {
            //Debug.Log("Updating Values");

            _stateManager.moveVector = _MoveVector;
            _stateManager.dashVector = _DashVector;



            _stateManager.isAttacking = _isAttacking;

            if (_isJumping)
            {
                _stateManager.jump = _isJumping;
                _isJumping = false;

            }

            if (_isDashing)
            {
                _stateManager.isDashing = _isDashing;
                _isDashing = false;
            }
        }




        #region Input Initialization








        private void OnEnable()
        {
            controls.Enable();

            controls.Traversal.Movement.performed += Movement_performed;
            controls.Traversal.Movement.canceled += Movement_canceled;

            controls.Traversal.Jump.started += Jump_started;
            controls.Traversal.Jump.performed += Jump_performed;
            controls.Traversal.Jump.canceled += Jump_canceled;

            controls.Traversal.Dash.started += Dash_started;
            controls.Traversal.Dash.performed += Dash_performed;


            controls.Combat.Attack.started += Attack_started;
        }



        private void OnDisable()
        {
            controls.Traversal.Movement.performed -= Movement_performed;
            controls.Traversal.Movement.canceled -= Movement_canceled;

            controls.Traversal.Jump.started -= Jump_started;
            controls.Traversal.Jump.performed -= Jump_performed;
            controls.Traversal.Jump.canceled -= Jump_canceled;

            controls.Traversal.Dash.started -= Dash_started;
            controls.Traversal.Dash.performed -= Dash_performed;

            controls.Combat.Attack.started -= Attack_started;

            controls.Disable();
        }







        #endregion









        #region Input Functions


        private void Attack_started(InputAction.CallbackContext obj)
        {
        }




        private void Dash_started(InputAction.CallbackContext obj)
        {
            //Debug.Log("Dash Started");

            _DashVector = _MoveVector;
        }

        private void Dash_performed(InputAction.CallbackContext obj)
        {
            //Debug.Log("Dash Performed");
            _isDashing = true;
            //isDashing = false;
        }

        private void Jump_canceled(InputAction.CallbackContext obj)
        {
            //Debug.Log("Jump Canceled Early");
            if (obj.interaction is HoldInteraction)
            {
            }
        }

        private void Jump_started(InputAction.CallbackContext obj)
        {
            //Debug.Log("Jump Started");
        }

        private void Jump_performed(InputAction.CallbackContext obj)
        {
            _isJumping = true;
            //Debug.Log("Jump Performed");
        }

        private void Movement_performed(InputAction.CallbackContext ctx)
        {
            // when WASD has been pressed for more than 0.01 seconds, 
            // player is moving and move vector is updated with a key pair representing 
            // vertical and horizontal input.
            if (ctx.interaction is HoldInteraction)
            {
                _MoveVector = ctx.ReadValue<Vector2>();
            }
            // when WASD is released, player is no longer giving movement input and 
            // move vector is reset to wait for the next update
            else if (ctx.interaction is PressInteraction)
            {
                _MoveVector = Vector2.zero;
            }

        }

        private void Movement_canceled(InputAction.CallbackContext obj)
        {

        }






        #endregion
    }
}
