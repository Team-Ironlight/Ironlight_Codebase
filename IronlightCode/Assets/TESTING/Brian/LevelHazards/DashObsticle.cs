using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashObsticle : MonoBehaviour
{
    public GameObject Enemy;
    public GameObject markerOne;
    public GameObject markerTwo;
    bool darting = false;
    public float speed=5;
    bool goMarkOne;

    // Start is called before the first frame update
    void Start()
    {
        Enemy.transform.position = markerOne.transform.position;


    }

    // Update is called once per frame
    void Update()
    {
        if (darting)
        {
            Dart();
        }
    }

    void Dart()
    {
        print("Move bitch");
        if (Enemy.transform.position.z <= markerOne.transform.position.z)
        {
            Enemy.transform.LookAt(markerTwo.transform);
            goMarkOne = false;
        }
            Enemy.transform.Translate(Vector3.forward * Time.deltaTime * speed);
        
            if (Enemy.transform.position.z >= markerTwo.transform.position.z)
        {
            Enemy.transform.LookAt(markerOne.transform.position);
            goMarkOne = true;
            Enemy.transform.Translate(Vector3.forward * Time.deltaTime*speed);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            print("LetsDash");
            darting = true;
        }
    }
       
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            returnHome();
        }
    }
    void returnHome()
    {
        if (goMarkOne)
        {
            Enemy.transform.Translate(Vector3.forward * Time.deltaTime * speed);
            if(!goMarkOne)
            {
                darting = false;
            }
        }
    }
}
