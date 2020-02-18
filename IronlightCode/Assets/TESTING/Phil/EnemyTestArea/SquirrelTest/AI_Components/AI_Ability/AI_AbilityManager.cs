// ----------------------------------------------------------------------------
// Capstone 2020 - IronLight
// 
// Programmer: Phil James
// Date:   01/20/2020       Version 1
// Date:   01/29/2020       Version 2
// Date:   02/12/2020       Version 3
// ----------------------------------------------------------------------------
using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System.Runtime.InteropServices;
using IronLight;

public enum AbilityLinkMoveMethod
{
    Orbit,
    Parabola,
    Swag,
    Curve,
    Rollback
}


public class AI_AbilityManager : MonoBehaviour
{
#if UNITY_EDITOR
    [TextArea]
    public string Informative_comments;
#endif

    [Header("Target")]
    private Transform target;
    [HideInInspector] private StateMachine mCurrentBaseState;
    private bool isCharging =false;

    public AbilityLinkMoveMethod m_Method = AbilityLinkMoveMethod.Parabola;
    public AbilityLinkMoveMethod old_Method;
    public AbilityLinkMoveMethod[] attacks;

    public AnimationCurve m_Curve = new AnimationCurve();
    private Vector3 startPos = Vector3.zero;
    private bool _hasReached = true;

    [SerializeField] float _stoppingDistance = 0.5f;
    [SerializeField] float _agentRunSpeed = 5.0f;
    [SerializeField] float _agentWalkSpeed = 1.0f;

    private float timeLeft = 2.0f;
    private float duration = 3f;
    private float time;

    //Local Variable
    private float _gMinDistance; // field
    private float _gMaxDistance; // field
    private string mCurrentState;
    private bool mHopping;
    private bool mWaitOrbit;
    private bool mAttack;
    private int attackIndex = 0;

    [HideInInspector] public float SwagcoolDown = 1.5f;
    [HideInInspector] public float SwagcoolDownTimer;

    [HideInInspector] public float CurvecoolDown = 0.99f;
    [HideInInspector] public float CurvecoolDownTimer;

    [Header("Particle Effects")]
    public SYS_DustScriptableObject runDustEffect;                          //For Pouncing ground dust effect
    public SYS_TendrilScriptableObject runDrillCloudEffect;                 //For Dash + Swag  Flash trail effect
    public SYS_RingScriptableObject runRingEffect;                          //For Pouncing dust effect
    public SYS_GlassScriptableObject runSplatterEffect;

    [HideInInspector] public GameObject _abSorb;
    protected float m_ShieldActivationTime;                                 //Particle effect for the Absorb /recharge before the Swag Attack(dash)
    private ParticleSystem particleTrail;                                   //Particle effect for the Dash



    private NavMeshAgent _navMeshAgent;

    IEnumerator Start()
    {
        target = GameObject.FindWithTag("Player").transform;
        _navMeshAgent = GetComponent<NavMeshAgent>();
        mCurrentBaseState = GetComponent<StateMachine>();

        _abSorb = this.gameObject.transform.GetChild(8).gameObject;                                             //Assigns the first child of the eight child of the Game Object this particle is attached to.      
        particleTrail = GetComponentInChildren<ParticleSystem>();

        _navMeshAgent.autoTraverseOffMeshLink = false;
        startPos = Vector3.zero;


        //Synchronous Coroutine

        while (true)
        {
            if ((!_navMeshAgent.isPathStale) && (!_navMeshAgent.pathPending) && (_navMeshAgent.pathStatus == UnityEngine.AI.NavMeshPathStatus.PathComplete))
            {
                mCurrentState = mCurrentBaseState.CurrentState.Name;
                if (mCurrentState == null) { yield break; }

                isCharging = target.GetComponentInChildren<LightCharging>().isCharging;                         //Real Time Check
               
                switch (mCurrentState)                                                                          //Info : we need Pounce movement, but only for specific States
                {
                    case "WanderState":
                        mHopping = true;
                        break;
                   case "ChaseState":
                        mHopping = true;
                        break;
                    case "AttackState":
                        mAttack = true;
                        break;
                    default:
                        mHopping = false;
                        break;
                }
                                                                                                                //  m_Method = (attacks[Random.Range(0, attacks.Length)]);

                m_Method = (attacks[attackIndex]);

                //if (old_Method != m_Method)
                //{

                if ((Vector3.Distance(this.transform.position, target.position) < _gMaxDistance))
                {
                    if (Vector3.Distance(this.transform.position, target.position) > _gMinDistance)              // Current State <Patrol State>
                    {
                        if ((m_Method == AbilityLinkMoveMethod.Swag) && (isCharging != true) && (mCurrentBaseState.isActive != false) && (mAttack))                     //Execute the Function if the Player is Not Charging & not Dead yet
                            yield return StartCoroutine(ActivateShield(_navMeshAgent));
                        else if ((m_Method == AbilityLinkMoveMethod.Parabola) && (isCharging != true) && (mCurrentBaseState.isActive != false))                        //Execute the Function if the Player is Not Charging & not Dead yet
                            yield return StartCoroutine(Parabola(_navMeshAgent, 0.8f, 1f));
                        else if ((m_Method == AbilityLinkMoveMethod.Curve) && (isCharging != true) && (mCurrentBaseState.isActive != false))                            //Execute the Function if the Player is Not Charging & not Dead yet
                            yield return StartCoroutine(Curve(_navMeshAgent, 1f));

                    }
                    else
                    {
                        particleTrail.Stop();
                    }
                }
                else
                {
                    particleTrail.Stop();
                }

                attackIndex++;
                //}
                //old_Method = m_Method;

                if (attackIndex >= attacks.Length) { attackIndex = 0;  }

            }

            yield return null;
        }
      
    }
        
