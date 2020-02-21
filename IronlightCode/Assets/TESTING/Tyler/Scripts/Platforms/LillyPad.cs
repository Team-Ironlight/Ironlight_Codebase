using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LillyPad : MonoBehaviour
{
    public linecastdown lcd;
    bool isRising = false;
    bool isFalling = false;
    public float isDownSpeed = 0;
    public float fallDelay = 4.0f;
    public float Rise = 0f;
    public float isRiseSpeed = 0;
    private Vector3 StartPosition;



    private void Start()
    {
        StartPosition = transform.position;
    }





    private void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Player")
        {
            StartCoroutine(FallAfterDelay());
            
            Debug.Log("HERE");
        }
    }


    private void OnTriggerExit(Collider collider)
    {
        if(collider.tag == "Player")
        {
            StartCoroutine(RiseDelay());
        }
    }


    public void falllillypad()
    {
        StartCoroutine(FallAfterDelay());
        print("fall");
    }

    public void riselillypad()
    {
        StartCoroutine(RiseDelay());
        print("rise");
    }

    void Update()
    {
        if (isFalling)
        {
            isDownSpeed += Time.deltaTime / 15;
            transform.position += Vector3.down * Time.deltaTime *isDownSpeed;

        }
        
        if (StartPosition.y <= transform.position.y)
        {
            isRising = false;
        }


        if (isRising)
        {
            isRiseSpeed += Time.deltaTime / 15;
            transform.position += Vector3.up * Time.deltaTime * isRiseSpeed;
        }
       

    }

    private void LateUpdate()
    {
        if (!lcd.LillyPad)
       {
    //      StartCoroutine(FallAfterDelay());

         // Debug.Log("HERE");
      // }
       //else
       //{
           StartCoroutine(RiseDelay());
       }

    }

    IEnumerator FallAfterDelay()
    {
        Debug.Log("HUMP");
        yield return new WaitForSeconds(fallDelay);
        isFalling = true;
        Debug.Log("HI");
    }

    IEnumerator RiseDelay()
    {
        yield return new WaitForSeconds(Rise);
        isFalling = false;
        isRising = true;
        print("rising");
    }


}
