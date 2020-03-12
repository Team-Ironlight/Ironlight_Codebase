using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContactVertexDisplacementRing : MonoBehaviour
{
    private Renderer rend;
    public bool on = false;
    public float speed = 1f;
    public float activeTime = 1f;
    public float emissionFadeTime = 0f;
    public float normalExtrude = 3f;
    public float normalFadeTime = 1f;
    //public float bodyRadius = 1f;

    private Coroutine cc = null;

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
        rend.material.SetFloat("_Length", -1f);
        rend.material.SetFloat("_Fade", -1f);
        rend.material.SetFloat("_ExtrudeAmount", normalExtrude * 0.01f);
    }

    // Update is called once per frame
    void Update()
    {
        ////debug
        //if(Input.GetKeyUp(KeyCode.P) && on == false)
        //{
        //    cc = StartCoroutine(Work());
        //}
    }


    IEnumerator Work()
    {
        on = true;
        float count = 0.1f;
        rend.material.SetFloat("_Fade", 1f);
        rend.material.SetFloat("_NormalExtrude", normalExtrude * 0.01f);

        while (count < activeTime)
        {
            count += Time.deltaTime;
            rend.material.SetFloat("_Length", count * speed);

            if(count > emissionFadeTime)
            {
                //start fading
                rend.material.SetFloat("_Fade", (activeTime - count)/(activeTime - emissionFadeTime));
            }

            if(count > normalFadeTime)
            {                
                float value = ((activeTime - count) / (activeTime - normalFadeTime)) * normalExtrude * 0.01f;
                rend.material.SetFloat("_ExtrudeAmount", value);
            }

            yield return null;
        }

        rend.material.SetFloat("_Length", -1f);
        rend.material.SetFloat("_Fade", -1f);
        on = false;
    }

    private void RunShader(Collision collision)
    {
        Debug.Log("Collision with Player");

        //set collision location in shader
        Vector3 temp = new Vector3(collision.transform.position.x,
                                   transform.position.y,
                                   collision.transform.position.z);
        rend.material.SetVector("_ContactLocation", temp);

        //start shader
        cc = StartCoroutine(Work());
    }


    private void OnCollisionEnter(Collision collision)
    {
        //if player collides with object from above
        if(collision.transform.position.y > this.transform.position.y &&
           collision.gameObject.tag == "Player" &&
           on == false)
        {
            //rend.material.SetFloat("_BodyRadius", bodyRadius);
            RunShader(collision);
        }
    }
}
