using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Straffing : MonoBehaviour
{
    int decision;
    public Transform player;
    public Animator anim;
    float decisionTime;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Works");
    }

    // Update is called once per frame
    void Update()
    {
        float distanceToPlayer = Vector3.Distance(player.position, this.transform.position);
        transform.LookAt(player.position);

        if (distanceToPlayer < 5 && distanceToPlayer > 3 && decisionTime <= 0)
        {
            this.transform.position += this.transform.forward * 0.1f;
            /*decision = Random.Range(0, 4);

            if (decision == 1 || decision == 2 || decision == 3)
            {
                decisionTime = 5;
            }*/
            decision = 1;
            decisionTime = 5;
        }

        else
        {
            decisionTime -= Time.deltaTime;
        }
        Debug.Log(decisionTime);

        if (decision == 1)
        {
            anim.SetBool("TargetLongRange", true);
            for (int i = 0; i < 4; ++i)
            {
                this.transform.position += this.transform.forward * -0.005f;
            }
        }

        else if (decision == 2)
        {
            transform.Translate(Vector3.left * .005f);
        }

        else if (decision == 3)
        {
            transform.Translate(Vector3.right * .005f);
        }
    }
}
