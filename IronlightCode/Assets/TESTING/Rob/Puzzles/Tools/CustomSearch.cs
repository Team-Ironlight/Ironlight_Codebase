using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomSearch
{
    //generic raycast method that gets are hit information
    private static RaycastHit[] Search(GameObject obj, int numbRays, float viewRange, float viewDis)
    {
        //list to store hits
        List<RaycastHit> hits = new List<RaycastHit>();

        RaycastHit hit;
        float deg = viewRange / numbRays;
        float start = -(viewRange / 2f);

        //shoot rays
        for (int i = 0; i <= numbRays; i++)
        {
            //get rotated vector
            Vector3 dir = LinearRotation.AroundY(obj.transform.forward * viewDis, start + deg * i);

            //draw lines            
            Debug.DrawRay(obj.transform.position, dir, Color.green, 5f);
            //Debug.Log("Dir: " + dir);

            //raycast
            if (Physics.Raycast(obj.transform.position, dir, out hit, viewDis))
            {
                hits.Add(hit);
            }
        }

        //return array for space purposes
        return hits.ToArray();
    }

    //returns the PRIVATE search results
    public static RaycastHit[] AllCollisions(GameObject obj, int numbRays, float viewRange, float viewDis)
    {
        return Search(obj, numbRays, viewRange, viewDis);
    }


    //allow things to raycast search for specific gameObjects
    public static bool FindObject(GameObject obj, int numbRays, float viewRange, float viewDis, string objTag)
    {
        RaycastHit[] hits = Search(obj, numbRays, viewRange, viewDis);

        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i].collider.gameObject.tag == objTag)
            {
                return true;
            }
        }

        return false;
    }

    //get the gameobject of the nearest type of object
    public static GameObject GetNearestObj(GameObject obj, int numbRays, float viewRange, float viewDis, LayerMask layer)
    {
        RaycastHit[] hits = Search(obj, numbRays, viewRange, viewDis);

        //extract only objects with correct tag/layer
        List<Transform> correctObj = new List<Transform>();
        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i].transform.gameObject.layer == layer)
            {
                correctObj.Add(hits[i].transform);
            }
        }

        //if nothing with correct tag/layer found return null
        if (correctObj.Count == 0)
        {
            return null;
        }

        Transform[] cO = correctObj.ToArray();


        //get closest collision
        int cc = -1;
        float closestCollision = viewDis;
        Vector3 startPos = obj.transform.position;

        for (int i = 0; i < hits.Length; i++)
        {
            if ((cO[i].position - startPos).magnitude < closestCollision)
            {
                closestCollision = (cO[i].position - startPos).magnitude;

                //store the i position of the obj
                cc = i;
            }
        }

        //couldn't find it
        return cO[cc].gameObject;
    }

    //search for the nearest collision to object within specified range
    //return gameobjects information
    public static GameObject ClosestCollision(GameObject obj, int numbRays, float viewRange, float viewDis)
    {
        RaycastHit[] hits = Search(obj, numbRays, viewRange, viewDis);

        int cc = -1;
        float closestCollision = viewDis;
        Vector3 startPos = obj.transform.position;

        for (int i = 0; i < hits.Length; i++)
        {
            if ((hits[i].point - startPos).magnitude < closestCollision)
            {
                closestCollision = (hits[i].point - startPos).magnitude;

                //store the i position of the obj
                cc = i;
            }
        }

        return hits[cc].collider.gameObject;
    }
}
