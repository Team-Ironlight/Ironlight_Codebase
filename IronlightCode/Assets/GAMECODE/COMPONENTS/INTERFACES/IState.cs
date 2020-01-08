using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public interface IState
{
    void Setup(Transform pTarget, Animator pAnim, NavMeshAgent pAgent); // Replacement for Start
    void Enter(); // When entering state
    void Exit(); // When Exiting state
    bool CanEnter(float pDistance); // Check if entering is possible
    bool CanExit(float pDistance); // Check if exiting is possible
    void Tick(); // Replacement for Update (Called every frame when it is the current state)
}
