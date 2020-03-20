using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LoadCredits : MonoBehaviour
{

    public void Credits()
    {       

        SceneManager.LoadScene(2);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
         
            Credits();
        }
    }


}
