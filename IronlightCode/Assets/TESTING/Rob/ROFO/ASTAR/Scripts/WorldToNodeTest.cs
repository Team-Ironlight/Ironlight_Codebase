using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ROFO
{
    public class WorldToNodeTest : MonoBehaviour
    {
        public Grid grid;
        public bool inGrid = false;

        private void Update()
        {
            inGrid = grid.CheckWorldPointInGrid(transform.position);
            Debug.Log("<color=red>In Grid: </color>" + inGrid);
        }

        private void OnDrawGizmos()
        {
            if (inGrid)
            {
                GridNode g = grid.WorldPointToGridNode(transform.position);
                Gizmos.color = Color.green;
                Gizmos.DrawCube(g.worldPosition, Vector3.one);
            }
        }
    }
}


