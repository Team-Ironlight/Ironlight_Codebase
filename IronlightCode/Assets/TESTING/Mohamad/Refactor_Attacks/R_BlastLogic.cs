using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sharmout.SO;

public class R_BlastLogic : MonoBehaviour
{
    private float currentRadius = 0;
    private float radius = 0;
    private float chargeCount = 0;
    [SerializeField] private float radiusMax = 10f;
    [SerializeField] private float radiusChargeSpeed = 1f;
    [SerializeField] private float BlastSpeedMultiplyer = 1f;

    Vector3 centerPoint = Vector3.zero;

    Coroutine currentCo = null;
    public Transform visual = null;

    public Transform blast = null;

    List<LayerMask> layersToCheck = new List<LayerMask>();

    // does the pre-charge before launching blast attack
    public void Tick(Vector3 center)
    {
        centerPoint = center;
        visual.position = centerPoint;

        if (chargeCount < radiusMax)
        {
            chargeCount += radiusChargeSpeed * Time.deltaTime;
        }
    }

    public void Init(BlastSO stats)
    {
        radiusMax = stats._radiusMax;
        radiusChargeSpeed = stats._radiusChargeSpeed;
        BlastSpeedMultiplyer = stats._BlastSpeedMultiplyer;
        layersToCheck = stats.layersToCheck;
    }



    private void testCollider()
    {
        foreach (var layer in layersToCheck)
        {
            foreach (Collider pcollider in Physics.OverlapSphere(blast.transform.position, radius, layer))
            {
               if ( pcollider.gameObject.tag == "Enemy")
                {
                    Debug.Log("Enemy Hit");
                }
   
            }

        }

    }

    public void OnRelease()
    {
        currentCo = StartCoroutine(BlastOff(chargeCount));
    }

   


    IEnumerator BlastOff(float charge)
    {
        radius = charge;
        while (currentRadius < radius)
        {
            currentRadius += BlastSpeedMultiplyer * Time.deltaTime;

            visual.localScale = new Vector3(currentRadius, currentRadius, currentRadius) * 2;

            yield return null;
        }

        yield return new WaitForEndOfFrame();

        radius = 0;
        chargeCount = 0;
        currentRadius = 0;

        currentCo = null;
        gameObject.SetActive(false);
    }
}
