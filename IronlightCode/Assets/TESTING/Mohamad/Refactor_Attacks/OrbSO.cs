using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sharmout.SO
{
    [CreateAssetMenu(fileName = "Orb Stats.asset", menuName = "Attack Stats/Orb", order = 0)]
    public class OrbSO : ScriptableObject
    {
        public float TraveliSpeed = 0;
        public float DamageAmount = 0;
        public float DisableTimer = 0;
    }
}