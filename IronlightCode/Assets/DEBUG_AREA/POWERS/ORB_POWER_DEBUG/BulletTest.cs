using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletTest : MonoBehaviour
{
    //Linecast
    private RaycastHit currentWallHit;
    public Vector3 Direction;

    // Start is called before the first frame update
    void Start()
    {
        //deactivate after 5 seconds
        StartCoroutine(BulletDisable(gameObject));
    }

    // Update is called once per frame
    void Update()
    {
        ObsticleCheck();
    }

    private void ObsticleCheck()
    {

        Debug.DrawLine(transform.position, transform.position + Direction);

        if (Physics.Linecast(transform.position, transform.position + Direction * 2.0f, out currentWallHit))
        {
            if (currentWallHit.transform.gameObject.tag == ("Wall"))
            {
                gameObject.SetActive(false);        

                //set velocity to 0
                gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;

                //stop 
                StopCoroutine("BulletDisable");
            }

            if (currentWallHit.transform.gameObject.tag == ("Enemy"))
            {
                gameObject.SetActive(false);

                //set velocity to 0
                gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;

                //stop 
                StopCoroutine("BulletDisable");

                //push back enemy
                currentWallHit.transform.GetComponent<Rigidbody>().AddForce(Direction.normalized,ForceMode.Impulse);
            }
        }
    }
    IEnumerator BulletDisable(GameObject bullet)
    {
        // suspend execution for 5 seconds
        yield return new WaitForSeconds(5);
        bullet.SetActive(false);

        //set velocity to 0
        bullet.GetComponent<Rigidbody>().velocity = Vector3.zero;
    }
}
