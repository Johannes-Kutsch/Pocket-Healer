using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

/// <summary>
/// Responsible for Navigation during the talent selection Scene
/// </summary>
public class TalentsButtons : MonoBehaviour
{
    /// <summary>
    /// Called on every update.
    /// </summary>
    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("MainMenu"); //escape key is pressed
        }
    }

    /// <summary>
    /// Called when the "Next" button is pressed.
    /// </summary>
    public void SwitchToFightScene()
    {
        GameControl.control.SwitchCurrentLevel(GameControl.control.currentLevelId);
        GameControl.control.Save();
        SceneManager.LoadScene(GameControl.control.currentLevel);
    }

    /// <summary>
    /// Called when the "Talents" button is pressed.
    /// </summary>
    public void SwitchToSkillSelectorScene()
    {
        GameControl.control.Save();
        SceneManager.LoadScene("Skill_Selector");
    }
}
