using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IMovement
{
    float moveAmount { get; set; }
    float moveDir { get; set; }
    float moveSpeed { get; set; }


    void CalculateMoveAmount();
    void CalculateMoveDirection();
    void CaluculateMoveSpeed();

    //moveDir * (targetSpeed * moveAmount);
}
