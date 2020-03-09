using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sharmout.SO
{
    [CreateAssetMenu(fileName = "Blast Stats.asset", menuName = "Attack Stats/Blast", order = 0)]
    public class BlastSO : ScriptableObject
    {
        public float _radiusMax = 0;
        public float _radiusChargeSpeed = 0f;
        public float _BlastSpeedMultiplyer = 0f;
        public List<LayerMask> layersToCheck = new List<LayerMask>();
    }
}
