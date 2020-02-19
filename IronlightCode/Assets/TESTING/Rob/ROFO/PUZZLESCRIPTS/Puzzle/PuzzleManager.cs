using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ROFO
{
    //for now
    //gather all gameobjects with a trigger script into an array
    //iterate through array and enter each object with a GROUP trigger into a dictionary
    //the dictionary will be of type <String, List<ITrigger>>
    public class PuzzleManager : MonoBehaviour
    {
        //dictionary that holds all groups that get triggered by something
        private static Dictionary<string, PuzzlePiece[]> groups = new Dictionary<string, PuzzlePiece[]>();


        private void Start()
        {
            DictionarySetup();
        }

        private void DictionarySetup()
        {
            //NOTE
            //It only finds active objects from your current scene
            //If an object is disabled at run time, or not in your current scene, it won’t grab it.
            //PuzzlePiece[] temp = FindObjectsOfType<PuzzlePiece>();
            PuzzlePiece[] temp = GetComponentsInChildren<PuzzlePiece>();

            //convert array to list
            List<PuzzlePiece> gT = new List<PuzzlePiece>();

            for (int i = 0; i < temp.Length; i++)
            {
                gT.Add(temp[i]);
            }

            List<PuzzlePiece> mini = new List<PuzzlePiece>();

            while (gT.Count != 0)
            {
                //add first element of list
                mini.Add(gT[0]);
                gT.RemoveAt(0);

                for (int i = 0; i < gT.Count; i++)
                {
                    if (gT[i].apartOfGroup == mini[0].apartOfGroup)
                    {
                        mini.Add(gT[i]);
                        gT.RemoveAt(i);
                        i--;
                    }
                }

                PuzzlePiece[] t = SortGroup(mini.ToArray());

                groups.Add(t[0].apartOfGroup, t);
                Debug.Log("<color=red>PuzzleGroupApartOf: </color>" + t[0].apartOfGroup +
                          "   <color=red>Count: </color>" + t.Length);

                mini.Clear();
            }
        }


        private PuzzlePiece[] SortGroup(PuzzlePiece[] g)
        {
            bool change = true;
            while (change)
            {
                change = false;
                for (int i = 0; i < g.Length - 1; i++)
                {
                    if (g[i].order > g[i + 1].order)
                    {
                        //swap 
                        PuzzlePiece temp = g[i];
                        g[i] = g[i + 1];
                        g[i + 1] = temp;

                        change = true;
                    }
                }
            }

            return g;
        }


        public static PuzzlePiece[] GetGroup(string groupName)
        {
            if (groupName != null)
            {
                return groups[groupName];
            }

            return null;

        }

        public static int GetLength(string groupName)
        {
            return groups[groupName].Length;
        }



        public static bool IsGroupMoving(string groupName)
        {
            PuzzlePiece[] temp = GetGroup(groupName);

            for (int i = 0; i < temp.Length; i++)
            {
                AChange[] t = temp[i].GetChanges();
                for (int j = 0; j < t.Length; j++)
                {
                    if ((AChangeTransform)t[j] == false)
                    {
                        Debug.Log("Invalid cast to AChangeTransform");
                        continue;
                    }

                    AChangeTransform a = (AChangeTransform)t[j];

                    if (a.GetIsMoving())
                    {
                        //something in the list is still moving therefore don't run anything
                        Debug.Log("<color=blue>Something is moving</color>");
                        return false;
                    }
                    else
                    {
                        Debug.Log("<color=red>Nope</color>");
                    }
                }
            }

            //everything passed
            return true;
        }

        public static bool IsGroupInCorrectPosiition(string groupName)
        {
            //get check group
            PuzzlePiece[] temp = PuzzleManager.GetGroup(groupName);

            //check condition of group
            //go through all of its IChanges
            for (int i = 0; i < temp.Length; i++)
            {
                AChange[] c = temp[i].GetChanges();

                for (int j = 0; j < c.Length; j++)
                {
                    if ((AChangeTransform)c[j] == false)
                    {
                        Debug.Log("Invalid cast to AChangeTransform");
                        continue;
                    }

                    AChangeTransform a = (AChangeTransform)c[j];

                    //check if has correct position
                    //if not than consider as in correct pos
                    if (a.hasCorrectPosition == false)
                    {
                        continue;
                    }
                    else if (a.hasCorrectPosition)
                    {
                        if (a.IsInCorrectPos() == false)
                        {
                            //if any change is in the wrong position
                            //return and don't run anything
                            return false;
                        }
                    }
                }
            }

            //if all changes are in the correct position, trigger the trigger group
            return true;
        }

        public static PuzzlePiece[] IsGroupInCorrectPositionArray(string groupName)
        {
            //get check group
            PuzzlePiece[] temp = PuzzleManager.GetGroup(groupName);


            List<PuzzlePiece> returnGroup = new List<PuzzlePiece>();

            //check condition of group
            //go through all of its IChanges
            for (int i = 0; i < temp.Length; i++)
            {
                //if no changes exist than always in correct pos
                if (temp[i].GetChanges().Length == 0)
                {
                    returnGroup.Add(temp[i]);
                    Debug.Log("<color=purple>Doesn't have changes: </color>" + returnGroup.Count);
                    continue;
                }

                AChange[] c = temp[i].GetChanges();
                for (int j = 0; j < c.Length; j++)
                {
                    if ((AChangeTransform)c[j] == false)
                    {
                        Debug.Log("Invalid cast to AChangeTransform");
                        continue;
                    }

                    AChangeTransform a = (AChangeTransform)c[j];

                    //check if has correct position
                    //if not than consider as in correct pos
                    if (a.hasCorrectPosition == false)
                    {
                        returnGroup.Add(temp[i]);
                        Debug.Log("<color=purple>Doesn't have correct pos: </color>" + returnGroup.Count);
                        continue;
                    }
                    else if (a.hasCorrectPosition)
                    {
                        if (a.IsInCorrectPos() == false)
                        {
                            //if any change is in the wrong position
                            //return and don't run anything
                            Debug.Log("<color=red>Found incorrect pos: </color>" + returnGroup.Count);
                            //make sure to incorrect one as we still need to have projectile reach it
                            returnGroup.Add(temp[i]);
                            return returnGroup.ToArray();
                        }

                        //is in correct position add to array
                        returnGroup.Add(temp[i]);
                    }
                }
            }

            //if all changes are in the correct position, trigger the trigger group
            return returnGroup.ToArray();
        }



        public static void TriggerThisGroup(string groupName)
        {
            //get check group
            PuzzlePiece[] temp = PuzzleManager.GetGroup(groupName);

            Debug.Log("<color=green>Moving Group</color>");
            for (int i = 0; i < temp.Length; i++)
            {
                //trigger calls all changes attached to that object
                temp[i].Trigger();
            }
        }
    }
}
