using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyTrap : MonoBehaviour
{
    public GameObject rightLeaf;
    public GameObject leftLeaf;
    bool warning = false;
    bool reset = false;
    float RightleafRot;
    float LeftLeafRot;
    public float trapSpeed= 20;
    public float multiplyer = 10;
    public GameObject DeathColl;
    public float KillAngle;

    // Start is called before the first frame update
    void Start()
    {
        DeathColl.SetActive(false);
       // warning = true;
    }
    // Update is called once per frame
    void Update()
    {
        if (warning)
        {
            if (leftLeaf.transform.eulerAngles.x < 80)
            {
                TrapWarn();
                TrapClose();
            }
            else
            {
                warning = false;
                reset = true;
            }
        }

        if (reset == true)
        {
            trapReturn();
            if (leftLeaf.transform.eulerAngles.x < 2)
            {
                reset = false;
                
            }
        }

    }
    void TrapWarn()
    {
        if (leftLeaf.transform.eulerAngles.x <= 15)
        {
            print("Warning");
            //RightleafRot -= 5 * Time.deltaTime;
            //LeftLeafRot += 5 * Time.deltaTime;
            rightLeaf.transform.Rotate(Vector3.right * Time.deltaTime * -trapSpeed);
            leftLeaf.transform.Rotate(Vector3.right * Time.deltaTime* trapSpeed);
        }

    }
    void TrapClose()
    {
         if(leftLeaf.transform.eulerAngles.x > 10)
        {

            rightLeaf.transform.Rotate(Vector3.right * Time.deltaTime * -trapSpeed * multiplyer);
            leftLeaf.transform.Rotate(Vector3.right * Time.deltaTime*trapSpeed *multiplyer);
            if (leftLeaf.transform.eulerAngles.x > KillAngle)
            {
                DeathColl.SetActive(true);
            }
        }
    }
    void trapReturn()
    {
        DeathColl.SetActive(false);
        rightLeaf.transform.Rotate(Vector3.right * Time.deltaTime * trapSpeed);
        leftLeaf.transform.Rotate(Vector3.right * Time.deltaTime * -trapSpeed);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            warning = true;
        }
    }
}
