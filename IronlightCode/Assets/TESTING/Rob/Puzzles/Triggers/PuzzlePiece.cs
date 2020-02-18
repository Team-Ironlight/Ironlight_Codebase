using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this object is apart of a group
public class PuzzlePiece : MonoBehaviour, ITrigger
{
    //puzzle group this object is apart of
    public string apartOfGroup;
    public int order;

    //runs 1 iteration of all of its changes
    public void Trigger()
    {
        for (int i = 0; i < changes.Length; i++)
        {
            changes[i].Change();
        }
    }

    private AChange[] changes;
    public AChange[] GetChanges()
    {
        return changes;
    }

    //get all changes attached to this object
    private AChange[] GatherChanges()
    {
        return GetComponents<AChange>();
    }

    private void Start()
    {
        changes = GatherChanges();
    }
}
