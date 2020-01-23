using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlTestScript : MonoBehaviour
{
    TestController inputs;
    public CT_StateManager manager;

    public GameObject m_firePoint;
    public Animator m_Anim;

    private void Awake()
    {
        inputs = new TestController();

        manager = GetComponent<CT_StateManager>();
        manager.Init();
    }

    private void OnEnable()
    {
        inputs.Combat.Enable();
    }

    private void OnDisable()
    {
        inputs.Combat.Disable();
    }





}
