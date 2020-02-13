using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSync : MonoBehaviour
{
    public float bias; //Dettermines the value that creates a "beat"
    public float timeStep; //set the time between detecting beats
    public float timeToBeat;//length of time for visual to match beat
    public float restSmoothTime; // how long the visual takes to reset after a beat

    float prevAudioVal;
    float audioVal;
    float timer;

    protected bool isBeat; //true is beat has occured
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        onUpdate();
    }
    public virtual void onUpdate()
    {
        prevAudioVal = audioVal;
        audioVal = AudioSpectrum.spectrumVal;

        if (prevAudioVal > bias && audioVal <= bias)
        {
            if (timer > timeStep)
            {
                OnBeat();
            }
        }
        if (prevAudioVal <= bias && audioVal > bias)
        {
            if (timer > timeStep)
            {
                OnBeat();
            }
        }
        timer += Time.deltaTime;
    }
    public virtual void OnBeat()
    {
        timer = 0;
        isBeat = true;
    }
}
