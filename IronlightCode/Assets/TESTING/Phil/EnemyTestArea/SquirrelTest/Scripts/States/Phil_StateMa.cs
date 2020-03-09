// ----------------------------------------------------------------------------
// Capstone 2020 - IronLight
// 
// Programmer: Phil James    /  Alteration dates below
// Date:   01/20/2020       Version 1
// Date:   01/29/2020       Version 2
// Date:   02/12/2020       Version 3
// ----------------------------------------------------------------------------


using UnityEngine;
using System.Collections;
using System.Collections.Generic;
//-------------------------------------------------------------------------------------------------------------                                      
//                                  -  Please Follow the INSTRUCTION Above -
//-------------------------------------------------------------------------------------------------------------
namespace IronLight
{
    [System.Serializable]
    public class Phil_StateMa : MonoBehaviour
    {
#if UNITY_EDITOR
        [TextArea]
        public string Informative_comments;
#endif
        // -----------------------------------------------------
        // Programmer : Phil
        // Note	:	main idea (interfaces commented below) but Interfaces classes, 
        //			Having Technical issue So we adopt a change (Tweak Main Idea)
        //			So ive Got Advice from Dominic that i have to use Abstract instead of using the interfaces class
        // -----------------------------------------------------


        //public interface BaseState
        //{
        //    public string Name;
        //    void OnEnter();                             // When entering state                                                                             - This is called before the first frame
        //    void Tick();                                // Replacement for Update (Called every frame when it is the current state)
        //    public abstract string CheckConditions();   // Validation and Decission Check and must be Return here                                          - Called every frame after the First Frame 
        //    void OnExit();                              // When Exiting state
        //}


        // -----------------------------------------------------------------------------------------------------------
        // Programmer : Phil
        // Description	:	Objective: create a Base State which is a Systematically, 
        //			kinda like pluggable States, this is to Break the AI Functionality into pieces(Class)
        //			Recommended by Dominic that i have to use Abstract here instead of using the interfaces class
        // Research: This New System got the idea from the (YouTuber) Jason Weimann!
        //           the Enhancement version still in-Progress!!!
        // -----------------------------------------------------------------------------------------------------------

        // -----------------------------------------------------------------------------------------------------------
        // Dominic suggested This Abstract Class , Ideas and Structure was Plan by Danish
        [System.Serializable]
        public abstract class BaseState : ScriptableObject
        {
            public string Name;
            public bool isDead;
            public bool isOnSafeZone;
            public abstract void OnEnter(MonoBehaviour runner);                                     // When entering state
            public abstract void Tick(MonoBehaviour runner);                                        // Replacement for Update (Called every frame when it is the current state)
            public abstract string CheckConditions(MonoBehaviour runner);                           // Validation "Decission Check" , and must be Return here
            public abstract void OnExit(MonoBehaviour runner);                                      // When Exiting state
        }
        // -----------------------------------------------------------------------------------------------------------

        public BaseState[] AvailableStates;                                     // Container for BaseState
        [Header("State On Real Time.")]
        public BaseState CurrentState;                                          // Current BaseState


        [Header("Flag and Decisions.")]
        public bool isActive = true;                                            // Flag for this Class, if "Active" will do the process
        private Transform _mTarget;

        [Header("Components Entity.")]
        private UnityEngine.AI.NavMeshAgent _navMeshAgent;                      //reference to the navmesh agent.
        private Animator _aniMator;
        private Rigidbody _arcRigidBody;

