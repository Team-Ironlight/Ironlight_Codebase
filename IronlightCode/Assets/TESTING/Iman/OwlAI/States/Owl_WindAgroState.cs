using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using EZCameraShake;

public class Owl_WindAgroState : ImanBaseState
{
    Owl_StateManager stateManager;

    private Vector3 AgroPos;
    private Vector3 SavedPlayerPos;
    private float warningTimer;
    private bool CamShaked;
    private float DistAgroToOwl;
    //bankRotation
    private float Y1;
    private float Y2;

    public Owl_WindAgroState(Owl_StateManager _Manager) : base(_Manager.gameObject)
    {
        stateManager = _Manager;
    }

    public override void OnEnter()
    {
        Debug.Log("Entering Wind Agro State");
        SavedPlayerPos = stateManager.PLY_Transform.position;
        
    }

    public override void OnExit()
    {
        Debug.Log("Exiting Wind Agro State");
    }

    public override Type Tick()
    {
        calculateAgroPos();
        checkPlayerPos();
        stateManager.OwlAnim.SetBool("Idle", false);
        DistAgroToOwl = Vector3.Distance(AgroPos, stateManager.transform.position);
        //if owl havent reached position yet
        if (DistAgroToOwl > 0.3)
        {
            //get direction between point and owl
            var direction = AgroPos - stateManager.transform.position;
            //rotate towards point
            Y1 = stateManager.transform.eulerAngles.y;
            stateManager.transform.rotation = Quaternion.Slerp(stateManager.transform.rotation, Quaternion.LookRotation(direction), stateManager.RotationSpeed * Time.deltaTime);
            Y2 = stateManager.transform.eulerAngles.y;
            //move forward
            stateManager.transform.Translate(0, 0, Time.deltaTime * stateManager.MovementSpeed);
            warningTimer = Time.time + stateManager.TimeTillWarning;
            CamShaked = false;
            if(DistAgroToOwl <= stateManager.DistToSlowDown)
            {
                stateManager.SlowMoveSpeed(DistAgroToOwl);
                stateManager.SlowRotSpeed(DistAgroToOwl);
            }
        }
        //if reached the position
        else
        {
            stateManager.OwlAnim.SetBool("Idle", true);
            stateManager.MovementSpeed = stateManager.OGMovementSpeed;
            stateManager.RotationSpeed = stateManager.OGRotationSpeed;
            //get direction to player
            var PPos = stateManager.PLY_Transform.position;
            PPos.y = stateManager.transform.position.y;
            var direction = PPos - stateManager.transform.position;
            //rotate
            Y1 = stateManager.transform.eulerAngles.y;
            stateManager.transform.rotation = Quaternion.Slerp(stateManager.transform.rotation, Quaternion.LookRotation(direction), stateManager.RotationSpeed * Time.deltaTime);
            Y2 = stateManager.transform.eulerAngles.y;


            if (warningTimer - 1 <= Time.time)
            {
                if (!CamShaked)
                {
                    CameraShaker.Instance.ShakeOnce(5.0f, 10.0f, 0.5f, 0.5f);
                    CamShaked = true;
                }
            }

            if (warningTimer <= Time.time)
            {
                stateManager.WindAttack = true;
            }
        }

        //bank rotation
        stateManager.BankRotationCalc(Y1, Y2);

        //if player in close distance go to follow state
        if (stateManager.DisBetwnPLY > stateManager.DistToPatrol)
        {
            return typeof(Owl_PatrolState);
        }

        if (stateManager.WindAttack)
        {
            stateManager.OwlAnim.SetBool("Wind", true);
            return typeof(Owl_WindAttackState);
        }

        return null;
    }

    //calculated the position Owl needs to get to
    private void calculateAgroPos()
    {
        //get pos of owl
        var OwlPos = stateManager.transform.position;
        //set y to players y
        OwlPos.y = SavedPlayerPos.y;
        //get direction to the player
        AgroPos = -(SavedPlayerPos - OwlPos);
        //normalize the direction and add the distant away from the player
        AgroPos = AgroPos.normalized * stateManager.Wind_GroundPos;
        //add to players position
        AgroPos = AgroPos + SavedPlayerPos;
        //add y displacement
        AgroPos.y = SavedPlayerPos.y + stateManager.Wind_YPos;
    }
    //check if player moved too far to reposition
    private void checkPlayerPos()
    {
        if(Vector3.Distance(stateManager.PLY_Transform.position , SavedPlayerPos) > stateManager.DistToReAgro)
        {
            SavedPlayerPos = stateManager.PLY_Transform.position;
            warningTimer = Time.time + stateManager.TimeTillWarning;
        }
    }
}
