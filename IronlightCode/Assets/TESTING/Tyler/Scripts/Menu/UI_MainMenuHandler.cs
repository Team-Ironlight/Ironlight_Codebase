using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_MainMenuHandler : MonoBehaviour
{  
    public void NewGame()
    {
        SceneManager.LoadScene(1);     
    } 
    
    public void Option()
    {
        SceneManager.LoadScene(2);     
    } 
    
    public void Quit()
    {
        // SceneManager.LoadScene(3);
        Debug.Log("Quit Game");
    }
}
