using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ROFO
{
    public class PF_Wander : IState
    {
        //constructor
        public PF_Wander(GameObject grid, Transform parent, float speed)
        {
            g = grid.GetComponent<Grid>();
            this.parent = parent;
            this.speed = speed;
        }

        private Grid g;
        private Transform parent;
        private GridNode[] path;
        private float speed;

        public override void Enter()
        {
            GetPath();
        }

        private float count = 0f;
        private int index = 0;
        private Vector3 startPos;
        private float distance;
        public override void Execute(Transform t)
        {
            if (index < path.Length - 1)
            {
                //use a count, iterate through the path
                if (count < distance)
                {
                    count += Time.deltaTime;
                    parent.position = Vector3.Lerp(startPos, path[index + 1].worldPosition, count / distance);
                }
                else
                {
                    count = 0f;
                    index++;
                    if (index < path.Length - 1)
                    {
                        startPos = parent.position;
                        distance = (path[index + 1].worldPosition - startPos).magnitude / speed;
                    }
                }
            }
            //try to set up conditions where the enemy will not run this for
            //a set amount of time after completing a path
            else if (index >= path.Length - 1)
            {
                //new path
                GetPath();
            }
        }

        public override void Exit()
        {

        }

        public override string Name()
        {
            return "PF_Wander";
        }


        private void GetPath()
        {
            //get random point in grid
            //get random grid point that isn't blocked
            //point = new Vector3(-1, -1, -1);
            int x;
            int y;
            int z;
            do
            {
                x = Random.Range(0, g.length - 1);
                y = Random.Range(0, g.height - 1);
                z = Random.Range(0, g.width - 1);
            }
            while (g.GetGrid()[x, y, z].isClear == false);
            //get own position as node in grid
            GridNode start = g.WorldPointToGridNode(parent.position);
            path = g.FindPath(start, g.GetGrid()[x, y, z]);

            count = 0f;
            index = 0;
            startPos = parent.position;
            distance = (path[index + 1].worldPosition - startPos).magnitude / speed;
        }
    }
}
