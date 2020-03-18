using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LillyPad : MonoBehaviour
{
   //  public linecastdown lcd;
    public bool isRising = false;
    public bool isFalling = false;
    public float isDownSpeed = 0.1f;
    public float fallDelay = 4.0f;
    public float riseDelay = 0f;
    public float isRiseSpeed = 0.1f;
    public Vector3 StartPosition;

    Coroutine riseRoutine = null;
    Coroutine fallRoutine = null;

    Coroutine currentCo = null;

    public float fallAmount = 0;
    public float maxFallAmount = 2;

    public float riseAmount = 0;
    public float maxRiseAmount = 5;

    public int count = 0;

    private void Start()
    {
        // StartPosition = transform.position;
        StartPosition = Vector3.zero;
    }


    //private void OnTriggerStay(Collision collision)
   
    //{
    //    if (collision.gameObject.tag == "Player")
    //    {
    //        count++;
    //        Debug.Log(count);
    //        if(fallRoutine == null)
    //        {
    //            riseRoutine = null;
    //            fallRoutine = StartCoroutine(FallAfterDelay());
    //        }
            
            
    //        Debug.Log("On the lilipadx");
    //    }
    //}
    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            if(currentCo == null)
            {
                currentCo = StartCoroutine(FallAfterDelay());
            }


            Debug.Log("On the lilipadx");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            if (currentCo != null) 
            {
                currentCo = null;
                currentCo = StartCoroutine(RiseAfterDelay());

            }
        }
    }


    //private void OnCollisionExit(Collision collision)
    //{
    //    if(collision.gameObject.tag == "Player")
    //    {
    //        if(riseRoutine == null)
    //        {
    //            fallRoutine = null;
    //            riseRoutine = StartCoroutine(RiseAfterDelay());

    //        }
    //    }
    //}


    //public void falllillypad()
    //{
    //    StartCoroutine(FallAfterDelay());
    //    print("fall");
    //}

    //public void riselillypad()
    //{
    //    StartCoroutine(RiseAfterDelay());
    //    print("rise");
    //}

    void Update()
    {
        //if (isFalling)
        //{
        //    if(riseRoutine != null)
        //    {
        //        StopCoroutine(riseRoutine);
        //    }

        //    if (fallRoutine == null)
        //    {
        //        StopCoroutine(fallRoutine);
        //        fallRoutine = null;
        //        fallRoutine = StartCoroutine(PerformFall());
        //    }

        //    //StopCoroutine(RiseAfterDelay());
        //    //isDownSpeed += Time.deltaTime / 15;
        //    //transform.position += Vector3.down * Time.deltaTime *isDownSpeed;
        //    //print("Lillypad falling");

        //}

        ////if(transform.position.y <= maxFallHeight)
        ////{
        ////    StopCoroutine(fallRoutine);
        ////    isFalling = false;
        ////}
        
        ////if (StartPosition.y <= transform.position.y)
        ////{
        ////    if(riseRoutine != null)
        ////    {
        ////        StopCoroutine(riseRoutine);
        ////        riseRoutine = null;
        ////    }
        ////    isRising = false;
        ////}


        //if (isRising)
        //{
        //    isFalling = false;
        //    if(fallRoutine != null)
        //    {
        //        StopCoroutine(fallRoutine);
        //    }

        //    if(riseRoutine == null)
        //    {
        //        riseRoutine = StartCoroutine(PerformRise());
        //    }

        //    //StopCoroutine(FallAfterDelay());
        //    //isRiseSpeed += Time.deltaTime / 15;
        //    //transform.position += Vector3.up * Time.deltaTime * isRiseSpeed;
        //    //print("Lillypad rising");
        //}
       

    }

    //private void LateUpdate()
    //{
    //    if (!lcd.LillyPad)
    //   {
    ////      StartCoroutine(FallAfterDelay());

    //     // Debug.Log("HERE");
    //  // }
    //   //else
    //   //{
    //       StartCoroutine(RiseDelay());
    //   }

  //  }

    //IEnumerator PerformFall()
    //{
    //    while (fallAmount > maxFallAmount)
    //    {
    //        isDownSpeed += Time.deltaTime / 15;
    //        fallAmount += isDownSpeed;
    //        transform.position += Vector3.down * Time.deltaTime * fallAmount;
    //        print("Lillypad falling" + transform.position.y);
    //        yield return null;
    //    }

    //    isFalling = false;
    //    //StopCoroutine(fallRoutine);
    //    //fallRoutine = null;
    //    Debug.Log("Bitches");
    //}

    //IEnumerator PerformRise()
    //{
    //    while(transform.position.y < StartPosition.y)
    //    {
    //        isRiseSpeed += Time.deltaTime / 15;
    //        transform.position += Vector3.up * Time.deltaTime * isRiseSpeed;
    //        print("Lillypad rising");
    //        yield return null;
    //    }

    //    isRising = false;
    //    StopCoroutine(riseRoutine);
    //    riseRoutine = null;
    //}

    IEnumerator FallAfterDelay()
    {
        //if(riseRoutine != null)
        //{
        //    StopCoroutine(riseRoutine);
        //    riseRoutine = null;
        //}

        Debug.Log("HUMP");
        yield return new WaitForSeconds(fallDelay);
        isFalling = true;
        isRising = false;

        while(fallAmount < maxFallAmount)
        {
            //isDownSpeed += Time.deltaTime / 15;
            fallAmount += isDownSpeed * Time.deltaTime;
            transform.position += Vector3.down * Time.deltaTime * isDownSpeed;
            print("Lillypad falling" + transform.position.y);
            yield return null;
        }

        isFalling = false;
        //riseRoutine = StartCoroutine(RiseAfterDelay());

        StopCoroutine(currentCo);
        currentCo = null;
        currentCo = StartCoroutine(RiseAfterDelay());
        
    }

    IEnumerator RiseAfterDelay()
    {
        //if(fallRoutine != null)
        //{
        //    StopCoroutine(fallRoutine);
        //    fallRoutine = null;
        //}


        yield return new WaitForSeconds(riseDelay);
        isFalling = false;
        isRising = true;

        while (fallAmount > 0 && !(fallAmount < 0))
        {
            //isRiseSpeed += Time.deltaTime / 15;
            fallAmount -= isRiseSpeed * Time.deltaTime;
            transform.position += Vector3.up * Time.deltaTime * isRiseSpeed;
            print("Lillypad rising");
            yield return null;
        }

        isRising = false;

        StopCoroutine(currentCo);
        currentCo = null;
    }


}
