using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class TransformFinder
{
    //find parent of child
    public static Transform GetParent(Transform child)
    {
        Transform temp = child;
        if(child.parent)
        {
            temp = GetParent(child.parent);
        }

        return temp;
    }
}
