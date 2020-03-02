using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveCrystalManager : MonoBehaviour
{
    public GameObject Crystal1;
    public GameObject Crystal2;
    public GameObject Crystal3;
    ActivateCrystal Active1;
    ActivateCrystal Active2;
    ActivateCrystal Active3;
    [SerializeField]
    bool solved;
    // Start is called before the first frame update
    void Start()
    {
        Active1 = Crystal1.GetComponent<ActivateCrystal>();
        Active2 = Crystal2.GetComponent<ActivateCrystal>();
        Active3 = Crystal3.GetComponent<ActivateCrystal>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Active1.activated == true && Active2.activated == true && Active3.activated == true)
        {
            solved = true;
        }
    }
}
