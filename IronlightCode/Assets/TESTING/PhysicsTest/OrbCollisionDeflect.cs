using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbCollisionDeflect : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody>().velocity = transform.forward * 15;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        collision.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.Reflect(this.GetComponent<Rigidbody>().position, this.GetComponent<Rigidbody>().position) *15, ForceMode.Impulse);
    }
}
