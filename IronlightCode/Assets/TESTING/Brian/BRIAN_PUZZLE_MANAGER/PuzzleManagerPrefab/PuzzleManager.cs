using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{

    [SerializeField]
    bool PuzzleComplete;


    public PuzzleSO puzzle;

    public List<GameObject> nodeObjs = new List<GameObject>();
    public List<GameObject> NodeTransform = new List<GameObject>();
    public CrystalObjPooler ObjPool;
    public IGoal puzzleGoal;
    RotateCrystal on;
    // Start is called before the first frame update
    void Start()
    {

        //if (puzzle.NodeCount >=0 )
        //
        //    print(puzzle.NodeCount)
        //}


        //This places the Rotate crystals at the position of the empty game objects in the scene
        // the objects are placed in the reverse order of the empty game objects
        if (puzzle.CurrType == PuzzleSO.PuzzleType.ConnectBeam)
        {
            GameObject[] CrystalTransform = GameObject.FindGameObjectsWithTag(puzzle.PuzzleTagName);
           // GameObject[] rotateCrystals = GameObject.FindGameObjectsWithTag(puzzle.PuzzleTagName);

            for (int i = 0; i < CrystalTransform.Length; i++)
            {

                GameObject Temp = ObjPool.RotCrytalsPrefab[i].gameObject;

                Temp.SetActive(true);

                Temp.transform.SetPositionAndRotation(CrystalTransform[i].transform.position, CrystalTransform[i].transform.rotation);

                nodeObjs.Add(Temp);
                if (i == 0)
                { 
                    on = Temp.GetComponent<RotateCrystal>();
                    on.lineActive = true;
                }
            }

        }
        //This places the activate crystals at the position of the empty game objects in the scene
        // the objects are placed in the reverse order of the empty game objects
        if (puzzle.CurrType == PuzzleSO.PuzzleType.ActivateCrystals)
        {
            GameObject[] CrystalTransform = GameObject.FindGameObjectsWithTag(puzzle.PuzzleTagName);
            // GameObject[] rotateCrystals = GameObject.FindGameObjectsWithTag(puzzle.PuzzleTagName);

            for (int i = 0; i < CrystalTransform.Length; i++)
            {
                GameObject Temp = ObjPool.ActiveCrystalPrefab[i].gameObject;

                Temp.SetActive(true);

                Temp.transform.SetPositionAndRotation(CrystalTransform[i].transform.position, CrystalTransform[i].transform.rotation);

                nodeObjs.Add(Temp);
            }

        }
    }

    // Update is called once per frame
    void Update()
    {

    }

}
