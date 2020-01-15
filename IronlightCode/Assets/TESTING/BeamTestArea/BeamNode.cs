using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamNode : MonoBehaviour
{
    private PLY_3rdBeam BeamAccess;

    private GameObject NextPoint;

    private int nextNode;
    // Start is called before the first frame update
    void Start()
    {
        BeamAccess = GameObject.FindGameObjectWithTag("Beam").GetComponent<PLY_3rdBeam>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnNext()
    {
        NextPoint = Instantiate(gameObject, transform.position, transform.rotation);
        BeamAccess.beamLists.Add(NextPoint);
    }

    public void AdjustNextNode()
    {
    }
}
