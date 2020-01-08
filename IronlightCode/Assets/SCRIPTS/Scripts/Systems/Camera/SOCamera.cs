using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Camera ScriptableObject", menuName = "Camera/New Camera Preset", order = 0)]
public class SOCamera : ScriptableObject
{
    [Header("Position")]
    public float UpOffset = 0.6f;
    public float LeftOffset = 0.0f;
    public float Distance = 6.0f;
    
    [Header("Input")]
    public float TurnDampening = 20.0f;
    public float SensitivityMult = 1;

    [Header("Constraints")]
    public float MaxY = 70.0f;
    public float MinY = -8.0f;
}