    IEnumerator Swag(NavMeshAgent agent)
    {
        Vector3 endPos = agent.pathEndPosition;                                                    
        startPos = transform.position;
        Vector3 dirToTarget = (agent.pathEndPosition - agent.transform.position).normalized;                                    //Get the difference
        Vector3 attackPosition = transform.position + dirToTarget * (_gMaxDistance + _gMaxDistance);                            // We force the AI to pass through in a Player Position

        Vector3 newPosition = transform.position - dirToTarget * (_gMaxDistance);

        if (SwagcoolDownTimer > 0.95f) { SwagcoolDownTimer -= Time.deltaTime; }
        if (SwagcoolDownTimer < 0.95f) {  SwagcoolDownTimer = 0; }    
        if (SwagcoolDownTimer == 0)
        {
            if (Vector3.Distance(agent.transform.position, target.position) <= _gMaxDistance)                                   // Verify if the player still in the Perimeter 
            {
                RaycastHit hit; int mask = 1 << 10;                                                                             // Now lets check if Grounded , Ground on layer 10 in the inspector
                if (Physics.Raycast(transform.position, Vector3.down, out hit, 2f, mask))                                       // Let's use Physics to verify
                {
                    float _mOldSpeed = agent.speed; float percent = 0;
                    while (percent <= 3f)
                    {                     
                        percent += Time.deltaTime; float interpolation = (-Mathf.Pow(percent, 2) + percent) * 5f;                      
                        agent.speed = _agentRunSpeed;
                        agent.destination = Vector3.Lerp(startPos, attackPosition, interpolation);                             // we are using agent to get the destination, no Callbacks needed right on the spot can determine if the path is Stale/invalid e.g ( Player runaway and hide from the bushes) 
                        old_Method = AbilityLinkMoveMethod.Swag;
                        yield return null;
                    }
                    agent.speed = _mOldSpeed;
                    // transform.position= Vector3.MoveTowards(transform.position, startPos, Time.deltaTime * 0.5f);
                }
                SwagcoolDownTimer = SwagcoolDown;

            }
        }

        mAttack = false;
        yield return null;
    }
        
    IEnumerator Parabola(NavMeshAgent agent, float height, float duration)
    {
        if (!mHopping) { yield return null; }                                                                          // Hopping /pouncing code below is for specific States only, not all states can exeucte the code below
        startPos = agent.transform.position; Vector3 endPos = agent.transform.position + Vector3.up * agent.baseOffset; float normalizedTime = 0.1f;
        while (normalizedTime < 1f)
        {
                particleTrail.Stop(); normalizedTime += Time.deltaTime;
                float yOffset = height * (normalizedTime - normalizedTime * normalizedTime);
                transform.position = Vector3.Lerp(startPos, endPos, normalizedTime) + yOffset * Vector3.up;
                old_Method = AbilityLinkMoveMethod.Parabola;
                yield return null;                             
        }

        RaycastHit hit; int mask = 1 << 10;                                                                                 // Now lets check if Grounded , Ground on layer 10 in the inspector
        if (Physics.Raycast(agent.transform.position, Vector3.down, out hit, 2f, mask))                                     // Let's use Physics to verify
        {
               if(runDustEffect !=null){ StartCoroutine(runDustEffect.DustCoroutine(this)); }
        }
        yield return null;
    }
           
