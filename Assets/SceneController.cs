using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        // Check if the "Escape" key is pressed
        if (Input.GetKeyDown(KeyCode.T))
        {
            // Stop the scene (quit the application in this example)
            Application.Quit();
        }
    }
}
