using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DanishStateManagerControl : MonoBehaviour
{
    [SerializeField]
    float PlayerMoveSpeed = 0;
    [SerializeField]
    float DashDistance = 0;
    [SerializeField]
    float JumpHeight = 0;
    [SerializeField]
    string TraversalState = " ";
    [SerializeField]
    string CombatState = " ";


    private void Awake()
    {
        stateManager = GetComponentInChildren<TestDanish_Controller_StateManager_v1>();
        Init(stateManager);
    }

    private void Update()
    {
        Tick();
    }


    private TestDanish_Controller_StateManager_v1 stateManager;

    public void Init(TestDanish_Controller_StateManager_v1 pManager)
    {
        stateManager = pManager;

        PlayerMoveSpeed = pManager.moveSpeed;
        DashDistance = pManager.dashDistance;
        JumpHeight = pManager.jumpHeight;
        TraversalState = pManager.currentTraversalState;
        CombatState = pManager.currentCombatState;
    }

    public void Tick()
    {
        UpdateValues();
    }

    void UpdateValues()
    {
        stateManager.moveSpeed = PlayerMoveSpeed;
        stateManager.dashDistance = DashDistance;
        stateManager.jumpHeight = JumpHeight;

        TraversalState = stateManager.currentTraversalState;
        CombatState = stateManager.currentCombatState;
    }
}
