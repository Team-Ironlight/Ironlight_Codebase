using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrossHairUI : MonoBehaviour
{
    public Transform player;
    public Image crossHair;
    public bool isOn = false;

    private RaycastHit hit;
    private Vector3 worldPoint;
    private Vector3 screenPoint;

    private void Update()
    {
        //input
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            Toggle(true);
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            Toggle(false);
        }        

        if(isOn)
        {
            //PlayerCrosshair();

            crossHair.rectTransform.position = Camera.main.WorldToScreenPoint(Camera.main.transform.position + Camera.main.transform.forward);

        }
    }

    private void PlayerCrosshair()
    {
        //raycast from players position to get point
        if (Physics.Raycast(player.position + player.forward, player.forward, out hit, 10f))
        {
            worldPoint = hit.point;
        }
        else
        {
            worldPoint = player.position + (player.forward * 10f);
        }

        //find that point in screen space...
        screenPoint = Camera.main.WorldToScreenPoint(worldPoint);
        Debug.Log("ScreenPoint: " + screenPoint);

        //set crosshair to screenPoint
        crossHair.rectTransform.position = screenPoint;
        Debug.Log("Crosshair Position: " + crossHair.rectTransform.position);
    }

    //basic switcing
    private void Toggle(bool b)
    {
        isOn = b;
        if (isOn)
        {
            crossHair.gameObject.SetActive(true);
        }
        else
        {
            crossHair.gameObject.SetActive(false);
        }
    }
}