        //local variable
        [HideInInspector] public bool isOnAnimation = false;
        [HideInInspector] private bool isOnAttackMode = false;
        [HideInInspector] private bool isOnSafeMode = false;
         private AI_AbilityManager _updateMinMax;
        private string stateId;
        void Start()                                                            //---------------------------- This is called before the first frame ----------------------------
        {
            _navMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();        // Initialize all the Components
            _aniMator = GetComponent<Animator>();
            _arcRigidBody = GetComponent<Rigidbody>();
            _mTarget = GameObject.FindWithTag("Player").transform;

            if (CurrentState != null)                                           // Precaution Check - If Empty Do Nothing , this save memory calls
            {
                CurrentState.OnEnter(this);                                         // This is called before the first frame
            }
            _updateMinMax = GetComponent<AI_AbilityManager>();

            CurrentState.isOnSafeZone = _mTarget.GetComponent<LightCharging>().isCharging;
        }

        void Update()                                                           //---------------------------- Called every frame after the First Frame ----------------------------
        {
            isInFov = inFOV(this.transform, _mTarget, FacingMaxAngle, maxDistanceToAttack);


            if (isActive == true && CurrentState != null)                       // Precaution Check - if return Empty Do Nothing this save Memory ussage
            {
                CurrentState.isDead = false;
             
                CurrentState.Tick(this);                                       // called once per frame
                stateId = CurrentState.CheckConditions(this);
                if (stateId.Length > 0)                                         // If Empty Do Nothing, save Memory calls
                {
                    foreach (BaseState s in AvailableStates)                    // Let us Update the States   
                    {
                        if (s.Name == stateId)
                        {
                            CurrentState.OnExit(this);                          // ChangeState function call placed here.  
                            CurrentState = s;
                            CurrentState.OnEnter(this);
                            break;
                        }
                    }

                }

            }
            else
            {
         
                if (!isActive)
                    CurrentState.isDead = true;
            }
        }
        private void updateMinMax(string _StateName)
        {


            switch (_StateName)                                                                          //Info : we need Pounce movement, but only for specific States
            {
                case "WanderState":
                    _updateMinMax.Set_MaxDistance = maxDistanceToWander;
                    _updateMinMax.Set_MinDistance = minDistanceToWander;
                    break;
                case "ChaseState":
                    _updateMinMax.Set_MaxDistance = maxDistanceToChase;
                    _updateMinMax.Set_MinDistance = minDistanceToChase;
                    break;
                case "AttackState":
                    _updateMinMax.Set_MaxDistance = maxDistanceToAttack;
                    _updateMinMax.Set_MinDistance = minDistanceToAttack;
                    break;
                default:

                    break;
            }

        }
        public void ChangeState(BaseState state)                                    // This Code has been move & combine to the above Update function call , readiness for future version
        {
            if (state == null)
                return;
            if (CurrentState != null)
            {
                CurrentState.OnExit(this);
            }
            CurrentState = state;
            CurrentState.OnEnter(this);
        }

        public void SetActive(bool isActive)                                        // Update the Flag for this Class
        {
            this.isActive = isActive;
        }



        // To Do: This is for next version, to cater Animation using Animator / Why we need this ? 
        // ans : Unity engine doesnt allow to Jump(manipulate Y-axis thru Animator Root Motion) bec the NavAgent need to stick in the baked NavMesh ground

        public void AgentEventHandler()                                            // Our Event function call for the animator
        {
            if (!isOnAnimation)
            {
                _navMeshAgent.enabled = false;                                    // We need this to allow us to do Animation, and avoid sticking up to the Ground.  
                isOnAnimation = true;
            }
            else
            {
                _navMeshAgent.enabled = true;                                     // Enabled back to allow the NavAgent for Pathfinding / Obstacle avoidance 
                isOnAnimation = false;
            }
        }


        //---------------------------------------------------------------------------------------------------------------------------------
        public float Get_MinDistanceAttack   // property
        {
            get { return minDistanceToAttack; }
            set { minDistanceToAttack = value; }
        }
        public float Get_MaxDistanceAttack   // property
        {
            get { return maxDistanceToAttack; }
            set { maxDistanceToAttack = value; }
        }

        private bool isInFov = false;                                   //Field of View
                                                                        //Local Variable
        private Vector3 fovLine1;                                       //Local Variables
        private Vector3 fovLine2;
        private bool Bool_fovLine1;
        private bool Bool_fovLine2;