    IEnumerator Curve(NavMeshAgent agent, float duration)
    {
        RaycastHit hit;                                                                                                   // Now lets check if Grounded
        int mask = 1 << 10;                                                                                               // Ground on layer 10 in the inspector
        Vector3 startPos = agent.transform.position;
        if (!mHopping) { yield return null; }
        if (Vector3.Distance(agent.transform.position, target.position) <= _gMaxDistance)
        {
            if (Physics.Raycast(agent.transform.position, Vector3.down, out hit, 2f, mask))                               // Let's use Physics to verify
            {
                Vector3 endPos = target.position + Vector3.up * agent.baseOffset;
                Vector3 NewPosition = startPos + Vector3.up * agent.baseOffset;

                float normalizedTime = 0.0f;
                while (normalizedTime < 1f)
                {
                    particleTrail.Stop();
                    float yOffset = m_Curve.Evaluate(normalizedTime);
                    agent.transform.position = Vector3.Lerp(startPos, endPos, normalizedTime) + yOffset * Vector3.up;
                    normalizedTime += Time.deltaTime / duration;

                    old_Method = AbilityLinkMoveMethod.Curve;
                    yield return null;
                }

                if (Physics.Raycast(agent.transform.position, Vector3.down, out hit, 2f, mask))                               // Let's use Physics to verify
                {
                    if (runSplatterEffect != null)
                    { StartCoroutine(runSplatterEffect.GlassCoroutine(this)); }
                }
            }
        }


      //  Debug.Log("curve");
        yield return null;
    }
   

    IEnumerator ActivateShield(NavMeshAgent agent)
    {
        _abSorb.SetActive(true);
        m_ShieldActivationTime = 1f;
        while (m_ShieldActivationTime > 0)
        {
            agent.isStopped = true;
            m_ShieldActivationTime -= Time.deltaTime;
            if (m_ShieldActivationTime <= 0.0f)
            {
                agent.isStopped = false;
                yield return StartCoroutine(DeactivateShield(agent));
            }
            yield return null;
        }       
    }

    IEnumerator DeactivateShield(NavMeshAgent agent)
    {
        _abSorb.SetActive(false);
     
        //  m_Damageable.SetColliderState(true);

        if (agent.isOnNavMesh == true) 
        { particleTrail.Play(); }
        else if (agent.isStopped)
        { particleTrail.Stop(); }

        yield return StartCoroutine(Swag(agent));
    }

    public float Set_MinDistance   // property
    {
        get { return _gMinDistance; }
        set { _gMinDistance = value; }
    }

    public float Set_MaxDistance   // property
    {
        get { return _gMaxDistance; }
        set { _gMaxDistance = value; }
    }

    public void OnTriggerEnter(Collider other)
    {
        //For this Version we use Collision Trigger -  This can be change into a Projectile (Blast) that Collide into this Orbs GameObject , basically when the Blast Hit then set isActive into True! 
        if (other.gameObject.tag == "Player")
        {
            if (runRingEffect != null)
            {
                StartCoroutine(runRingEffect.RingHorizontalCoroutine(this, other.transform.position));
            }
        }
    }


    // ----------------------------------------------------------------------------
    //                  Codes Below are for Testing pursposes and reference for future changes
    // 
    // Programmer: Phil James
    // Date:   01/23/2020
    //
    //     
    //
    // ----------------------------------------------------------------------------

    IEnumerator NormalSpeed(NavMeshAgent agent)
    {
        Vector3 endPos = agent.pathEndPosition; // + Vector3.up * agent.baseOffset;
        while (agent.transform.position != endPos)
        {
            agent.transform.position = Vector3.MoveTowards(agent.transform.position, endPos, agent.speed * Time.deltaTime);
            yield return null;
        }

    }

