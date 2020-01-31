using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PLY_2ndOrbAttack : MonoBehaviour
{
    public bool inputReceived = false;

    //object pool
    [SerializeField] private GameObject Magezine;
    [SerializeField] private GameObject Muzzle;

    //bool check
    public bool Orbshoot;

    List<GameObject> bulletPool = new List<GameObject>();
    [SerializeField] private int MagezineSize = 10;

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
        AttackTimer = Time.time;
        OrbScale = MinOrbScale;
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
                    Shoot(transform.forward , OrbScale);
                    print(OrbScale);
                }
                OrbScale = MinOrbScale;
            }
        }

    }

    IEnumerator OrbCharge()
    {
        while(OrbScale < MaxOrbScale)
        {
            OrbScale += OrbScaleSpeed * Time.deltaTime;
            yield return null;
        }
    }

    // Code to perform attack
    public void Shoot(Vector3 pDir, float orbScale)
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
            clone.transform.localScale = new Vector3(orbScale, orbScale, orbScale);

            Vector3 shootDirection = pDir;
            //add spread in x-axis
            shootDirection.x += Random.Range(-spreadFactor, spreadFactor);
            //add spread in y-axis
            shootDirection.y += Random.Range(-_yMaxSpread, spreadFactor);
            //set the position of the bullet to the muzzle
            clone.transform.position = Muzzle.transform.position;
            //call shoot function on the bullet
            clone.GetComponent<PLY_2ndBulletOrb>().StartOrb(shootDirection);
        }
    }
}
