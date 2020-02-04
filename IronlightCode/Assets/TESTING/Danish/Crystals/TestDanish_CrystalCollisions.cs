using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDanish_CrystalCollisions : MonoBehaviour, IHit
{
    public TestDanish_RotatingCrystal crystal;

    public void HitWithLight(float pAmount)
    {
        crystal.lineActive = true;
        Vector3 start = crystal.startPoint.position;
        Vector3 end = crystal.startPoint.position + (crystal.CrystalFace.forward * crystal.RayDistance);

        RaycastHit hit;
        if (Physics.Linecast(start, end, out hit, crystal.lm))
        {
            IHit hitThing = hit.transform.GetComponent<IHit>();

            if (hitThing != null)
            {
                hitThing.HitWithLight(0);
                //if (hitThing != lastHitThing)
                //{
                //    hitThing.EnterHitWithLight(0);
                //    lastHitThing.ExitHitWithLight();

                //}
            }
            print("Draw a line");
            //hitThing = lastHitThing;



        }
        crystal._line.SetPosition(1, end);

        Debug.DrawLine(start, end, Color.green);
    }
    public void EnterHitWithLight(float pAmount)
    {

    }
    public void ExitHitWithLight()
    {
        crystal.lineActive = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            crystal.playerCanActivate = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            crystal.playerCanActivate = false;
        }
    }
}
