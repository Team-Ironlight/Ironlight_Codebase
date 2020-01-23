using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PLY_3rdBeam : MonoBehaviour
{

    [HideInInspector] public List<GameObject> beamLists = new List<GameObject>();
    [SerializeField] public GameObject Muzzle;
    public GameObject Node;

    public LineRenderer line;
    public int MaxNodes;
    public int SpaceBetweenNodes = 5;
    // Start is called before the first frame update
    void Start()
    {

        for(int i = 0;i < 2; i++)
        {
            GameObject clone;
            clone = Instantiate(Node, transform.position, transform.rotation);
            beamLists.Add(clone);
            if(i == 1)
            {
                clone.transform.position = Muzzle.transform.forward * SpaceBetweenNodes;
                clone.GetComponent<BeamNode>().SpawnNext();
            }
        }

        line.positionCount = 10;
    }

    // Update is called once per frame
    void Update()
    {
        DrawLine();
    }

    void DrawLine()
    {
        for (int i = 0; i < beamLists.Count; i++)
        {
            line.SetPosition(i, beamLists[i].transform.position);
        }
    }
}
