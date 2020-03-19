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

    public AudioSource sound;
    public AudioClip soundToPlay;
    public float volume;
   
    [SerializeField] [Range(0, 1)] private float value = 0;

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
        if (goMarkOne)
        {
            value += Time.deltaTime * speed;
        }
        else
        {
            value -= Time.deltaTime * speed;
        }
    

        if (value >= 1)
        {
            sound.PlayOneShot(soundToPlay, volume);
            Enemy.transform.LookAt(markerOne.transform);
            goMarkOne = false;
            value = 1;
        }
        
        if (value <= 0)
        {
            sound.PlayOneShot(soundToPlay, volume);
            Enemy.transform.LookAt(markerTwo.transform.position);
            goMarkOne = true;
            value = 0;
            //Enemy.transform.Translate(Vector3.forward * Time.deltaTime*speed);
        }

        Enemy.transform.position = Vector3.Lerp(markerOne.transform.position, markerTwo.transform.position, value);
        //Enemy.transform.Translate(Enemy.transform.forward * Time.deltaTime * speed);
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
