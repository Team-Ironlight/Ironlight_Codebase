using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

public class TestDanish_Controller_StateManager_v1 : MonoBehaviour
{
    [Header("Movement Variables")]
    public float moveSpeed = 0.005f;
    public Vector2 moveVector;
    public bool isMoving = false;

    [Header("Dash Variables")]
    public Vector2 dashVector;
    public bool isDashing = false;
    public float dashDistance = 0.1f;

    [Header("Jump Variables")]
    public bool jump = false;

    [Header("Combat Variables")]
    public bool isAttacking = false;

    public ChosenAttack currentAttack = ChosenAttack.ORB;

    public string currentTraversalState = " ";
    public string currentCombatState = " ";

    public GameObject playerObject;
    public GameObject modelHolder;
    public Rigidbody rigid;
    public GameObject playerCamera;
    public TestDanish_Controller_TraversalStateMachine_v1 traversalMachine;
    public TestDanish_Controller_CombatlStateMachine_v1 combatMachine;
    TestDanish_Controller_Input controls;


    public TestDanish_Components_Movement_v1 movement_V1;
    public TestDanish_Components_Dash dash;


    public enum ChosenAttack
    {
        ORB,
        BEAM,
        BLAST
    }


    public void Init()
    {
        traversalMachine = gameObject.AddComponent<TestDanish_Controller_TraversalStateMachine_v1>();
        combatMachine = gameObject.AddComponent<TestDanish_Controller_CombatlStateMachine_v1>();

        movement_V1 = new TestDanish_Components_Movement_v1();
        movement_V1.Init(this);

        dash = new TestDanish_Components_Dash();
        dash.Init(this);

        controls = new TestDanish_Controller_Input();

        InitializeCombatMachine();
        InitializeTraversalMachine();
    }

    public void Tick()
    {
        currentCombatState = combatMachine.currentState.ToString();
        currentTraversalState = traversalMachine.currentState.ToString();
    }














    #region Setup Functions

    void InitializeTraversalMachine()
    {
        var states = new Dictionary<Type, TestDanish_TraversalBaseState>()
        {
            {typeof(TestDanish_TIdleState), new TestDanish_TIdleState(_Manager:this) },
            {typeof(TestDanish_TMoveState), new TestDanish_TMoveState(_Manager:this) },
            {typeof(TestDanish_TDashState), new TestDanish_TDashState(_Manager:this) }
        };

        traversalMachine.SetStates(states);
    
    }
    
    void InitializeCombatMachine()
    {
        var states = new Dictionary<Type, TestDanish_CombatBaseState>()
        {
            {typeof(TestDanish_CReadyState), new TestDanish_CReadyState(_Manager:this) },
            {typeof(TestDanish_CAttackState), new TestDanish_CAttackState(_Manager:this) }
        };

        combatMachine.SetStates(states);
    
    }



    #endregion
}
