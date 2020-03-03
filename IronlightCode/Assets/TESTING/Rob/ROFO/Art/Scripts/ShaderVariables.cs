using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShaderVariables : MonoBehaviour
{
    private Renderer rend;
    private bool on = false;
    [SerializeField] private float count = 0.0f;
    public float changeTime = 1f;

    private Coroutine currentCoroutine = null;



    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();        
    }

    // Update is called once per frame
    void Update()
    {
        //check for input and only run if not already running
        if (Input.GetKeyUp(KeyCode.P) && on == false)
        {            
            Debug.Log(rend.material.GetFloat("_Switch"));
            currentCoroutine = StartCoroutine(Change());   
        }
    }

    IEnumerator Change()
    {
        //flip state
        on = true;

        //move up to change time
        while(count < changeTime)
        {
            count += Time.deltaTime;
            rend.material.SetFloat("_Switch", count/changeTime);
            yield return null;
        }

        //move back down to 0
        while(count > 0f)
        {
            count -= Time.deltaTime;
            rend.material.SetFloat("_Switch", count/changeTime);
            yield return null;
        }

        //reset 
        count = 0.0f;
        on = false;        
    }
}
