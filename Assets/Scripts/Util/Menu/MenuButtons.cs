using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Responsible for Navigation during the talent selection Scene and Settings.
/// </summary>
public class MenuButtons : MonoBehaviour {
    public GameObject options;
    public Button easy;
    public Button hard;
    public Slider volume;
    private bool reset = false;

    /// <summary>
    /// Called on Start.
    /// </summary>
    void Start()
    {
        volume.value = GameControl.control.soundMultiplier; 
    }

    /// <summary>
    /// Called with every update.
    /// Responsible for quitting the app and adjusting the sound volume.
    /// </summary>
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GameControl.control.Save();
            Application.Quit();
        }
        GameControl.control.source.volume = volume.value; //ToDO: Only adjust volume when the slider is moved, not on every update
    }

    /// <summary>
    /// Called when the "Play" button is pressed.
    /// Loads the spell select scene.
    /// </summary>
    public void LoadSkillSelector()
    {
        GameControl.control.currentLevelId = GameControl.control.maxLevelIdUnlocked;
        GameControl.control.Save();
        SceneManager.LoadScene("Skill_Selector");
    }

    /// <summary>
    /// Called when the "Talents" button is pressed.
    /// Loads the talents scene.
    /// </summary>
    public void LoadTalents()
    {
        GameControl.control.Save();
        SceneManager.LoadScene("Talents");
    }

    /// <summary>
    /// Called when the "Tutorial" button is pressed.
    /// Loads the tutorial Scene.
    /// </summary>
    public void LoadTutorial()
    {
        GameControl.control.Save();
        SceneManager.LoadScene("Tutorial");
    }

    /// <summary>
    /// Called when the "Options" button is pressed.
    /// Enables the options popup.
    /// </summary>
    public void OptionsPopup()
    {
        switch (GameControl.control.difficulty) {
            case 0:
                easy.image.color = new Color32(150, 150, 150, 255);
                hard.image.color = new Color32(130, 130, 130, 255);
                break;
            case 1:
                hard.image.color = new Color32(150, 150, 150, 255);
                easy.image.color = new Color32(130, 130, 130, 255);
                break;
        }
        options.SetActive(true);
        volume.value = GameControl.control.soundMultiplier;
    }

    /// <summary>
    /// Called when the "Reset" button in the options popup is pressed.
    /// Shedules a reset for the next "Save" button press.
    /// </summary>
    public void Reset()
    {
        reset = true;
    }

    /// <summary>
    /// Called when the "Save" button in the options popup is pressed.
    /// Saves all changes.
    /// </summary>
    public void OptionsSave()
    {
        options.SetActive(false);
        if (reset)
        {
            GameControl.control.ResetProgress();
            reset = false;
        }
        GameControl.control.soundMultiplier = volume.value;
        GameControl.control.Save();
    }

    /// <summary>
    /// Called when the "Discard" button in the options popup is pressed.
    /// Discards all changes made and unshedules the game reset.
    /// </summary>
    public void OptionsDiscard()
    {
        volume.value = GameControl.control.soundMultiplier;
        reset = false;
        options.SetActive(false);
    }

    /// <summary>
    /// Called when the "Hard" button in the options popup is pressed.
    /// Changes the difficulty to hard.
    /// </summary>
    public void OptionsHard()
    {
        GameControl.control.difficulty = 1;
        hard.image.color = new Color32(150, 150, 150, 255);
        easy.image.color = new Color32(130, 130, 130, 255);
    }

    /// <summary>
    /// Called when the "Easy" button in the options popup is pressed.
    /// Changes the difficulty to easy.
    /// </summary>
    public void OptionsEasy()
    {
        GameControl.control.difficulty = 0;
        easy.image.color = new Color32(150, 150, 150, 255);
        hard.image.color = new Color32(130, 130, 130, 255);
    }

    /// <summary>
    /// Called when the "Selectlevel" button is pressed.
    /// Loads the levelselect Scene.
    /// </summary>
    public void SelectLevel()
    {
        GameControl.control.Save();
        SceneManager.LoadScene("Level_Selector");
    }

    /// <summary>
    /// Called when the "Exit" button is pressed.
    /// Exits the game.
    /// </summary>
    public void Exit()
    {
        GameControl.control.Save();
        Application.Quit();
    }
}
