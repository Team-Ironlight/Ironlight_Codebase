using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
public class AudioSpectrum : MonoBehaviour
{
    public static float spectrumVal { get; set; }
    public AudioMixer ElderMix;
    private float[] _audioSpectrum;
    public float val;
    public AudioSource source;
    
    // Start is called before the first frame update
    void Start()
    {

        _audioSpectrum = new float[128];
       
    }

    // Update is called once per frame
    void Update()
    {
       // source.clip = getSound.soundToPlay;
        source.outputAudioMixerGroup = ElderMix.FindMatchingGroups("ElderVocals")[0];
        //ElderMix.GetFloat("ElderVocals", out val);
        //AudioListener.GetSpectrumData(_audioSpectrum, 0, FFTWindow.Hamming);
        source.GetSpectrumData(_audioSpectrum, 0, FFTWindow.Hamming);
        if (_audioSpectrum!=null && _audioSpectrum.Length > 0)
        {
            spectrumVal = _audioSpectrum[0] * 100;
            //ElderMix.audioMixer.FindMatchingGroups("ElderVocals");
        }
    }
}
