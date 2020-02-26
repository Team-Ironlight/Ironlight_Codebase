using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ROFO
{
    public class WanderTemp : MonoBehaviour
    {
        public Transform grid;
        public float speed;
        private Grid g;
        private GridNode[] path;
        public bool wander = false;
        private Vector3 point;

        private float count;

        private void Awake()
        {
            g = grid.GetComponent<Grid>();
        }

        // Update is called once per frame
        void Update()
        {
            if (wander == false)
            {
                //get random grid point that isn't blocked
                point = new Vector3(-1, -1, -1);
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
                GridNode start = g.WorldPointToGridNode(transform.position);
                path = g.FindPath(start, g.GetGrid()[x, y, z]);

                if (path != null)
                {
                    wander = true;
                    c = null;
                    c = StartCoroutine(FollowPath());
                }
            }
        }

        private Coroutine c = null;

        IEnumerator FollowPath()
        {
            int index = 0;
            //Debug.Log("Length: " + path.Length + " Travel: " + (path.Length - 2));        
            while (index < path.Length - 1)
            {
                Vector3 start = path[index].worldPosition;
                Vector3 end = path[index + 1].worldPosition;
                float distance = (end - start).magnitude / speed;

                float count = 0f;
                while (count < distance)
                {
                    count += Time.deltaTime;
                    transform.position = Vector3.Lerp(path[index].worldPosition, path[index + 1].worldPosition, count / distance);
                    yield return null;
                }

                index++;
                yield return null;
            }

            //reset 
            wander = false;
        }
    }
}
