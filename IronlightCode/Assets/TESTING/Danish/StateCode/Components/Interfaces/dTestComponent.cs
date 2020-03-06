using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Danish.Components
{

    [CreateAssetMenu(fileName = "Test Component.asset", menuName = "Components/Test")]
    public class dTestComponent : Abstract.dBaseComponent
    {
        public float moveSpeed = 0;
        public Transform camera = null;
        public Vector3 vector = Vector3.zero;



        public override void Init()
        {
            base.Init();
        }

        public override void Tick()
        {
            base.Tick();
        }
    }
}