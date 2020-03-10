using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AITEST
{
    public interface IProjectile
    {
        void Setup(Transform transform);
        void Fire(Transform transform);

        //for whatever checks need to happen
        bool Check();
    }
}


