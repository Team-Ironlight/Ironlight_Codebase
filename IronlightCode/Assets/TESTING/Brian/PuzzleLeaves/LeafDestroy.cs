using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeafDestroy : MonoBehaviour
{
    public AttackPlaceHolder hit;
    Rigidbody rb;
    [SerializeField]
    float currCount;
    public float StartCount;
    public float deathCount;
    bool deadLeaf;
    bool hitLeaf = false;
    // Start is called before the first frame update
    void Start()
    {
        deathCount = 4;
        StartCount = 5;
        currCount = StartCount;
        rb = GetComponent<Rigidbody>();

        //hit = GetComponent<AttackPlaceHolder>();
    }

    // Update is called once per frame
    void Update()
    {
        if (hitLeaf == true)
        {
            currCount -= Time.deltaTime;
            if (currCount <= 0)
            {
                print("Leaf should fall");
                rb.isKinematic = false;
                deadLeaf = true;
            }
        }
        if (deadLeaf)
        {
            deathCount -= Time.deltaTime;
            if (deathCount <=0)
            {
                Destroy(gameObject);
            }
        }
        if (hit.hitLeaf)
        {
            RayOff();
        }
    }
    void RayHit()
    {
        hitLeaf = true;
    }
    void RayOff()
    {
        hitLeaf = false;
    }
}
