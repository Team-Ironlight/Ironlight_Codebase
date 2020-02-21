using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ROFO
{
    public class Pathfind : MonoBehaviour
    {
        public Transform grid;
        public Transform target;
        public float speed = 1f;
        private Grid g;
        private GridNode[] path;
        private Coroutine c = null;

        private void Start()
        {
            g = grid.GetComponent<Grid>();
        }

        private void Update()
        {
            //testing
            if (Input.GetMouseButtonUp(1))
            {
                //get path
                path = GetPath();

                //move along path
                Debug.Log("Start FollowPath");
                c = null;
                c = StartCoroutine(FollowPath());
            }
        }

        private GridNode[] GetPath()
        {
            return g.FindPath(transform.position, target.position);
        }

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
        }
    }
}
