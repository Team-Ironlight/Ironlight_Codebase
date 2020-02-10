// ----------------------------------------------------------------------------
// Capstone 2020 - IronLight
// 
// Programmer: Phil James
// Date:   01/29/2020       Version 1
// ----------------------------------------------------------------------------
using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using IronLight;

public enum AbilityLinkMoveMethod
{
    Orbit,
    Parabola,
    Swag,
    Curve,
    Rollback
}

[RequireComponent(typeof(NavMeshAgent))]
public class AI_AbilityManager : MonoBehaviour
{
    [Header("Target")]
    private Transform target;
    private StateMachine mCurrentBaseState;
    private bool isCharging =false;

    public AbilityLinkMoveMethod m_Method = AbilityLinkMoveMethod.Parabola;
    public AbilityLinkMoveMethod old_Method;
    public AbilityLinkMoveMethod[] attacks;

    public AnimationCurve m_Curve = new AnimationCurve();
    private Vector3 startPos = Vector3.zero;
    private bool _hasReached = true;

    [SerializeField] float _stoppingDistance = 0.5f;

    private float timeLeft = 2.0f;
    private float duration = 3f;
    private float time;

    //Local Variable
    private float _gMinDistance; // field
    private float _gMaxDistance; // field
    private string mCurrentState;
    private bool mHopping;

    private int attackIndex = 0;

    [HideInInspector] public float SwagcoolDown = 1.5f;
    [HideInInspector] public float SwagcoolDownTimer;

    [HideInInspector] public float CurvecoolDown = 0.99f;
    [HideInInspector] public float CurvecoolDownTimer;

    [Header("Particle Effects")]
    public SYS_DustScriptableObject runDustEffect;                          //For Pouncing ground dust effect
    public SYS_TendrilScriptableObject runDrillCloudEffect;                 //For Dash + Swag  Flash trail effect
    public SYS_RingScriptableObject runRingEffect;                          //For Pouncing dust effect

    private ParticleSystem particleTrail;
    IEnumerator Start()
    {
        target = GameObject.FindWithTag("Player").transform;
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        mCurrentBaseState = GetComponent<StateMachine>();

        particleTrail = GetComponentInChildren<ParticleSystem>();

        agent.autoTraverseOffMeshLink = false;
        startPos = Vector3.zero;

        if(mCurrentBaseState.isActive ==false) { yield return null; }
     

        while (true)
        {
            if ((!agent.isPathStale) && (!agent.pathPending) && (agent.pathStatus == UnityEngine.AI.NavMeshPathStatus.PathComplete))
            {
                mCurrentState = mCurrentBaseState.CurrentState.Name;
             
                switch (mCurrentState)                                      //Info : we need Pounce movement but only for specific States
                {
                    case "WanderState":
                        mHopping = true;
                        break;
                    case "ChaseState":
                        mHopping = true;
                        break;
                    default:
                        mHopping = false;
                        break;
                }


        

                isCharging = target.GetComponentInChildren<LightCharging>().isCharging;                         //Real Time Check
                                                                                                                //  m_Method = (attacks[Random.Range(0, attacks.Length)]);

                m_Method = (attacks[attackIndex]);

               // Debug.Log(attackIndex);
                //if (old_Method != m_Method)
                //{
                    if (m_Method == AbilityLinkMoveMethod.Swag)
                        yield return StartCoroutine(Swag(agent));
                    else if (m_Method == AbilityLinkMoveMethod.Parabola)
                        yield return StartCoroutine(Parabola(agent, 1.0f, 1f));
                    else if (m_Method == AbilityLinkMoveMethod.Curve)
                        yield return StartCoroutine(Curve(agent, 0.5f));

                    //agent.speed = 30f;
                    //agent.CompleteOffMeshLink();

                    attackIndex++;
                //}
                //old_Method = m_Method;

                if (attackIndex >= attacks.Length)
                {
                    attackIndex = 0;
                }


                //RaycastHit hit;                                                                                               // Now lets check if Grounded
                //int mask = 1 << 12;
                //if (Physics.Raycast(agent.transform.position, Vector3.forward, out hit, 4.7f, mask))                               // Let's use Physics to verify
                //{
                //    Debug.Log("Player");
                //}
                //Debug.DrawRay(agent.transform.position, agent.transform.TransformDirection(Vector3.forward) * hit.distance, Color.green);
            }

            yield return null;
        }

    }
        
