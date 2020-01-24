using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PLY_InputHandler : MonoBehaviour
{
    float vertical;
    float horizontal;

    bool jump;
    bool orb;
    bool beamStart;
    bool beamEnd;
    bool radial;
    bool dodge;

    float delta;

    PLY_StateManager playerState;
    //CameraManager cameraManager;

    // Start is called before the first frame update
    void Start()
    {
        playerState = GetComponent<PLY_StateManager>();
        playerState.Init();

        //cameraManager = CameraManager.singleton;
        //cameraManager.Init(transform);

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
        //cameraManager.Tick(delta);
    }



    #region Functions

    void GetInput()
    {
        vertical = Input.GetAxis("Vertical");
        horizontal = Input.GetAxis("Horizontal");

        jump = Input.GetKeyDown(KeyCode.Z);
        orb = Input.GetKeyDown(KeyCode.Mouse1);
        beamStart = Input.GetKeyDown(KeyCode.Mouse0);
        beamEnd = Input.GetKeyUp(KeyCode.Mouse0);
        radial = Input.GetKeyUp(KeyCode.LeftShift);
        dodge = Input.GetKeyDown(KeyCode.C);
    }


    void UpdateStates()
    {
        playerState.vertical = vertical;
        playerState.horizontal = horizontal;

        playerState.orb = orb;
        playerState.beamStart = beamStart;
        playerState.beamEnd = beamEnd;
        playerState.radial = radial;
        playerState.jump = jump;
        playerState.dodge = dodge;
    }


    #endregion
}
