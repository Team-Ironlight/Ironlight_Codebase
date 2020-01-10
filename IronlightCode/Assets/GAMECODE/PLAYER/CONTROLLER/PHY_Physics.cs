using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PHY_Physics : MonoBehaviour
{
    [SerializeField] private Vector3 _vVelocity;
    [SerializeField] private float _fVerticalVelocity;
    [SerializeField] private Vector2 _vHorizontalVelocity;
    [SerializeField] private Vector2 _vHorizontalFriction;
    [SerializeField] private float _fGravity;
    [SerializeField] private float _fVerticalDrag;
    [SerializeField] private Vector3 _vVerticalNormal;

    [SerializeField] [Range(0, 4f)] private float raycastLength;
    [SerializeField] private Vector3 raycastStartOffset;
    [SerializeField] [Range(0, 4f)] private float raycastExtraLength;
    [SerializeField] private float raycastExtraMult;
    [SerializeField] private float raycastMult;

    [SerializeField] private float coefficientOfFriction = 0.4f;

    void Start()
    {
        
    }

    void Update()
    {
        CalculateVerticalVelocity();
        CalculateHorizontalVelocity();

        _vVelocity = new Vector3(_vHorizontalVelocity.x, _fVerticalVelocity, _vHorizontalVelocity.y);

        // Normals
        CalculateNormals();

        transform.position += _vVelocity * Time.deltaTime;

        _fVerticalVelocity = _vVelocity.y;
        _vHorizontalVelocity = new Vector2(_vVelocity.x, _vVelocity.z);
    }

    public void AddHorizontalAcceleration(Vector2 pVelocity)
    {
        _vHorizontalVelocity += pVelocity * Time.deltaTime;
    }

    public void SetVerticalForce(float pForce)
    {
        _fVerticalVelocity = pForce;
    }

    #region Vertical Functions

    private void CalculateVerticalVelocity()
    {
        // Gravity
        _fVerticalVelocity += _fGravity * Time.deltaTime;

        // Drag
        CalculateVerticalDrag();
        _fVerticalVelocity += _fVerticalDrag * Time.deltaTime;
    }

    private void CalculateVerticalDrag()
    {
        //float dragMagnitude = (Mathf.Pow(_fVerticalVelocity * _dragVFactor, 2) - _dragVDefault) / 2 * _dragVMult;
        //if (_fVerticalVelocity < )
    }

    #endregion

    #region Horizontal Functions

    private void CalculateHorizontalVelocity()
    {
        // Friction
        CalculateFriction();
        _vHorizontalVelocity += _vHorizontalFriction * Time.deltaTime;
    }
    private void CalculateFriction()
    {
        Vector2 friction = coefficientOfFriction * _vHorizontalVelocity;
        if (friction.magnitude <= 0.001f)
            _vHorizontalFriction = -_vHorizontalVelocity;
        else
            _vHorizontalFriction = -friction;
    }

    #endregion

    #region Normal Functions

    private void CalculateNormals()
    {
        RaycastHit hit;
        if (_fVerticalVelocity <= 0 && Physics.Raycast(transform.position + raycastStartOffset, Vector3.down, out hit, raycastLength))
        {
            CalculateVerticalNormal(hit);
            _vVelocity += _vVerticalNormal;
        }
    }

    private void CalculateVerticalNormal(RaycastHit pHit)
    {
        float normalMult = -pHit.distance + raycastExtraLength;
        if (normalMult < 0)
        {
            normalMult = (normalMult + 1) * raycastExtraMult;
        }
        else
        {
            normalMult = (normalMult * raycastMult) + 1;
        }

        Vector3 normalForce = Vector3.Project(_vVelocity, pHit.normal) * normalMult;
        _vVerticalNormal = -normalForce;
    }

    #endregion

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position + raycastStartOffset, transform.position + raycastStartOffset + (Vector3.down * raycastLength));
        Gizmos.DrawSphere(transform.position + raycastStartOffset + (Vector3.down * raycastExtraLength), 0.1f);
    }
}