    IEnumerator Rotate(NavMeshAgent agent)
    {
           Debug.Log("Coroutine Orbit Created.");

        bool completed_OneCycle = false;
        float maxDistanceToAttack = 2f;
        float rotationSpeed = 2f;
        float radiusSpeed = 2f;

        //  myCollisionRadius = runner.transform.GetComponent<CapsuleCollider>().radius;
        // targetCollisionRadius = target.transform.GetComponent<CapsuleCollider>().radius;


        //Since we dont have Obstacle Avoidance Agent like NavAgent, then we have to suffer making our Own
        //we need to Check for any Obstacle in front.
        RaycastHit hit; int range = 5;

        // Two rays left and right to the object to detect the obstacle.
        Transform leftRay = agent.transform; Transform rightRay = agent.transform;

        // DrawLine for debugging.
        Debug.DrawRay(leftRay.position + (agent.transform.right * 2), agent.transform.forward * 2, Color.green);
        Debug.DrawRay(rightRay.position - (agent.transform.right * 2), agent.transform.forward * 2, Color.green);
        //Use Phyics.RayCast to detect the obstacle
        if (Physics.Raycast(leftRay.position + (agent.transform.right * 2), agent.transform.forward * 2, out hit, range))
        {
            // this gonna be reduntant switching this boolean flag, but it works and so that it will easily to understand the logic behind here
            completed_OneCycle = false;

        }
        else if (Physics.Raycast(rightRay.position - (agent.transform.right * 2), agent.transform.forward * 2, out hit, range))
        {

            completed_OneCycle = true;

        }
        // Use to debug the Physics.RayCast.
        Debug.DrawRay(agent.transform.position - (agent.transform.forward * 2), agent.transform.right * 2, Color.green);
        Debug.DrawRay(agent.transform.position - (agent.transform.forward * 2), -agent.transform.right * 2, Color.green);

        // Now Two More RayCast At The End of Object to detect that object has already pass the obsatacle.
        // Just making this boolean variable false it means there is nothing in front of object.
        if (Physics.Raycast(agent.transform.position - (agent.transform.forward * 2), agent.transform.right * 2, out hit, range))
        {

            completed_OneCycle = false;

        }
        else if (Physics.Raycast(agent.transform.position - (agent.transform.forward * 2), -agent.transform.right * 2, out hit, range))
        {

            completed_OneCycle = true;

        }

        //Get the Latest Magnitude Distance
        float sqrDstToTarget = (agent.transform.position - agent.transform.position).sqrMagnitude;


        //attack_Distance //Mathf.Pow(maxDistanceToAttack + myCollisionRadius + targetCollisionRadius, 2))
        if (sqrDstToTarget < Mathf.Pow(maxDistanceToAttack + 0.1f + 0.1f, 2))
        {

            //We need to Get the precise/latest Transform Position
            //  Transform playerPosition = GameObject.FindWithTag("Player").transform;

            Vector3 dirToTarget = (agent.transform.position - agent.transform.position).normalized;
            Vector3 attackPosition = agent.pathEndPosition - dirToTarget * (maxDistanceToAttack);

            //This part is important , we need to makesure our AI facing the Player and is in the correct distance
            Vector3 axis = Vector3.up;
            //  agent.transform.LookAt(agent);
            agent.transform.position = attackPosition;


            //Forcing to reset the Rotation
            if (completed_OneCycle == true)
            { agent.transform.RotateAround(agent.pathEndPosition, axis, -rotationSpeed * Time.deltaTime); }
            else
            { agent.transform.RotateAround(agent.pathEndPosition, axis, rotationSpeed * Time.deltaTime); }


            agent.transform.position = Vector3.MoveTowards(agent.transform.position, attackPosition, Time.deltaTime * radiusSpeed);
            //   agent.transform.LookAt();



        }

        yield return null;

        //   Debug.Log("Ability Orbit enables coroutineTrigger to run.");
    }
    
    IEnumerator Rollback(NavMeshAgent agent, Vector3 startPos)
    {

        Vector3 endPos = startPos + Vector3.up * agent.baseOffset;
        while (agent.transform.position != endPos)
        {
            agent.transform.position = Vector3.MoveTowards(agent.transform.position, endPos, agent.speed * Time.deltaTime);

            if (_hasReached)
            {
                yield return null;
            }
            yield return null;
        }

        yield return null;

    }

    IEnumerator Parabola2(NavMeshAgent agent, float height, float duration)
    {
        startPos = agent.transform.position;

        Vector3 _mTarget = Vector3.zero;
        if (isCharging)
        {
            _mTarget = agent.transform.position;
        }
        else
        {
            _mTarget = agent.pathEndPosition;
        }


        Vector3 dirToTarget = (_mTarget - agent.transform.position).normalized;
        Vector3 attackPosition = _mTarget - dirToTarget * (1f);
        Vector3 newPosition = _mTarget - dirToTarget * (1f);

        agent.transform.position = attackPosition;
        
        Vector3 endPos = _mTarget + Vector3.up;// * agent.baseOffset;

        float normalizedTime = 0.0f;
        while (normalizedTime < 1.0f)
        {
            float yOffset = 1f * 0.1f * (normalizedTime - normalizedTime * normalizedTime);
            agent.transform.position = Vector3.Lerp(startPos, endPos, normalizedTime) + yOffset * Vector3.up;
            normalizedTime += Time.deltaTime / duration;
            yield return null;

        }


        agent.transform.position = Vector3.MoveTowards(agent.transform.position, newPosition, Time.deltaTime * 0.1f);
                
        yield return null;
    }
    
    protected bool pathComplete()
    {
        //if (Vector3.Distance(m_NavAgent.destination, m_NavAgent.transform.position) <= m_NavAgent.stoppingDistance)
        //{
        //    if (!m_NavAgent.hasPath || m_NavAgent.velocity.sqrMagnitude == 0f)
        //    {
        //        return true;
        //    }
        //}

        return false;
    }
}

















































































































































































// Programmer : Phil James Lapuz
// Linked-in Profile : www.linkedin.com/in/philjameslapuz
// Youtube Channel : Don Philifeh