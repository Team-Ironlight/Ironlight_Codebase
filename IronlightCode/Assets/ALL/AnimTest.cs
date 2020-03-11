using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//test input for animators
public class AnimTest : MonoBehaviour
{
    private Animator anim;
    private bool isOn = false;
    public string testBool;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.P))
        {
            isOn = !isOn;

            anim.SetBool(testBool, isOn);

            Debug.Log("Anim Go");
        }
    }
}
