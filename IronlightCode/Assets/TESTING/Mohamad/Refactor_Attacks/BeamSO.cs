using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sharmout.SO
{
    [CreateAssetMenu(fileName = "Beam Stats.asset", menuName = "Attack Stats/Beam", order = 0)]
    public class BeamSO : ScriptableObject
    {
        public int beamRange = 0;
        public float speedGoing = 0;
        public float speedEnding = 0;
    }
}