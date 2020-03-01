using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Projectile", menuName = "ScriptableObjects/Projectile_Seeker")]
public class SO_Projectile_Seeker : ScriptableObject
{
    public float velocity = 1f;
    public float lifeTime = 1f;
    [Range(0.1f, 2f)] public float seekIntensity = 0.5f;
    [Range(0.1f, 1f)] public float seekControl = 0.1f;
}
