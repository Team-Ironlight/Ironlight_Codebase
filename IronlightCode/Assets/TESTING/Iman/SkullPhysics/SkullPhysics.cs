using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkullPhysics : MonoBehaviour
{

    private List<GameObject> Original = new List<GameObject>();
    private List<GameObject> Broken = new List<GameObject>();
    [SerializeField] private GameObject BrokenSkull;


    // Start is called before the first frame update
    void Start()
    {
        //get all pieces in original object
        foreach (Transform t in transform)
        {
            //add to list
            Original.Add(t.gameObject);
        }
        //get all pieces in broken version
        foreach (Transform t in BrokenSkull.transform)
        {
            //add to list
            t.gameObject.SetActive(false);
            //set deactive
            Broken.Add(t.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Destroyed();
        }
    }

    private void Destroyed()
    {
        //go through all the pieces and replace original with broken pieces
        for (int i = 0; i < Original.Count; i++)
        {
            Broken[i].SetActive(true);
            Broken[i].transform.position = Original[i].transform.position;
            Broken[i].transform.rotation = Original[i].transform.rotation;
            Original[i].SetActive(false);
        }
    }
}
