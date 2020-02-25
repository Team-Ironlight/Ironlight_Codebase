using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ROFO
{
    //needs access to the grid...
    public class PF_GoTo : IState
    {
        //constructor
        public PF_GoTo(GameObject grid, Transform parent, Transform player, float speed)
        {
            g = grid.GetComponent<Grid>();
            this.parent = parent;
            this.player = player;
            this.speed = speed;
        }

        private Grid g;
        private Transform parent;
        private Transform player;
        private GridNode[] path;
        private float speed;
        private float distance;

        public override void Enter()
        {
            path = g.FindPath(parent.position, player.position);
            count = 0f;
            index = 0;
            startPos = parent.position;
            distance = (path[index + 1].worldPosition - startPos).magnitude / speed;
        }

        private float count = 0f;
        private int index = 0;
        private Vector3 startPos;
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
            else
            {
                //reset
                path = g.FindPath(parent.position, player.position);
                count = 0f;
                index = 0;

                //if ontop of player already don't do anything
                if (path.Length > 1)
                {
                    startPos = parent.position;
                    distance = (path[index + 1].worldPosition - startPos).magnitude / speed;
                }
            }
        }

        public override void Exit()
        {

        }

        public override string Name()
        {
            return "PF_GoTo";
        }
    }
}
