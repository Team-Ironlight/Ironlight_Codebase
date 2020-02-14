using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamPuzzleShrine : MonoBehaviour, IHit, ICrystal
{
    public GameObject Plat1;
    public GameObject Plat2;
    public GameObject Plat3;
    public GameObject Plat4;
    // Start is called before the first frame update

    public bool LinkActive = false;
    public Transform crystalPos;

    void Start()
    {
        Plat1.SetActive(false);
        Plat2.SetActive(false);
        Plat3.SetActive(false);
        Plat4.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (LinkActive)
        {
            Plat1.SetActive(true);
            Plat2.SetActive(true);
            Plat3.SetActive(true);
            Plat4.SetActive(true);
        }
    }
    public void HitWithLight(float pAmount)
    {
        //LinkActive = true;
        //Debug.Log("HIT");
    }
    public void EnterHitWithLight(float pAmount)
    { }
    public void ExitHitWithLight()
    { }

    public void isActivated()
    {
        LinkActive = true;
        Debug.Log("HIT");
    }
}
