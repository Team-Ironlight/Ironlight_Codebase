using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public CharacterController PlayerControl;
    public float moveSpeed = 10.0f;
    public float playerGravity = 20.0f;
    public float vGravity;
    public bool checkActions;

    public Vector3 moveDirections;

    private void Start()
    {
        checkActions = false;
        PlayerControl = GetComponent<CharacterController>();
    }

    public void Update()
    {
        PlayerMoves ();
    }

    public void PlayerMoves()
    {
        if (Input.anyKey.Equals(false))
        {
            checkActions = false;
        }
        else
        {
            checkActions = true;
        }

        //checking for the rotation of the camera with player and rotate around the player in idle state

        moveDirections = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        moveDirections = transform.TransformDirection(moveDirections);
        vGravity -= playerGravity * Time.deltaTime;
        moveDirections.y = vGravity;
        moveDirections *= moveSpeed;
        PlayerControl.Move(moveDirections * Time.deltaTime);
    }





}
