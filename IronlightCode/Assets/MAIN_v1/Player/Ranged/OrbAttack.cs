using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbAttack : MonoBehaviour
{
    private int I_speed;
    public GameObject GB_Bullet;
    private float F_chargeTime;
    public Camera Cam;
    private float F_x = Screen.width / 2;
    private float F_y = Screen.height / 2;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Shoot()
    {
        //instantiate the bullet
        GameObject GB_Clone = Instantiate(GB_Bullet, transform.position, transform.rotation);

        //calculate the direction
        var ray = Cam.ScreenPointToRay(new Vector3(F_x, F_y, 0));

        //add force to bullets rigidbody in the right direction
        GB_Clone.GetComponent<Rigidbody>().velocity = ray.direction * I_speed;
    }

}
