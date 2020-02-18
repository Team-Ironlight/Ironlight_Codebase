using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class td_StateMachineSetup : MonoBehaviour
{
    [ExecuteAlways]
    public class ExampleClass : MonoBehaviour
    {
        void Start()
        {
            if (Application.IsPlaying(gameObject))
            {
                // Play logic
            }
            else
            {
                // Editor logic

                print("What's good");
            }
        }
    }
}
