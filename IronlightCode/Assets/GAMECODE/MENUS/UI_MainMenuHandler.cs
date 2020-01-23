using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_MainMenuHandler : MonoBehaviour
{  
    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);     
    } 
    
    
    
    public void Quit()
    {
        // SceneManager.LoadScene(3);
        Debug.Log("Quit Game");
        Application.Quit();
    }
}
