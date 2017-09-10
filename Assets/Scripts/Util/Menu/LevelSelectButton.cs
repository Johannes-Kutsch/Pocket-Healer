using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

/// <summary>
/// Attached to each level-button in the level selct scene.
/// Manages the displaying/destroying of these Buttons and the onClick event of these Buttons.
/// </summary>
public class LevelSelectButton : MonoBehaviour {
    public int levelID; //The ID of the Button the Scipt is attached to.

    /// <summary>
    /// Called on start.
    /// </summary>
    void Start() {
        if(GameControl.control.maxLevelIdUnlocked < levelID)
        {
            Destroy(gameObject); //Level is not yet unlocked, destroy the Button.
        }
    }

    /// <summary>
    /// Called when the button is clicked.
    /// Sets the currentLevelId to the levelID of the button and loads the "SkillSelect" scene.
    /// </summary>
    public void Click() {
        GameControl.control.currentLevelId = levelID;
        GameControl.control.Save();
        SceneManager.LoadScene("Skill_Selector");
    }

}