        [Header("-----------------------------")]
        [Header("Attack Color Red")]
        public bool ShowAttack_Sensor;

        [Tooltip("Min - Red WireSphere.")]
        [Range(0f, 5f)]
        public float minDistanceToAttack = 2.0f;
        [Tooltip("Max - Red WireSphere.")]
        [Range(0f, 15f)]
        public float maxDistanceToAttack = 10.0f;                       //Circle Radius, Chase Distance


        [Header("FOV Radar Limits")]
        [SerializeField]
        public float FacingMaxAngle = 45f;                              //Facing Angle allertness at Z axis




        public float Get_MinDistanceChase   // property
        {
            get { return minDistanceToChase; }
            set { minDistanceToChase = value; }
        }
        public float Get_MaxDistanceChase   // property
        {
            get { return maxDistanceToChase; }
            set { maxDistanceToChase = value; }
        }


        [Header("-----------------------------")]
        [Header("Chase Color Cyan")]
        public bool ShowChase_Sensor;


        [Tooltip("Min - Cyan WireSphere.")]
        [Range(0f, 10f)]
        public float minDistanceToChase = 10.0f;                        //Minimum Chase Distance
        [Tooltip("Max - Cyan WireSphere.")]
        [Range(0f, 40f)]
        public float maxDistanceToChase = 14.0f;                        //Maximum Chase Distance


        public float Get_MinDistanceWander   // property
        {
            get { return minDistanceToWander; }
            set { minDistanceToChase = value; }
        }
        public float Get_MaxDistanceWander   // property
        {
            get { return maxDistanceToWander; }
            set { maxDistanceToWander = value; }
        }


        [Header("-----------------------------")]
        [Header("Wander Color Green.")]
        public bool ShowWander_Sensor;
        [Tooltip("Min - Green WireSphere.")]
        [Range(0f, 20f)]
        public float minDistanceToWander = 5.0f;
        [Tooltip("Max - Green WireSphere.")]
        [Range(0f, 30f)]
        public float maxDistanceToWander = 20.0f;


