using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDanish_RotatingCrystal : MonoBehaviour
{
    public LayerMask lm;
    public bool lineActive;
    public bool playerCanActivate;
    public float smoothRot = 1;
    public float RayDistance = 5;
    public LineRenderer _line;
    public Transform startPoint;
    public Transform CrystalFace;

    //private GameObject Crystal;
    private Quaternion targetRot;
    IHit lastHitThing;


    void Start()
    {
        //Crystal = GetComponentInParent<GameObject>();
        targetRot = CrystalFace.rotation;
        _line.gameObject.SetActive(true);

        _line.SetPosition(0, startPoint.position);
        _line.SetPosition(1, startPoint.position);
    }

    void Update()
    {
        if (playerCanActivate)
        {
            Rotate();
        }

        //if (lineActive)
        //{
        //    HitWithLight(0);
        //}
        //else
        //{
        //    _line.SetPosition(1, startPoint.position);
        //}

    }

    void Rotate()
    {

        if (Input.GetKeyDown(KeyCode.Q))
        {
            targetRot *= Quaternion.AngleAxis(45, Vector3.up);
            print("Rotate");
        }
        CrystalFace.rotation = Quaternion.Lerp(CrystalFace.rotation, targetRot, 10 * smoothRot * Time.deltaTime);
    }


}
