using System.Collections;
using System.Collections.Generic;
using UnityEngine;



namespace Danish.Components.Interfaces
{


    public interface IComponent
    {
        void Init();
        void Tick();
    }
}