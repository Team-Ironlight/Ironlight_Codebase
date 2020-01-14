﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MOD_blast : MonoBehaviour
{
    //blast
    public bool inputReceived = false;
    private float radius;
    [SerializeField] private float radiusMax = 1f;
    [SerializeField] private float radiusChargeSpeed = 1f;
    [SerializeField] private float BlastSpeedMultiplyer = 1f;
    [SerializeField] private float PushBackForce;
    [SerializeField] private int bigDmg;
    [SerializeField] private int medDmg;
    [SerializeField] private int smallDmg;
    [SerializeField] private LayerMask enemiesLayer;

    private bool coroutineOn = false;
    private float chargeCount = 0f;
    [SerializeField] private GameObject ChargeVisual;
    private List<GameObject> enemiesDamaged = new List<GameObject>();

    // Update is called once per frame
    void Update()
    {
        Charge();
    }


    //coroutine for doing the blast attack
    IEnumerator RadialAction(float x)
    {
        //initializing
        float count = 0.0f;
        bool damaged = false;
        GameObject blast = Instantiate(ChargeVisual, transform.position, transform.rotation);
        //loop for the blast
        while (count < x)
        {
            //increasing the size of the blast
            radius = count;
            //visual for blast
            blast.transform.localScale = new Vector3(radius, radius, radius) * 2;
            //get all colliders in the sphere
            foreach (Collider pcollider in Physics.OverlapSphere(transform.position, radius, enemiesLayer))
            {
                IAttributes cIA = pcollider.gameObject.GetComponent<IAttributes>();
                //check if gameobject is damagable
                if (cIA != null)
                {
                    //loop to check if the object was damaged before
                    for (int i = 0; i < enemiesDamaged.Count; i++)
                    {
                        if (enemiesDamaged[i].gameObject == pcollider.gameObject)
                        {
                            damaged = true;
                            break;
                        }
                    }
                    //if not damaged before
                    if (!damaged)
                    {
                        //do damage to the enemy
                        CalcDmg(pcollider.gameObject);
                        //push back the enemy
                        PushBack(pcollider.gameObject);
                        //add enemy to damagedenemies
                        enemiesDamaged.Add(pcollider.gameObject);
                    }
                }
                damaged = false;
            }
            count += Time.deltaTime * BlastSpeedMultiplyer;

            yield return null;
        }
        //reset variables to get ready for next blast
        coroutineOn = false;
        chargeCount = 0;
        Destroy(blast);
        enemiesDamaged.Clear();
        //radius = 0.01f;
    }


    //function for pushing back the enemy
    private void PushBack(GameObject enemy)
    {
        enemy.GetComponent<Rigidbody>().AddForce((enemy.transform.position - transform.position).normalized * PushBackForce);
        //get distance between player and the enemy
        float dist = Vector3.Distance(enemy.transform.position, transform.position);
        //if close range
        if (dist <= radiusMax * (1f / 3f))
        {
            enemy.GetComponent<Rigidbody>().AddForce((enemy.transform.position - transform.position).normalized * (PushBackForce * (3f / 3f)));
            print("Big push");
        }
        //if medium range
        else if (dist <= radiusMax * (2f / 3f))
        {
            enemy.GetComponent<Rigidbody>().AddForce((enemy.transform.position - transform.position).normalized * (PushBackForce * (2f / 3f)));
            print("Medium med");
        }
        //if long range
        else if (dist <= radiusMax)
        {
            enemy.GetComponent<Rigidbody>().AddForce((enemy.transform.position - transform.position).normalized * (PushBackForce * (1f / 3f)));
            print("Small small");
        }
    }


    //function for managing inputs
    private void Charge()
    {
        //while key is held add to radius if it hasnt reached max radius
        if (Input.GetKey(KeyCode.L) && !coroutineOn)
        {
            if (chargeCount < radiusMax)
            {
                chargeCount += radiusChargeSpeed * Time.deltaTime;
            }
        }
        //on key released call the blast coroutine with the blast radius calculated
        if (Input.GetKeyUp(KeyCode.L))
        {
            StartCoroutine(RadialAction(chargeCount));
            coroutineOn = true;
        }
    }


    //function to do damage based on distance from the player
    private void CalcDmg(GameObject enemy)
    {
        //get distance between player and the enemy
        float dist = Vector3.Distance(enemy.transform.position, transform.position);
        //if close range
        if (dist <= radiusMax * (1f / 3f))
        {
            //enemy.gameObject.GetComponent<IAttributes>().TakeDamage(bigDmg, false);
            print("Big Damage");
        }
        //if medium range
        else if (dist <= radiusMax * (2f / 3f))
        {
            //enemy.gameObject.GetComponent<IAttributes>().TakeDamage(medDmg, false);
            print("Medium Damage");
        }
        //if long range
        else if (dist <= radiusMax)
        {
            // enemy.gameObject.GetComponent<IAttributes>().TakeDamage(smallDmg, false);
            print("Small Damage");
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
