// ----------------------------------------------------------------------------
// Capstone 2020 - IronLight
// 
// Programmer: Phil James
// Date:   01/29/2020       Version 1
// ----------------------------------------------------------------------------
using UnityEngine;
using UnityEngine.AI;
using System.Collections;

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
    public bool isCharging =false;

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

    IEnumerator Start()
    {
        target = GameObject.FindWithTag("Player").transform;
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        agent.autoTraverseOffMeshLink = false;
        startPos = Vector3.zero;

        while (true)
        {
            if ((!agent.isPathStale) && (!agent.pathPending) && (agent.pathStatus == UnityEngine.AI.NavMeshPathStatus.PathComplete))
            {

               

                isCharging = target.GetComponentInChildren<LightCharging>().isCharging;                         //Real Time Check
                m_Method = (attacks[Random.Range(0, attacks.Length)]);


                if(old_Method != m_Method) 
                {


                    if (m_Method == AbilityLinkMoveMethod.Swag)
                        yield return StartCoroutine(Swag(agent));
                    else if (m_Method == AbilityLinkMoveMethod.Parabola)
                        yield return StartCoroutine(Parabola(agent, 2.0f, 0.5f));
                    else if (m_Method == AbilityLinkMoveMethod.Curve)
                        yield return StartCoroutine(Curve(agent, 0.5f));

                    //agent.speed = 30f;
                    //agent.CompleteOffMeshLink();
                }
                old_Method = m_Method;

               
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
        Vector3 attackPosition = agent.pathEndPosition - dirToTarget * (1f);
        Vector3 newPosition = agent.pathEndPosition - dirToTarget * (1f);

        if (isCharging) { yield break; }
       
        if (Vector3.Distance(agent.transform.position, agent.pathEndPosition) <= _gMaxDistance)
        {
            float percent = 0;
            while (percent <= 3)
            {
                percent += Time.deltaTime * 5f;

                float interpolation = (-Mathf.Pow(percent, 2) + percent) * 5;
                agent.transform.position = Vector3.Lerp(startPos, attackPosition, interpolation);
                
                yield return null;

            }

            agent.transform.position = Vector3.MoveTowards(agent.transform.position, newPosition, Time.deltaTime * 0.1f);

        }
        yield break;
    }
        
    IEnumerator Parabola(NavMeshAgent agent, float height, float duration)
    {
        startPos = agent.transform.position;
           
        if (isCharging) {  yield break; }                                                               // Force to stop
        

        Vector3 endPos = agent.pathEndPosition + Vector3.up;// * agent.baseOffset;

        if (Vector3.Distance(agent.transform.position, agent.pathEndPosition) <= _gMaxDistance)
        {
            float normalizedTime = 0.0f;
            while (normalizedTime < 1.0f)
            {
                float yOffset = height * 2f * (normalizedTime - normalizedTime * normalizedTime);
                agent.transform.position = Vector3.Lerp(startPos, endPos, normalizedTime) + yOffset * Vector3.up;
                normalizedTime += Time.deltaTime / duration;
                yield return null;

            }

        }
        yield break;
    }
           
    IEnumerator Curve(NavMeshAgent agent, float duration)
    {

        Vector3 startPos = agent.transform.position;

        if (isCharging) { yield break; }                                                               // Force to stop

        if (Vector3.Distance(agent.transform.position, agent.pathEndPosition) <= _gMaxDistance)
        {
            Vector3 endPos = agent.pathEndPosition + Vector3.up * agent.baseOffset;
            Vector3 NewPosition = startPos + Vector3.up * agent.baseOffset;

            float normalizedTime = 0.0f;
            while (normalizedTime < 1.0f)
            {
                float yOffset = m_Curve.Evaluate(normalizedTime);
                agent.transform.position = Vector3.Lerp(startPos, endPos, normalizedTime) + yOffset * Vector3.up;
                normalizedTime += Time.deltaTime / duration;

                yield return null;
            }
            Debug.Log("Curve");
        }

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