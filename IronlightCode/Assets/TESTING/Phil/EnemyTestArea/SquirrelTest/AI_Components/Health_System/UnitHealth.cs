// ----------------------------------------------------------------------------
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

    private Dissolve mDissolveComponent;
    private Phil_StateMa mStateMachine;
    private AI_AbilityManager mAbilityManager;
    
    public GameObject particleDissolve;

   public GameObject _fillImage;  //Health Bar of your AI

    public bool isPlayer;

    public GameObject respchkpnt;
    private Transform _mTarget;
    public GameObject _oPlayer;

    private void Start()
    {
        if (isPlayer)
        {
            _oPlayer = GameObject.FindWithTag("Player").gameObject;
            if (_oPlayer != null)
            {
                _mTarget = _oPlayer.transform;
            }
            else
            {
                Debug.Log("No Player game objects found in the 'Scene'");
            }
        }

        if (ResetHP)
            HP.SetValue(StartingHP);
    }

    private void OnTriggerEnter(Collider other)                                      //For this version we need Trigger Component (ex. Sphere Collider) 
    {
        DamageDealer damage = other.gameObject.GetComponent<DamageDealer>();        //When the projectile Collides get the Component Script "DamageDealer" attached to the Projectile.


        //Weapon Filter [ 19 Weapon -  18 Player ]
        if ((other.gameObject.layer == 19) && this.gameObject.layer != 18)
        {
            //Initialize Components Here, we use this to check where's this Script Attach to ?

            if (this.gameObject.layer == 15) //Squirrel
            {
                mDissolveComponent = GetComponent<Dissolve>();
                mStateMachine = GetComponent<Phil_StateMa>();
                mAbilityManager = GetComponent<AI_AbilityManager>();
            }

            if (this.gameObject.layer == 16) //Owl
            {

            }

            if (this.gameObject.layer == 17) //Snake
            {

            }

         
            if (damage != null)
            {
                HP.ApplyChange(-damage.DamageAmount);
                DamageEvent.Invoke();                                                   //Deal the Damage Event here, using the HP variable attached to this Script.
            }

        }
        else if ((other.gameObject.layer != 19) && this.gameObject.layer == 18)
        {
            if (damage != null)
            {
                HP.ApplyChange(-damage.DamageAmount);
                DamageEvent.Invoke();                                                   //Deal the Damage Event here, using the HP variable attached to this Script.
            }
        }




        if (HP.Value <= 0.0f)
        {
            if(_fillImage)
                _fillImage.SetActive(false);

            DeathEvent.Invoke();                                                                    //Deal the Death Event here, so far no actions yet for Death Event , example trigger Animation Death w/ particles effect

       
       
                if (this.gameObject.layer == 15) // 15 Squirrel
                {
                    mStateMachine.isActive = false;
                    OnSquirrelDeath();                                                              //TO DO: create a script to deal the Animation Death, or write a function private call here to deal the Death actions similar to the HP which is declared above these UnitHealth script.

                }

                if (this.gameObject.layer == 16) //Owl
                {

                }

                if (this.gameObject.layer == 17) //Snake
                {

                }

                if(this.gameObject.layer == 18) //Player
                {
                     //respawn the Player here
                     _oPlayer.transform.position = respchkpnt.GetComponent<RespawnCheckPoint>().lastCheckPoint.transform.position;

                    if (_fillImage)
                        _fillImage.SetActive(true);

                    if (ResetHP)
                        HP.SetValue(StartingHP);
             
                    //  OnPlayerDeath();
                }

        }

    }

    //Put your death Animation , and SetActive False the GameObject
    private void OnOwlDeath()
    {

    }
    //Put your death Animation , and SetActive False the GameObject
    private void OnSnakeDeath()
    {

    }

    private void OnPlayerDeath()
    {
      
            this.transform.position = respchkpnt.GetComponent<RespawnCheckPoint>().lastCheckPoint.transform.position;
    }
    //To Do : Create Death Function here / or create seperate Pluggable Script to deal the Death Event.
    private void OnSquirrelDeath()
    {
        if(mAbilityManager)
            mAbilityManager.enabled = false;

        if (mDissolveComponent)
            mDissolveComponent.enabled = true;

        if (particleDissolve)
            particleDissolve.SetActive(true);

        if (ResetHP)
            HP.SetValue(StartingHP);

    }

}



























































































































































































































































































































































































































































































// Programmer: Phil James
// Date:   01/23/2020
// LinkedIn: https://www.linkedin.com/in/phillapuz/