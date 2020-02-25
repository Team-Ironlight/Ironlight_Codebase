using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ROFO
{
    //when this object gets triggered, not by player but from a group
    //call, also trigger another group
    public class LinkTrigger : MonoBehaviour, ITrigger
    {
        public string groupToTrigger;



        public void Trigger()
        {

        }
    }
}
