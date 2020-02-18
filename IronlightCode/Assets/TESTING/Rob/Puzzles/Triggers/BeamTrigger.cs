using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//needs a group to trigger on
//go through the group and check if piece is in correct pos
//lerp a particle from piece to next, iterate
//if puzzle piece is not in correct position just shoot particle on obj forward
public class BeamTrigger : MonoBehaviour, ITrigger
{
    public string groupToBeam;
    public string groupToTrigger;
    public string tagTrigger;
    private int groupToBeamLength;
    public float beamSpeed = 1f;
    //GroupTrigger[] group;
    public GameObject projectile;
    public bool isOn = false;
    private Coroutine c = null;

    private void Start()
    {
        //get length of group to check for success
        groupToBeamLength = PuzzleManager.GetLength(groupToBeam);
    }

    //trigger for this object is to send proj through puzzle
    public void Trigger()
    {
        if(isOn == false)
        {
            isOn = true;

            //c = StartCoroutine(Beam());
            c = StartCoroutine(BeamALT());
        }
    }

    //also need to activate groupToTrigger is successful
    private void Success()
    {
        bool work = PuzzleManager.IsGroupMoving(groupToTrigger);

        if (work)
        {
            PuzzleManager.TriggerThisGroup(groupToTrigger);
        }
    }

    //need generic statement that will work here
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == tagTrigger)
        {
            //send call to input manager to put in interaction list
            InputManagerTemp.SetInteractObject(this);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == tagTrigger)
        {
            //send call to input manager to put in interaction list
            InputManagerTemp.RemoveInteractObject(this);
        }
    }
       

    IEnumerator BeamALT()
    {
        //get array of objects in group starting from beginning that are in
        //the correct position and will have the beam interact with
        PuzzlePiece[] g = PuzzleManager.IsGroupInCorrectPositionArray(groupToBeam);
        Debug.Log("<color=blue>Correct Positions: </color>" + g.Length);

        //make projectile or particle but deactivate
        GameObject proj = Instantiate(projectile, transform.position, transform.rotation);
        proj.SetActive(false);

        int index = 0;
        while (index < g.Length - 1)
        {
            float count = 0.0f;

            //get lerp points
            Vector3 startPos = g[index].transform.position;
            Vector3 endPos = g[index + 1].transform.position;

            //temp
            startPos += Vector3.up * g[index].transform.localScale.y / 2f;
            endPos += Vector3.up * g[index].transform.localScale.y / 2f;

            //get distance so speed stays consistent
            float distance = (endPos - startPos).magnitude;

            //set proj active as it will move
            proj.SetActive(true);

            while (proj.transform.position != endPos)
            {
                count += Time.deltaTime;

                proj.transform.position = Vector3.Lerp(startPos, endPos, (beamSpeed * count) / distance);

                yield return null;
            }

            //turn off till calc again
            proj.SetActive(false);

            //increment index and restart from top
            index++;
        }

        //destroy proj
        Destroy(proj);


        //if entire group trigger check group
        if (g.Length == groupToBeamLength)
        {
            Success();
        }

        Reset();
    }
    

    //reset stuff
    private void Reset()
    {
        isOn = false;
        c = null;       
    }
}


