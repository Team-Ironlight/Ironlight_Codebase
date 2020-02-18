using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossHair : MonoBehaviour
{
    public GameObject crossHair;
    public bool isOn = false;

    private SpriteRenderer rend;
    private Sprite sprite;

    private void Start()
    {
        rend = crossHair.GetComponent<SpriteRenderer>();
        sprite = rend.sprite;
    }

    private void Update()
    {
        //input
        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            Toggle(true);
        }
        else if(Input.GetKeyUp(KeyCode.LeftShift))
        {
            Toggle(false);
        }


        //when active move position
        if(isOn)
        {
            Vector2 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(cursorPos.x, cursorPos.y, 0f);
        }
    }

    //basic switcing
    private void Toggle(bool b)
    {
        isOn = b;
        if(isOn)
        {
            crossHair.SetActive(true);            
        }
        else
        {
            crossHair.SetActive(false);
        }
    }
}
