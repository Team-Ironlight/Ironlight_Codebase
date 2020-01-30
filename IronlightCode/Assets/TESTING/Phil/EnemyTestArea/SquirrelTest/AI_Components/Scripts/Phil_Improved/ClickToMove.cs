// ----------------------------------------------------------------------------
// Capstone 2020 - IronLight
// 
// Programmer: Phil James
// Date:   01/29/2020       Version 1
// ----------------------------------------------------------------------------

using UnityEngine;
using UnityEngine.AI;

// Use physics raycast hit from mouse click to set agent destination
[RequireComponent(typeof(NavMeshAgent))]
public class ClickToMove : MonoBehaviour
{
    NavMeshAgent m_Agent;
    RaycastHit m_HitInfo = new RaycastHit();

    private AgentLinkMover Mover;

    public int minDmg = 1;
    public int maxDmg = 5;

    void Start()
    {
        m_Agent = GetComponent<NavMeshAgent>();

        Mover = GetComponent<AgentLinkMover>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !Input.GetKey(KeyCode.LeftShift))
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray.origin, ray.direction, out m_HitInfo))
                Mover.GetRandom();
                m_Agent.destination = m_HitInfo.point;
        }

     
    }
}
