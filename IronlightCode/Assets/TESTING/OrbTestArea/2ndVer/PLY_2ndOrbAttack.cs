using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PLY_2ndOrbAttack : MonoBehaviour
{
    public bool inputReceived = false;

    //object pool
    public GameObject _pool;
    List<GameObject> bulletPool = new List<GameObject>();
    [SerializeField] private int MagezineSize = 10;

    //attack
    public GameObject GB_Bullet;
    public Camera Cam;
    [SerializeField] private float AttackCoolDown = 0.5f;
    private float AttackTimer;
    [SerializeField] private float spreadFactor;
    [SerializeField] private float _yMaxSpread;


    // Initilization - Instantiate a set number of bullets

    private void Start()
    {
        for (int i = 0; i <= MagezineSize; i++)
        {
            //instantiate the bullet
            GameObject GB_Clone = Instantiate(GB_Bullet, transform.position, transform.rotation);
            //deactivate object
            GB_Clone.SetActive(false);
            //child to the pool
            GB_Clone.transform.parent = _pool.transform;
            //add to the object pool
            bulletPool.Add(GB_Clone);
        }
        AttackTimer = Time.time;
    }

    private void Update()
    {
        GetInput();
    }

    void GetInput()
    {
        // Change this depending on how you want the attack to work
        if (Input.GetKeyDown(KeyCode.Space))
        {
            inputReceived = true;
            if (AttackTimer <= Time.time)
            {
                // TODO Change Inputed Parameter to not just be camera forward :D
                Shoot(Cam.transform.forward);
                AttackTimer = Time.time + AttackCoolDown;
            }

        }
        else
        {
            inputReceived = false;
        }
    }

    // Code to perform attack
    public void Shoot(Vector3 pDir)
    {
        GameObject clone = null;
        //loop to find the first deactive bullet in the pool

        for (int i = 0; i < bulletPool.Count; i++)
        {
            if (bulletPool[i].activeSelf == false)
            {
                clone = bulletPool[i];
                break;
            }
        }
        if (clone != null)
        {
            Vector3 shootDirection = pDir;
            //add spread in x-axis
            shootDirection.x += Random.Range(-spreadFactor, spreadFactor);
            //add spread in y-axis
            shootDirection.y += Random.Range(-_yMaxSpread, spreadFactor);
            //set the position of the bullet to the muzzle
            clone.transform.position = _pool.transform.position;
            //call shoot function on the bullet
            clone.GetComponent<PLY_2ndBulletOrb>().StartOrb(shootDirection);
        }
    }
}
