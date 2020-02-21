using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ROFO
{ 
public class MoveControlXZ : MonoBehaviour
{
    private DirectionCheck dC;

    public Transform parent;
    public Transform playerBody;
    public Transform cameraMovement;

    [Header("Speeds")]
    public float forwardSpeed = 1f;
    public float backwardSpeed = 1f;
    public float straffeSpeed = 1f;
    public float generalSpeed = 1f;

    [Header("Movement")]
    public Vector3 move = Vector3.zero;

    [Header("Input")]
    public Vector2 input;

    private void Awake()
    {
        dC = GetComponent<DirectionCheck>();
    }

    private void Update()
    {
        ////calc
        //move = CalculateMove();

        ////move
        //Vector3 f = move.z * cameraMovement.forward;
        //Vector3 s = move.x * cameraMovement.right;
        //parent.position += (f + s) * Time.deltaTime;

        ////rotate
        //CalculateRotation(move.z);

        if (dC.canMoveXZ)
        {
            //calc
            move = CalculateMove();

            //move
            Vector3 f = move.z * cameraMovement.forward;
            Vector3 s = move.x * cameraMovement.right;
            parent.position += (f + s) * Time.deltaTime;

            //rotate
            CalculateRotation(move.z);
        }
    }

    private Vector3 CalculateMove()
    {
        input = GetInput();

        input.x *= straffeSpeed;
        input.y *= forwardSpeed;

        input = NormalizedMovement(input);

        return new Vector3(input.x, 0f, input.y) * generalSpeed;
    }

    public Vector2 GetInput()
    {
        float v = Input.GetAxis("Vertical");
        float h = Input.GetAxis("Horizontal");

        return new Vector2(h, v);
    }

    public Vector2 NormalizedMovement(Vector2 v)
    {
        return v.normalized;
    }

    [Range(0.01f, 0.99f)] public float faceRotation = 0.2f;
        private void CalculateRotation(float zInput)
        {
            if (zInput > 0)
            {
                playerBody.rotation = Quaternion.Lerp(playerBody.rotation, Quaternion.LookRotation(cameraMovement.forward), faceRotation);
            }
        }
    }
}
