using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Viet.Components
{

    [CreateAssetMenu(fileName = "SFX.asset", menuName = "GameFX/New Sound Effect")]
    public class SoundSO : ScriptableObject
    {
        public int id;
        public bool Looping;
        public AudioClip Audio;

    }
}