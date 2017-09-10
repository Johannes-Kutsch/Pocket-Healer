using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

/// <summary>
/// Listens for the escape button during the level select scene.
/// </summary>
public class LevelSelect : MonoBehaviour {

    /// <summary>
    /// Called in each simulation tick i.e. 50 times a second.
    /// Loads the main menu scene when the escape button is pressed.
    /// </summary>
    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}
