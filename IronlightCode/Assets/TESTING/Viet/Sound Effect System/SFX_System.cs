using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Viet.Components
{
    public class SFX_System : MonoBehaviour
    {

        public List<SoundSO> soundEffects = new List<SoundSO>();

        public bool allPlay;

        public void PlaySoundById(int id) //Play by ID
        {
            foreach(var sfx in soundEffects)
            {
                if(id == sfx.id)
                {
                    Debug.Log("Clip with ID found");
                }
                else
                {
                    Debug.Log("Clip with ID not found");
                }
            }
        }

        public void StopSoundById(int id) //Stop by ID
        {
            foreach (var sfx in soundEffects)
            {
                if (id == sfx.id)
                {
                    Debug.Log("Clip with ID Stop");
                }
                else
                {
                    Debug.Log("Not Found ID to Stop");
                }
            }
        }

        public void PlayAllSound() //Play all sound
        {
            Debug.Log("play all");
        }

        public void StopAllSound() //Stop all sound
        {
            Debug.Log("stop all");
        }
      
    }
}