﻿// ----------------------------------------------------------------------------
// Capstone 2020 - IronLight
// 
// Programmer: Phil James
// Date:   01/23/2020
//
// Description : Attached this script to the e.g( AI enemy or Player )
//             + This script require a pluggable Variables e.g ( HP_Squirrel , HP_Owl, HP_Snake,  or HP_Player ).
//                  >> Pluggable Variables can be created thru(Assets>Create>Health System>New Variable)
//             + Pluggable Events is needed too , for DamageEvent / DeathEvent
//                  >> Pluggable Events can be created thru (Assets>Create>Health System >New HP events)
// ----------------------------------------------------------------------------

using UnityEngine;
using UnityEngine.Events;
using IronLight;

public class UnitHealth : MonoBehaviour
{
    public FloatVariable HP;                                     //Plug the Variable here e.g (HP_Owl or HP_Player)

    public bool ResetHP;                                        //If you set Active, the above attach Variable will be reset When the Enemy or Player  respawn.
    public FloatReference StartingHP;                           //Plug the HP variable ,contain the setup Health value . eg (Max_HP , Min_HP)
    public UnityEvent DamageEvent;                              //Plug your Event here for Damage
    public UnityEvent DeathEvent;                               //Plug your Event here for Death

  //  public UnityEvent OnReceiveDamage;

    public Dissolve mDissolveComponent;
    private StateMachine mStateMachine;
    private AI_AbilityManager mAbilityManager;
    
    public GameObject particleDissolve;



    private void Start()
    {
        mDissolveComponent = GetComponent<Dissolve>();
        mStateMachine = GetComponent<StateMachine>();
        mAbilityManager = GetComponent<AI_AbilityManager>();
     //   OnDissolve = GetComponentInChildren<ParticleSystem>();

        if (ResetHP)
            HP.SetValue(StartingHP);
    }

    private void OnTriggerEnter(Collider other)                                      //For this version we need Trigger Component (ex. Sphere Collider) 
    {
        DamageDealer damage = other.gameObject.GetComponent<DamageDealer>();        //When the projectile Collides get the Component Script "DamageDealer" attached to the Projectile.
        if (damage != null)
        {
            HP.ApplyChange(-damage.DamageAmount);
            DamageEvent.Invoke();                                                   //Deal the Damage Event here, using the HP variable attached to this Script.
        }

        if (HP.Value <= 0.0f)
        {


            DeathEvent.Invoke();                                                    //Deal the Death Event here, so far no actions yet for Death Event , example trigger Animation Death w/ particles effect


            OnDeath();                                                       //TO DO: create a script to deal the Animation Death, or write a function private call here to deal the Death actions similar to the HP which is declared above these UnitHealth script.
            if (ResetHP)
                HP.SetValue(StartingHP);
        }
        //else                                                                        //Lets do damage Hit Effect  
        //{
        //    OnReceiveDamage.Invoke();
        //}
    }

    //To Do : Create Death Function here / or create seperate Pluggable Script to deal the Death Event.
    private void OnDeath()
    {
        mStateMachine.isActive = false;
        mAbilityManager.enabled = false;
        mDissolveComponent.enabled = true;
        particleDissolve.SetActive(true);

        if (ResetHP)
            HP.SetValue(StartingHP);

    }

}



























































































































































































































































































































































































































































































// Programmer: Phil James
// Date:   01/23/2020
// LinkedIn: https://www.linkedin.com/in/phillapuz/