    IEnumerator Swag(NavMeshAgent agent)
    {
        Vector3 endPos = agent.pathEndPosition;                                                    //Initialized
        
        //Get the position value, use this to show the Player was stepping backward 
        startPos = agent.transform.position;
        //Get the difference
        Vector3 dirToTarget = (agent.pathEndPosition - agent.transform.position).normalized;
        Vector3 attackPosition = agent.transform.position - dirToTarget * (_gMaxDistance);
        Vector3 newPosition = agent.transform.position - dirToTarget * (_gMaxDistance);

        if (isCharging) { yield break; }

        if (mCurrentBaseState.isActive == false) { yield break; }

        if (SwagcoolDownTimer > 0.95f)
        {
            SwagcoolDownTimer -= Time.deltaTime;
        }
        if (SwagcoolDownTimer < 0.95f)
        {
            SwagcoolDownTimer = 0;
        }


        if (SwagcoolDownTimer == 0)
        {
            //if (Vector3.Distance(agent.transform.position, agent.pathEndPosition) <= _gMaxDistance)
            //{


            //RaycastHit hit;                                                                                               // Now lets check if Grounded
            //int mask = 1 << 10;                                                                                            // Ground on layer 10 in the inspector

            //if (Physics.Raycast(agent.transform.position, Vector3.down, out hit, 2f, mask))                               // Let's use Physics to verify
            //{
            //   StartCoroutine(runDrillCloudEffect.DrilCloudCoroutine(this));
            //}
            //Debug.DrawRay((new Vector3(agent.transform.position.x, agent.transform.position.y + 1f, agent.transform.position.z)), Vector3.down, Color.green, 2);

            if((agent.isOnNavMesh == true) && (agent.velocity.sqrMagnitude > 0))
            {
                particleTrail.Play();
            }
            else if(agent.isStopped)
            {
                particleTrail.Stop();
            }
                


            float percent = 0;
            while (percent <= 3)
            {
                percent += Time.deltaTime * 5f;

                float interpolation = (-Mathf.Pow(percent, 2) + percent) * 5;
                agent.transform.position = Vector3.Lerp(startPos, attackPosition, interpolation);


               
                old_Method = AbilityLinkMoveMethod.Swag;
                yield return null;

            }

            agent.transform.position = Vector3.MoveTowards(agent.transform.position, newPosition, Time.deltaTime * 0.5f);

            SwagcoolDownTimer = SwagcoolDown;


         
        }
        //}
        Debug.Log("Swag");
        yield break;
    }
        
    IEnumerator Parabola(NavMeshAgent agent, float height, float duration)
    {
        startPos = agent.transform.position;


        //  if ((isCharging) || (!mHopping)) {  yield break; }                                                               // Force to stop
        if (mCurrentBaseState.isActive == false) { yield break; }

        if (!mHopping) { yield break; }                                                                                   // Hopping /pouncing code below is only for specific States, not all states can exeucte the code below

        Vector3 endPos = agent.pathEndPosition + Vector3.up;// * agent.baseOffset;

        //if (Vector3.Distance(agent.transform.position, agent.pathEndPosition) <= _gMaxDistance)                           //Precaution check - Player is on perimeter  
        //{

       
       

        float normalizedTime = 0.1f;
  
            while (normalizedTime < 1f)
            {
            particleTrail.Stop();

            float yOffset = height * 2f * (normalizedTime - normalizedTime * normalizedTime);
                agent.transform.position = Vector3.Lerp(startPos, endPos, normalizedTime) + yOffset * Vector3.up;
                normalizedTime += Time.deltaTime / duration;

            yield return null;
                              
            }



        // }
        // agent.isOnNavMesh = true
        // agent.velocity.sqrMagnitude = 0

    //    Debug.Log("parabola = " + agent.velocity.sqrMagnitude);

        RaycastHit hit;                                                                                               // Now lets check if Grounded
       int mask = 1 << 10;                                                                                            // Ground on layer 10 in the inspector

        if (Physics.Raycast(agent.transform.position, Vector3.down, out hit, 2f, mask))                               // Let's use Physics to verify
        {
          //  Debug.Log("is grounded");

            StartCoroutine(runDustEffect.DustCoroutine(this));
        }
     //   Debug.DrawRay((new Vector3(agent.transform.position.x, agent.transform.position.y + 1f, agent.transform.position.z)), Vector3.down, Color.green, 2);


   

        yield return null;
    }
           
    IEnumerator Curve(NavMeshAgent agent, float duration)
    {

        Vector3 startPos = agent.transform.position;

        if (isCharging) { yield break; }                                                               // Force to stop

        if (mCurrentBaseState.isActive == false) { yield break; }

        if (CurvecoolDownTimer > 0.98f)
        {
            CurvecoolDownTimer -= Time.deltaTime;
        }
        if (CurvecoolDownTimer < 0.98f)
        {
            CurvecoolDownTimer = 0;
        }

        if (CurvecoolDownTimer == 0)
        {
            if (Vector3.Distance(agent.transform.position, agent.pathEndPosition) <= _gMaxDistance)
            {
                Vector3 endPos = agent.pathEndPosition + Vector3.up * agent.baseOffset;
                Vector3 NewPosition = startPos + Vector3.up * agent.baseOffset;

                float normalizedTime = 0.0f;
                while (normalizedTime < 1.0f)
                {
                particleTrail.Stop();

                float yOffset = m_Curve.Evaluate(normalizedTime);
                    agent.transform.position = Vector3.Lerp(startPos, endPos, normalizedTime) + yOffset * Vector3.up;
                    normalizedTime += Time.deltaTime / duration;

                    yield return null;
                }


            RaycastHit hit;                                                                                               // Now lets check if Grounded
            int mask = 1 << 10;                                                                                            // Ground on layer 10 in the inspector

            if (Physics.Raycast(agent.transform.position, Vector3.down, out hit, 2f, mask))                               // Let's use Physics to verify
            {
                //  Debug.Log("is grounded");
            //    hit.point
                StartCoroutine(runDustEffect.DustCoroutine(this));
            }
          



            CurvecoolDownTimer = CurvecoolDown;
            }
        }

        Debug.Log("curve");
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

            StartCoroutine(runRingEffect.RingHorizontalCoroutine(this,other.transform.position));
            Debug.Log("Player Collide");
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
        //   Debug.Log("Coroutine Orbit Created.");

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
                yield break;
            }
            yield return null;
        }
   
        yield break;

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



        yield break;
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