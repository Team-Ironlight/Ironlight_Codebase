using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PLY_ImanBlastTest : MonoBehaviour
{
    public bool inputReceived = false;
    public float radius;
    public float radiusMax = 1f;
    public float radiusSpeed = 1f;
    public float BlastSpeedMultiplyer = 1f;


    public bool isPressed = false;

    public float timer = 5.0f;
    private float countdown = 0.01f;

    bool coroutineOn = false;
    float chargeCount = 0f;
    [SerializeField] private int _iExplosionDmg;

    [SerializeField] private GameObject ChargeVisual;

    public List<GameObject> enemiesDamaged = new List<GameObject>();

    [SerializeField] private LayerMask enemiesLayer;

    // Update is called once per frame
    void Update()
    {
        Charge();
    }

    IEnumerator RadialAction(float x)
    {
        Debug.Log("Charge");
        
        float count = 0.0f;
        bool damaged = false;
        GameObject blast = Instantiate(ChargeVisual, transform.position, transform.rotation);
        while (count < x)
        {
            
            radius = count;
            blast.transform.localScale = new Vector3(radius, radius, radius) * 2;
            //get all colliders in the sphere
            foreach (Collider pcollider in Physics.OverlapSphere(transform.position, radius, enemiesLayer))
            {
                Debug.Log("Entered collision check");
                IAttributes cIA = pcollider.gameObject.GetComponent<IAttributes>();
                //check if gameobject is damagable
                if (cIA != null)
                {
                    for(int i = 0; i < enemiesDamaged.Count;i++)
                    {
                        if(enemiesDamaged[i].gameObject == pcollider.gameObject)
                        {
                            damaged = true;
                            Debug.Log("Found enemy");
                            break;
                        }
                    }
                    if (!damaged)
                    {
                        //cIA.TakeDamage(_iExplosionDmg, false);
                        print("Damaged enemy");
                        enemiesDamaged.Add(pcollider.gameObject);
                    }
                }
                damaged = false;
            }
            count += Time.deltaTime * BlastSpeedMultiplyer;

            yield return null;
        }
        //radius = 0.01f;
        coroutineOn = false;
        chargeCount = 0;
        Destroy(blast);
        enemiesDamaged.Clear();
    }

    private void Charge()
    {
        if (Input.GetKey(KeyCode.Space) && !coroutineOn)
        {
            if (chargeCount < radiusMax)
            {
                chargeCount += radiusSpeed * Time.deltaTime;
            }
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            StartCoroutine(RadialAction(chargeCount));
            coroutineOn = true;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
