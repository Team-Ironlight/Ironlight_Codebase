using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPlaceHolder : MonoBehaviour
{
    public bool hitLeaf = false;
    RaycastHit hit;
    public LayerMask layerMask;
    // Start is called before the first frame update
    void Start()
    {

       layerMask= (1 << LayerMask.NameToLayer("Leaf"));
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(gameObject.transform.position, Vector3.forward * 40, Color.yellow);
        if (Physics.Raycast(gameObject.transform.position, Vector3.forward, out hit,40f, layerMask))
        {
            hit.transform.SendMessage("RayHit");
            hitLeaf = true;
            print("Hit");
        }
        else
        {

            hitLeaf = false;
        }
    }
}
