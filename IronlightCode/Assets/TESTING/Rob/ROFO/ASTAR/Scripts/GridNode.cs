using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ROFO
{
    public class GridNode
    {
        //constructor
        public GridNode(Vector3 gridPosition, Vector3 worldPosition, bool isClear)
        {
            this.gridPosition = gridPosition;
            this.worldPosition = worldPosition;
            this.isClear = isClear;
        }

        //store array position in grid
        public Vector3 gridPosition;
        public Vector3 worldPosition;
        public GridNode parent;
        public bool isClear;

        //distance to start
        public int Gcost;
        //distance to end node
        public int Hcost;
        //total cost
        public int FCost()
        {
            return Gcost + Hcost;
        }
    }
}
