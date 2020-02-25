using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ROFO
{
    public class Grid : MonoBehaviour
    {
        public Vector3 gridSize;
        public Vector3 nodeSize;
        [Tooltip("For Draw Gizmos")]
        [Range(0.1f, 1f)] public float nodeScale = 0.5f;

        private GridNode[,,] grid;
        public GridNode[,,] GetGrid()
        {
            return grid;
        }
        private Vector3 topRight;
        private Vector3 bottomLeft;
        public int length;
        public int width;
        public int height;

        //testing
        [Header("test")]
        public Transform target;
        public Transform seeker;
        private GridNode[] n;


        private void Start()
        {
            Setup();
        }

        private void Update()
        {
            if (Input.GetMouseButtonUp(1))
            {
                //testing
                FindPath(seeker.position, target.position);
            }
        }

        private void Setup()
        {
            //get bottom corner of grid for calculations
            topRight = transform.position - nodeSize / 2f + gridSize / 2f;
            bottomLeft = transform.position + nodeSize / 2f - gridSize / 2f;

            //determine the grid values for length, width, height
            length = Mathf.RoundToInt(gridSize.x / nodeSize.x);
            width = Mathf.RoundToInt(gridSize.z / nodeSize.z);
            height = Mathf.RoundToInt(gridSize.y / nodeSize.y);

            Debug.Log("Length: " + length + " Height: " + height + " Width: " + width);

            //create array equal to grid sizes
            grid = new GridNode[length, height, width];

            //populate array and set GridNode values
            for (int h = 0; h < height; h++)
            {
                for (int w = 0; w < width; w++)
                {
                    for (int l = 0; l < length; l++)
                    {
                        Vector3 spot = bottomLeft + new Vector3(nodeSize.x * l,
                                                                nodeSize.y * h,
                                                                nodeSize.z * w);

                        bool b = true;
                        if (Physics.CheckBox(spot, nodeSize / 2f))
                        {
                            b = false;
                        }

                        //create gridNode
                        grid[l, h, w] = new GridNode(new Vector3(l, h, w), spot, b);
                        //Debug.Log("Grid: " + grid[l, w, h].position);
                    }
                }
            }
        }

        //checks if position is within grid
        public bool CheckWorldPointInGrid(Vector3 position)
        {
            if (Mathf.Abs(position.x - transform.position.x) < gridSize.x / 2f &&
               Mathf.Abs(position.y - transform.position.y) < gridSize.y / 2f &&
               Mathf.Abs(position.z - transform.position.z) < gridSize.z / 2f)
            {
                return true;
            }

            return false;
        }

        //return the node this position is in
        //assuming position is within range... call above method
        public GridNode WorldPointToGridNode(Vector3 position)
        {
            //position is within grid, position % in grid
            float x = position.x - bottomLeft.x;
            float y = position.y - bottomLeft.y;
            float z = position.z - bottomLeft.z;

            //get indexs for node
            int X = Mathf.RoundToInt(x / nodeSize.x);
            int Y = Mathf.RoundToInt(y / nodeSize.y);
            int Z = Mathf.RoundToInt(z / nodeSize.z);

            //why inverted???
            return grid[X, Y, Z];
        }

        public GridNode[] GetNeighbors(GridNode target)
        {
            //gridNodes contain their array position in grid
            List<GridNode> neighbors = new List<GridNode>();
            int X = (int)target.gridPosition.x;
            int Y = (int)target.gridPosition.y;
            int Z = (int)target.gridPosition.z;

            //check all 26 potential surrounding nodes
            for (int x = -1; x <= 1; x++)
            {
                //outside bounds
                if (X + x < 0 || X + x > length - 1)
                {
                    continue;
                }

                for (int y = -1; y <= 1; y++)
                {
                    //outside bounds
                    if (Y + y < 0 || Y + y > height - 1)
                    {
                        continue;
                    }

                    for (int z = -1; z <= 1; z++)
                    {
                        //outside bounds
                        if (Z + z < 0 || Z + z > length - 1)
                        {
                            continue;
                        }

                        //skip self
                        if (x == 0 && x == 0 && z == 0)
                        {
                            continue;
                        }

                        //all good add to list
                        neighbors.Add(grid[X + x, Y + y, Z + z]);
                    }
                }
            }

            //Test
            n = neighbors.ToArray();

            return neighbors.ToArray();
        }

        //find path given nodes in grid
        public GridNode[] FindPath(GridNode startNode, GridNode endNode)
        {
            //begin A* algorithm
            List<GridNode> openSet = new List<GridNode>();
            HashSet<GridNode> closedSet = new HashSet<GridNode>();

            openSet.Add(startNode);

            while (openSet.Count > 0)
            {
                //find node in openset with lowest Fcost
                //start at 1 to dodge checking self
                GridNode currentNode = openSet[0];
                for (int i = 1; i < openSet.Count; i++)
                {
                    //iterate through all open nodes, if fcost lower make current
                    if (openSet[i].FCost() < currentNode.FCost())
                    {
                        currentNode = openSet[i];
                    }
                    //if same Fcost select node with shortest Hcost, distance to end
                    else if (openSet[i].FCost() == currentNode.FCost())
                    {
                        currentNode = openSet[i].Hcost > currentNode.Hcost ? openSet[i] : currentNode;
                    }
                }

                //take out node that will be worked on
                openSet.Remove(currentNode);
                closedSet.Add(currentNode);

                //check if found end node
                if (currentNode == endNode)
                {
                    Debug.Log("Found end");
                    //retrace path from start to end
                    GridNode[] path = RetracePath(startNode, endNode);
                    return path;
                }

                //get neighbors
                GridNode[] neighbors = GetNeighbors(currentNode);

                for (int i = 0; i < neighbors.Length; i++)
                {
                    //skip if neighbor is in closed list or can't be traversed
                    //Debug.Log("Neighbor Array Position: " + neighbors[i].gridPosition);
                    if (closedSet.Contains(neighbors[i]) || neighbors[i].isClear == false)
                    {
                        continue;
                    }

                    //calculate distance from current to neighbor
                    int newCost = currentNode.Gcost + CalculateDistance(currentNode, neighbors[i]);
                    //if distance shorter than current gcost(distance to other neighbor potentially)
                    //or the neighbor isn't in the open list, hasn't been examined yet
                    //set all the shit
                    if (newCost < neighbors[i].Gcost || openSet.Contains(neighbors[i]) == false)
                    {
                        neighbors[i].Gcost = newCost;
                        neighbors[i].Hcost = CalculateDistance(neighbors[i], endNode);
                        neighbors[i].parent = currentNode;

                        //if not in open list, add to open list as might need to be re-examined
                        if (openSet.Contains(neighbors[i]) == false)
                        {
                            openSet.Add(neighbors[i]);
                        }
                    }
                }
            }

            //just in case
            Debug.Log("Something went wrong");
            return null;
        }


        //find path given world points
        public GridNode[] FindPath(Vector3 start, Vector3 end)
        {
            Debug.Log("Start: " + start + " End: " + end);
            //Check if valid positions, inside grid
            if (CheckWorldPointInGrid(start) == false ||
               CheckWorldPointInGrid(end) == false)
            {
                Debug.Log("Not in grid");
                return null;
            }

            //convert to nodes
            GridNode startNode = WorldPointToGridNode(start);
            GridNode endNode = WorldPointToGridNode(end);

            GridNode[] path = FindPath(startNode, endNode);

            return path;
        }

        //move backwards through nodes, should be connected
        private GridNode[] RetracePath(GridNode start, GridNode end)
        {
            List<GridNode> path = new List<GridNode>();
            //retrace backwards
            GridNode currentNode = end;

            while (currentNode != start)
            {
                path.Add(currentNode);
                currentNode = currentNode.parent;
            }

            path.Reverse();
            n = path.ToArray();
            return path.ToArray();
        }

        //finds value distance from two nodes
        private int CalculateDistance(GridNode start, GridNode end)
        {
            int[] values = new int[3];
            values[0] = Mathf.Abs((int)start.gridPosition.x - (int)end.gridPosition.x);
            values[1] = Mathf.Abs((int)start.gridPosition.y - (int)end.gridPosition.y);
            values[2] = Mathf.Abs((int)start.gridPosition.z - (int)end.gridPosition.z);

            //value = 17shortest + 14(medium - shortest) + 10(longest - medium)
            //put them into an array and sort them???
            bool check = true;
            while (check)
            {
                check = false;
                for (int i = 0; i < values.Length - 1; i++)
                {
                    if (values[i] < values[i + 1])
                    {
                        int temp = values[i];
                        values[i] = values[i + 1];
                        values[i + 1] = temp;
                        check = true;
                    }
                }
            }

            //test
            int v = (17 * values[2]) + (14 * (values[1] - values[2])) + (10 * (values[0] - values[1]));
            //Debug.Log("Value: " + v);

            return v;
        }

        //Debug
        private void OnDrawGizmos()
        {
            DrawGrid();
        }

        private void DrawGrid()
        {
            //boundaries
            Gizmos.DrawWireCube(transform.position, gridSize);
            Vector3 offset = new Vector3(nodeSize.x / 2f, 0f, nodeSize.z / 2f);
            Vector3 topRight = transform.position - nodeSize / 2f + gridSize / 2f;
            Vector3 bottomLeft = transform.position + nodeSize / 2f - gridSize / 2f;
            Gizmos.DrawWireCube(transform.position + gridSize / 2f, Vector3.one);
            Gizmos.DrawWireCube(transform.position - gridSize / 2f, Vector3.one);

            //determine the grid values for length, width, height
            int length = Mathf.RoundToInt(gridSize.x / nodeSize.x);
            int width = Mathf.RoundToInt(gridSize.z / nodeSize.z);
            int height = Mathf.RoundToInt(gridSize.y / nodeSize.y);

            for (int h = 0; h < height; h++)
            {
                for (int w = 0; w < width; w++)
                {
                    for (int l = 0; l < length; l++)
                    {
                        Vector3 spot = bottomLeft + new Vector3(nodeSize.x * l,
                                                                nodeSize.y * h,
                                                                nodeSize.z * w);

                        if (Physics.CheckBox(spot, nodeSize / 2f))
                        {
                            Gizmos.color = Color.gray;
                        }
                        else
                        {
                            Gizmos.color = Color.clear;
                        }

                        Gizmos.DrawCube(spot, nodeSize * nodeScale);
                    }
                }
            }

            //draw neighbors
            if (n != null)
            {
                Gizmos.color = Color.red;
                for (int i = 0; i < n.Length; i++)
                {
                    Gizmos.DrawCube(n[i].worldPosition, Vector3.one * nodeScale);
                }
            }
        }
    }
}
