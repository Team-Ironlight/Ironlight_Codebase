using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class imanStateRunner : MonoBehaviour
{
    Owl_StateManager stateManager;

    // Start is called before the first frame update
    void Start()
    {
        stateManager = GetComponent<Owl_StateManager>();

        stateManager.Init();
    }

    // Update is called once per frame
    void Update()
    {
        stateManager.Tick();
    }
}
