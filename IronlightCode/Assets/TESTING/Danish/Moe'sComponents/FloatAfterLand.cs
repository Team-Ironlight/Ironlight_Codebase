using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatAfterLand : MonoBehaviour
{
    public FloatingBody FB;
    public LineCastFloating LCF;

    public float delayTime = 1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LateUpdate()
    {
        if(!LCF.LCF)
        {
            Debug.Log("NotFloat");
            
        }
        else if(LCF.LCF)
        {
            StartCoroutine(delayFloat());
            Debug.Log("Float");
            FB.floaty();
        }
    }

    IEnumerator delayFloat()
    {
        yield return new WaitForSeconds(delayTime);
        
    }
}
