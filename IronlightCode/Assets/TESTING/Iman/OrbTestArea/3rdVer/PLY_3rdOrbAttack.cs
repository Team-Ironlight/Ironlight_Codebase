using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PLY_3rdOrbAttack : MonoBehaviour
{
    public bool inputReceived = false;

    //object pool
    [SerializeField] private GameObject Magezine;
    [SerializeField] private GameObject Muzzle;
    List<GameObject> bulletPool = new List<GameObject>();
    [SerializeField] private int MagezineSize = 10;
    private GameObject CurrentOrb;

    //bool check
    public bool Orbshoot;


    //attack
    public GameObject GB_Bullet;
    [SerializeField] private float AttackCoolDown = 0.5f;
    private float AttackTimer;
    [SerializeField] private float spreadFactor;
    [SerializeField] private float _yMaxSpread;

    //orbSize
    [SerializeField] private float MaxOrbScale;
    [SerializeField] private float MinOrbScale;
    [SerializeField] private float OrbScaleSpeed;
    private float OrbScale;

    // Initilization - Instantiate a set number of bullets

    private void Start()
    {
        //creat the magezine 
        for (int i = 0; i <= MagezineSize; i++)
        {
            //instantiate the bullet
            GameObject GB_Clone = Instantiate(GB_Bullet, Muzzle.transform.position, Muzzle.transform.rotation);
            //deactivate object
            GB_Clone.SetActive(false);
            //child to the pool
            GB_Clone.transform.parent = Magezine.transform;
            //add to the object pool
            bulletPool.Add(GB_Clone);
        }
        //initialize the variables
        AttackTimer = Time.time;
        OrbScale = MinOrbScale;
        CurrentOrb = null;
    }

    private void Update()
    {
        GetInput();
    }

    void GetInput()
    {
        // Change this depending on how you want the attack to work
        if (Input.GetKeyDown(KeyCode.E) || Input.GetMouseButtonDown(0))
        {
            inputReceived = true;
            if (AttackTimer <= Time.time)
            {
                GetBullet();
                print(CurrentOrb.GetComponent<Rigidbody>().velocity);
                StartCoroutine("OrbCharge");
            }
        }
        else
        {
            Orbshoot = false;
            inputReceived = false;
        }

        if (Input.GetKeyUp(KeyCode.E) || Input.GetMouseButtonUp(0))
        {
            if (AttackTimer <= Time.time)
            {
                StopCoroutine("OrbCharge");
                // TODO Change Inputed Parameter to not just be camera forward :D
                AttackTimer = Time.time + AttackCoolDown;
                Orbshoot = true;
                if (Orbshoot)
                {
                    Shoot(transform.forward, CurrentOrb);
                }
                OrbScale = MinOrbScale;
                CurrentOrb = null;
            }
        }
    }

    IEnumerator OrbCharge()
    {
        //if or size is smaller than max size
        while (OrbScale < MaxOrbScale)
        {
            //add to size
            OrbScale += OrbScaleSpeed * Time.deltaTime;
            //visualize it
            CurrentOrb.transform.localScale = new Vector3(OrbScale, OrbScale, OrbScale);
            yield return null;
        }
    }

    // Code to perform attack
    public void Shoot(Vector3 pDir, GameObject clone)
    {
        if (clone != null)
        {
            Vector3 shootDirection = pDir;
            //add spread in x-axis
            shootDirection.x += Random.Range(-spreadFactor, spreadFactor);
            //add spread in y-axis
            shootDirection.y += Random.Range(-_yMaxSpread, spreadFactor);
            //call shoot function on the bullet
            clone.GetComponent<PLY_2ndBulletOrb>().StartOrb(shootDirection);
        }
    }

    //function to get the first inactive orb
    private void GetBullet()
    {
        //loop to find the first deactive bullet in the pool
        for (int i = 0; i < bulletPool.Count; i++)
        {
            if (bulletPool[i].activeSelf == false)
            {
                //set current orb to first inactive bullet
                CurrentOrb = bulletPool[i];
                //set the position of the bullet to the muzzle
                CurrentOrb.transform.position = Muzzle.transform.position;
                //set gameobject to active
                CurrentOrb.SetActive(true);
                break;
            }
        }

    }
}
