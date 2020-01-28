using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamPuzzleShrine : MonoBehaviour, IHit
{
    public GameObject Plat1;
    public GameObject Plat2;
    public GameObject Plat3;
    // Start is called before the first frame update
    void Start()
    {
        Plat1.SetActive(false);
        Plat2.SetActive(false);
        Plat3.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void HitWithLight(float pAmount)
    {
        Plat1.SetActive(true);
        Plat2.SetActive(true);
        Plat3.SetActive(true);
        Debug.Log("HIT");
    }
    public void EnterHitWithLight(float pAmount)
    { }
    public void ExitHitWithLight()
    { }
}
