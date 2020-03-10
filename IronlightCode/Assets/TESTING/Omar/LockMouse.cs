using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LockMouse : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Cursor.lockState = CursorLockMode.Locked;
  //      if (SceneManager.GetActiveScene().buildIndex==1)
		//{
		//	Cursor.lockState = CursorLockMode.Locked;
		//}
		//else
		//{
		//	Cursor.lockState = CursorLockMode.None;
		//}
    }
}
