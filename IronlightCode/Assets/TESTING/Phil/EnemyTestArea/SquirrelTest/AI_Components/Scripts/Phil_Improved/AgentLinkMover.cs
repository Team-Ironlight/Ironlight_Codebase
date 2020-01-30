// ----------------------------------------------------------------------------
// Capstone 2020 - IronLight
// 
// Programmer: Phil James
// Date:   01/29/2020       Version 1
// ----------------------------------------------------------------------------

using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public enum OffMeshLinkMoveMethod
{
    Orbit,
    Parabola,
    Swag,
    Curve,
    Rollback
}

[RequireComponent(typeof(NavMeshAgent))]
public class AgentLinkMover : MonoBehaviour
{
    

    public OffMeshLinkMoveMethod m_Method = OffMeshLinkMoveMethod.Parabola;
    public OffMeshLinkMoveMethod old_Method;
    public OffMeshLinkMoveMethod[] attacks;

    public AnimationCurve m_Curve = new AnimationCurve();
    private Vector3 startPos = Vector3.zero;
    private bool _hasReached =true;

    [SerializeField] float _stoppingDistance = 0.5f;

    private float timeLeft = 2.0f;
    private float duration = 3f;
    private float time;
    
    IEnumerator Start()
    {
        
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        agent.autoTraverseOffMeshLink = false;
        startPos = Vector3.zero;

        while (true)
        {
            if ((!agent.isPathStale) && (!agent.pathPending) && (agent.pathStatus == UnityEngine.AI.NavMeshPathStatus.PathComplete))
            {
                m_Method = (attacks[Random.Range(0, attacks.Length)]);


                if (old_Method != m_Method)
                {

                    if (m_Method == OffMeshLinkMoveMethod.Rollback)
                        yield return StartCoroutine(Rollback(agent, startPos));
                    if (m_Method == OffMeshLinkMoveMethod.Swag)
                        yield return StartCoroutine(Swag(agent));
                    else if (m_Method == OffMeshLinkMoveMethod.Parabola)
                        yield return StartCoroutine(Parabola(agent, 2.0f, 0.5f));
                    else if (m_Method == OffMeshLinkMoveMethod.Orbit)
                        yield return StartCoroutine(Rotate(agent, 0.5f));
                    //agent.speed = 30f;
                    //agent.CompleteOffMeshLink();
                }
                old_Method = m_Method;
            }
         
            yield return null;
        }
        
    }

    IEnumerator NormalSpeed(NavMeshAgent agent)
    {
        Vector3 endPos = agent.pathEndPosition; // + Vector3.up * agent.baseOffset;
        while (agent.transform.position != endPos)
        {
            agent.transform.position = Vector3.MoveTowards(agent.transform.position, endPos, agent.speed * Time.deltaTime);
            yield return null;
        }
        Debug.Log("Normal");

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

        agent.transform.position = attackPosition;

        float percent = 0;
        while (percent <= 3)
        {
            percent += Time.deltaTime * 5f;

            float interpolation = (-Mathf.Pow(percent, 2) + percent) * 5;
           agent.transform.position = Vector3.Lerp(startPos, attackPosition, interpolation);
        

            yield return null;

        }

        agent.transform.position = Vector3.MoveTowards(agent.transform.position, newPosition, Time.deltaTime * 0.1f);

       // yield return null;

        Debug.Log("swag");
        //Debug.Log("Ability Swag enables coroutineTrigger to run.");
        yield break;
    }

    IEnumerator Parabola2(NavMeshAgent agent, float height, float duration)
    {
        startPos = agent.transform.position;

        Vector3 dirToTarget = (agent.pathEndPosition - agent.transform.position).normalized;
        Vector3 attackPosition = agent.pathEndPosition - dirToTarget * (1f);
        Vector3 newPosition = agent.pathEndPosition - dirToTarget * (1f);

        agent.transform.position = attackPosition;


       
        Vector3 endPos = agent.pathEndPosition + Vector3.up;// * agent.baseOffset;

        float normalizedTime = 0.0f;
        while (normalizedTime < 1.0f)
        {
            float yOffset = 1f * 0.1f * (normalizedTime - normalizedTime * normalizedTime);
            agent.transform.position = Vector3.Lerp(startPos, endPos, normalizedTime) + yOffset * Vector3.up;
            normalizedTime += Time.deltaTime / duration;
            yield return null;

        }


        agent.transform.position = Vector3.MoveTowards(agent.transform.position, newPosition, Time.deltaTime * 0.1f);

        Debug.Log("Parabola");

        yield break;
    }


    IEnumerator Parabola(NavMeshAgent agent, float height, float duration)
    {

       startPos = agent.transform.position;
        Vector3 endPos = agent.pathEndPosition + Vector3.up * agent.baseOffset;

        float normalizedTime = 0.0f;
        while (normalizedTime < 1.0f)
        {
            float yOffset = 1f * 0.1f * (normalizedTime - normalizedTime * normalizedTime);
            agent.transform.position = Vector3.Lerp(startPos, endPos, normalizedTime) + yOffset * Vector3.up;
            normalizedTime += Time.deltaTime / duration;
            yield return null;

        }
    

        yield break;
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
        Debug.Log("RollBack");
        yield break;

    }




    IEnumerator Rotate(NavMeshAgent agent, float duration)
    {
     
        Vector3 startPos = agent.transform.position;
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
    public void GetRandom()
    {
        //m_Method = (attacks[Random.Range(0, Mathf.RoundToInt(attacks.Length))]);
        //_hasReached = false;
        //Debug.Log("m_Method =" + m_Method);

    }

   IEnumerator Rotate(NavMeshAgent agent)
    {
        //   Debug.Log("Coroutine Orbit Created.");

        bool completed_OneCycle =false;
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
            if (completed_OneCycle ==true)
            { agent.transform.RotateAround(agent.pathEndPosition, axis, -rotationSpeed * Time.deltaTime); }
            else
            { agent.transform.RotateAround(agent.pathEndPosition, axis, rotationSpeed * Time.deltaTime); }


            agent.transform.position = Vector3.MoveTowards(agent.transform.position, attackPosition, Time.deltaTime * radiusSpeed);
         //   agent.transform.LookAt();



        }

        yield return null;

        //   Debug.Log("Ability Orbit enables coroutineTrigger to run.");
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
