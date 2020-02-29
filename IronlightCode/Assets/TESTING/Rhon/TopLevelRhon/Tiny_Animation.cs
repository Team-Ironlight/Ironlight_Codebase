using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Danish.StateCode;

public class Tiny_Animation 
{
    Animator anim;
    dStateManager Manager;


    float forward = 0;
    float straffe = 0;
    bool dash = false;

    bool orbAttack = false;
    bool beamAttack = false;
    bool blastAttack = false;
    

    public void Init(Animator _anim, dStateManager _manager)
    {
        anim = _anim;
        Manager = _manager;
    }

    public void Tick()
    {
        moveAnimations(Manager.moveVector);
        dashAnimations(Manager.isDashing);
        orbAnimations(Manager.launchOrb);
        beamAnimations(Manager.launchBeam);
        blastAnimations(Manager.launchBlast);
    }


    private void moveAnimations(Vector2 input)
    {
        if(forward != input.y)
        {
            forward = Mathf.Lerp(forward, input.y, 0.1f);
        }
        if(straffe != input.x)
        {
            straffe = Mathf.Lerp(straffe, input.x, 0.1f);
        }
    }

    private void dashAnimations(bool dashing)
    {
        if(dash != dashing)
        {
            dash = true;
            anim.SetBool("Dash", true);
        }
    }

    private void orbAnimations(bool orbAtk)
    {
        if(orbAttack != orbAtk)
        {
            orbAtk = true;
            anim.SetBool("Orb", true);
        }
    }

    private void beamAnimations(bool beamAtk)
    {
        if (beamAttack != beamAtk)
        {
            beamAtk = true;
            anim.SetBool("Beam", true);
        }
    }

    private void blastAnimations(bool blastAtk)
    {
        if (blastAttack != blastAtk)
        {
            blastAtk = true;
            anim.SetBool("Blast", true);
        }
    }


}
