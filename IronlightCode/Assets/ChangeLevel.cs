using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ChangeLevel : MonoBehaviour
{
    public Animator anim;
    private int levelToLoad;

        private void OnTriggerEnter(Collider other)
        {

            if (other.gameObject.CompareTag("Player"))
            {

            anim.SetTrigger("FadeOut");
           
            }
        }

    public void FadeToLevel(int levelIndex)
    {
        levelToLoad = levelIndex;
        anim.SetTrigger("FadeOut");
    }

     public void OnFadeComplete()
    {
            SceneManager.LoadScene(2);
    }


}

