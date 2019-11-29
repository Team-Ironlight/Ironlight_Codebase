using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    float vertical;
    float horizontal;

    bool jump;
    bool attack;
    bool dodge;

    PlayerStateManager playerState;

    // Start is called before the first frame update
    void Start()
    {
        playerState = GetComponent<PlayerStateManager>();

        playerState.Init();
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
        UpdateStates();
    }


    void GetInput()
    {
        vertical = Input.GetAxis("Vertical");
        horizontal = Input.GetAxis("Horizontal");

        jump = Input.GetKeyDown(KeyCode.Z);
        attack = Input.GetKeyDown(KeyCode.X);
        dodge = Input.GetKeyDown(KeyCode.C);
    }


    void UpdateStates()
    {
        playerState.vertical = vertical;
        playerState.horizontal = horizontal;

        playerState.attack = attack;
        playerState.jump = jump;
        playerState.dodge = dodge;
    }
}
