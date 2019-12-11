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

    float delta;

    PlayerStateManager playerState;
    CameraManager cameraManager;

    // Start is called before the first frame update
    void Start()
    {
        playerState = GetComponent<PlayerStateManager>();
        playerState.Init();

        cameraManager = CameraManager.singleton;
        cameraManager.Init(playerState);

    }

    private void Update()
    {
        delta = Time.deltaTime;
        playerState.Tick();
        GetInput();
    }

    private void FixedUpdate()
    {
        delta = Time.fixedDeltaTime;
        UpdateStates();
        playerState.FixedTick(delta);
        cameraManager.Tick(delta);
    }



    #region Functions

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


    #endregion
}
