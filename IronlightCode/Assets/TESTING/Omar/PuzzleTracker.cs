using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ROFO;

public class PuzzleTracker : MonoBehaviour
{

	public List<GameObject> puzzleList = new List<GameObject>(); //List of puzzle on level
	public List<GameObject> solvedPuzzleList = new List<GameObject>(); //List of solved puzzles

	public bool endCheckActive = false;
	public bool resetPlayer = false;

    public GameObject crystalReset;
    public List<GameObject> CrystalGOS = new List<GameObject>();

	void Start()
	{
		GameObject[] Shrines = GameObject.FindGameObjectsWithTag("Shrine");
		for (int i = 0; i < Shrines.Length; i++)
		{
			if (Shrines[i].GetComponent<BeamPuzzleShrine>() != null)
			{
				puzzleList.Add(Shrines[i]);
			}
		}

        
	}

	void Update()
	{
		CheckActivationState(); //check if the puzzle is solved or not

		if (resetPlayer == true)
		{

			ResetPlayer();
		}
	}


	void CheckActivationState() //Check enemy is dead or alive
	{
		for (int i = 0; i < puzzleList.Count; i++)
		{
			if (puzzleList[i].activeSelf && !puzzleList[i].GetComponent<BeamPuzzleShrine>().LinkActive) //Alive, continue
			{
				break;
			}
			else
			{
				if (!solvedPuzzleList.Contains(puzzleList[i]) && puzzleList[i].GetComponent<BeamPuzzleShrine>().LinkActive) //If solved, add them to the solved list
				{
					solvedPuzzleList.Add(puzzleList[i]);
				}
				else
				{
					break;
				}
			}
		}
	}

	void ResetPlayer()
	{
		if (!endCheckActive) //Player didn't hit new checkpoint, respawn player and remove all puzzle in the solved list and activate them in the all the puzzle list.
		{
			solvedPuzzleList.Clear();

			for (int i = 0; i < puzzleList.Count; i++)
			{
				if (!puzzleList[i].activeSelf || (puzzleList[i].activeSelf && puzzleList[i].GetComponent<BeamPuzzleShrine>().LinkActive))
				{
					puzzleList[i].SetActive(true);
                     //Add code to reset puzzle here......................................
					puzzleList[i].GetComponent<BeamPuzzleShrine>().LinkActive = false;
                    if (puzzleList[i].transform.parent.GetComponentInChildren<RotateChange>()!=null)
                    {
                       /* foreach()
                        {
                            
                        }
                        crystalReset.GetComponent<RotateChange>().ResetCrystals();*/
                    }
                }
				else
				{
					continue;
				}
			}
		}
		else if (endCheckActive) //Player hit new checkpoint, remove the puzzles solved from the puzzle list and clear solved puzzle list. 
		{
			for (int i = 0; i < puzzleList.Count; i++)
			{
				for (int k = 0; k < solvedPuzzleList.Count; k++)
				{
					if (puzzleList[i] == solvedPuzzleList[k])
					{
						//puzzleList[i].SetActive(false);
						//Destroy(puzzleList[i]);
						puzzleList.RemoveAt(i);
					}
				}
			}
		}

		resetPlayer = false;
	}

}
