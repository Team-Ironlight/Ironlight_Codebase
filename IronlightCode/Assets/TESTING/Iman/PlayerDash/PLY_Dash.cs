using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PLY_Dash : MonoBehaviour
{
    [SerializeField] private float dashForce;
    [SerializeField] private float dashDuration;
    [SerializeField] private float DashCooldown;
    private float CDTimer;
    [HideInInspector] public bool InputRecievced;

    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
    }

    private void GetInput()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (CDTimer < Time.time)
            {
                StartCoroutine(Dash());
                InputRecievced = true;
                CDTimer = Time.time + DashCooldown;
            }
        }
        else
        {
            InputRecievced = false;
        }
    }

    IEnumerator Dash()
    {
        rb.AddForce(transform.forward * dashForce, ForceMode.VelocityChange);

        yield return new WaitForSeconds(dashDuration);

        rb.velocity = Vector3.zero;
    }
}
