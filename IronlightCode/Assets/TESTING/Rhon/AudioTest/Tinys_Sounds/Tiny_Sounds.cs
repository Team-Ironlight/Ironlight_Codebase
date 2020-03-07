using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tiny_Sounds : MonoBehaviour
{
    public Tiny_SoundSystem sfx_Tiny = null;
    public int playID = 0;

    // Start is called before the first frame update
    void Start()
    {
       // sfx_Tiny = GetComponent<Tiny_SoundSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            sfx_Tiny.PlaySoundById(playID);
        }
    }
}