        private void OnDrawGizmos()
        {
            if (ShowAttack_Sensor)                                                                                              //For Attack State  
            {
                //Attack Radius WireSphere
                Gizmos.color = Color.red;
                Gizmos.DrawWireSphere(this.transform.position, minDistanceToAttack);

                //Gizmos.color = Color.yellow;
                //Gizmos.DrawWireSphere(transform.position, chase_Distance);

                Gizmos.color = Color.red;
                Gizmos.DrawWireSphere(this.transform.position, maxDistanceToAttack);

                //Let us now Rotate the Vector in Horizontally by multiplying the Vector thru Quaternion
                Vector3 fovLine1 = Quaternion.AngleAxis(FacingMaxAngle, this.transform.up) * this.transform.forward * maxDistanceToAttack;
                Vector3 fovLine2 = Quaternion.AngleAxis(-FacingMaxAngle, this.transform.up) * this.transform.forward * maxDistanceToAttack;

                Gizmos.color = Color.blue;
                Gizmos.DrawRay(this.transform.position, fovLine1);
                Gizmos.DrawRay(this.transform.position, fovLine2);
                //----
                Ray ray1 = new Ray(this.transform.position, fovLine1); RaycastHit hit1;                                          //Checking Left / Right if we hit something ?
                if (Physics.Raycast(ray1, out hit1, maxDistanceToAttack))
                {
                    Bool_fovLine1 = true;
                }
                else { Bool_fovLine1 = false; }

                Ray ray2 = new Ray(this.transform.position, fovLine2); RaycastHit hit2;
                if (Physics.Raycast(ray2, out hit2, maxDistanceToAttack))
                {
                    Bool_fovLine2 = true;
                }
                else { Bool_fovLine1 = false; }

                //----
                if (!isInFov)
                {
                    Gizmos.color = Color.red;
                    // transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, target.transform.localEulerAngles.y, transform.localEulerAngles.z);
                }
                else
                {
                    Gizmos.color = Color.green;
                }
                // transform.LookAt(target);
                if (_mTarget != null)
                {
                    Gizmos.DrawRay(this.transform.position, (_mTarget.position - this.transform.position).normalized * maxDistanceToAttack);
                }
                Gizmos.color = Color.black;
                Gizmos.DrawRay(this.transform.position, this.transform.forward * maxDistanceToAttack);
            }

            if (ShowChase_Sensor)                                                                                                //For Chase State 
            {
                //Minimum Distance WireSphere
                Gizmos.color = Color.cyan;
                Gizmos.DrawWireSphere(this.transform.position, minDistanceToChase);

                //Maximum Distance WireSphere
                Gizmos.color = Color.cyan;
                Gizmos.DrawWireSphere(this.transform.position, maxDistanceToChase);

                if (_mTarget != null)
                {
                    Vector3 dirToTarget = (_mTarget.position - this.transform.position);

                    // Turn the enemy facing to the Player
                    this.transform.rotation = Quaternion.Slerp(this.transform.rotation,
                                        Quaternion.LookRotation(dirToTarget),
                                        1.0f * Time.deltaTime);
                }
            }

            if (ShowWander_Sensor)
            {

                //Minimum Distance WireSphere
                Gizmos.color = Color.green;
                Gizmos.DrawWireSphere(this.transform.position, minDistanceToWander);

                //Maximum Distance WireSphere
                Gizmos.color = Color.green;
                Gizmos.DrawWireSphere(this.transform.position, maxDistanceToWander);

                //Let us now Rotate the Vector in Horizontally by multiplying the Vector thru Quaternion
                Vector3 fovLine1 = Quaternion.AngleAxis(FacingMaxAngle, this.transform.up) * this.transform.forward * maxDistanceToWander;
                Vector3 fovLine2 = Quaternion.AngleAxis(-FacingMaxAngle, this.transform.up) * this.transform.forward * maxDistanceToWander;

                Gizmos.color = Color.blue;
                Gizmos.DrawRay(this.transform.position, fovLine1);
                Gizmos.DrawRay(this.transform.position, fovLine2);

                //if (!isInFov)
                //    Gizmos.color = Color.red;
                //else
                //    Gizmos.color = Color.green;
                //Gizmos.DrawRay(this.transform.position, (wanderPoint - this.transform.position).normalized * maxDistanceToWander);

                Gizmos.color = Color.black;
                Gizmos.DrawRay(this.transform.position, this.transform.forward * maxDistanceToWander);
            }
        }

        public static bool inFOV(Transform checkingObject, Transform target, float maxAngle, float maxRadius)
        {
            Collider[] overlaps = new Collider[50];                                                                //assuming we are surrounded of 100 colliders e.g(mesh collider,sphere, cabsule, box, etc.)
            int count = Physics.OverlapSphereNonAlloc(checkingObject.position, maxRadius, overlaps);

            for (int i = 0; i < count + 1; i++)
            {
                if (overlaps[i] != null)
                {
                    if (overlaps[i].transform == target)
                    {
                        Vector3 directionBetween = (target.position - checkingObject.position).normalized;
                        directionBetween.y *= 0;

                        float angle = Vector3.Angle(checkingObject.forward, directionBetween);

                        if (angle <= maxAngle)
                        {
                            Ray ray = new Ray(checkingObject.position, target.position - checkingObject.position);
                            RaycastHit hit;

                            if (Physics.Raycast(ray, out hit, maxRadius))
                            {
                                if (hit.transform == target)
                                {
                                    overlaps = new Collider[0];                                                                                                  // Helping the GCA 
                                    return true;
                                }
                            }
                        }
                    }
                }
            }

            overlaps = new Collider[0];                                                                                                                         // Helping the GCA 

            return false;
        }
    }
}