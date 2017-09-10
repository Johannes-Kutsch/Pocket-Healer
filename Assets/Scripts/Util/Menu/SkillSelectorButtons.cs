using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

/// <summary>
/// This class is responsible for scene navigation and displaying of popups.
/// </summary>
public class SkillSelectorButtons : MonoBehaviour
{
    private bool spellSelected = false; 
    public GameObject spellSelectHintPopup;
    public GameObject bossDescription;

    /// <summary>
    /// Called on start.
    /// Displays a popup explaining the spell selection if no spell is selected (first visit of the Screen)
    /// </summary>
    void Start()
    {
        foreach (int id in GameControl.control.spellId)
        {
            if (id != 0)
            {
                spellSelected = true;
            }
        }
        if (spellSelected == false)
        {
            spellSelectHintPopup.SetActive(true);
            //ToDo: Change spell selection to drag and drop.
        }
    }

    /// <summary>
    /// Called in each simulation tick i.e. 50 times a second.
    /// Loads the main menu scene if the "Escape" key is pressed
    /// </summary>
    void FixedUpdate()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("MainMenu");
        }
    }

    /// <summary>
    /// Called when the "Continue" button is pressed.
    /// Switches to the nex fight/level scene.
    /// </summary>
    public void SwitchToFightScene()
    {
        foreach (int id in GameControl.control.spellId)
        {
            if (id != 0)
            {
                spellSelected = true;
            }
        }
        if (spellSelected == false)
        {
            Tooltips.tooltips.SetText("Please equip at least one spell to continue. To equip a spell first click on a unlocked spell and then on a slot in your actionbar.");
        }
        else
        {
            GameControl.control.SwitchCurrentLevel(GameControl.control.currentLevelId);
            GameControl.control.Save();
            SceneManager.LoadScene(GameControl.control.currentLevel);
        }

    }

    /// <summary>
    /// Called when the "Talents" button is pressed.
    /// Loads the talents scene.
    /// </summary>
    public void SwitchToTalentScene()
    {
        GameControl.control.Save();
        SceneManager.LoadScene("Talents");
    }

    /// <summary>
    /// Called when the "Understood" button in the spell select hin popup is pressed.
    /// Hides the spell select hint popup.
    /// </summary>
    public void HideSpellSelectHintPopup()
    {
        spellSelectHintPopup.SetActive(false);
    }

    /// <summary>
    /// Called when the "Tipp" button is pressed.
    /// Shows the boss desription.
    /// </summary>
    public void ShowDescription()
    {
        bossDescription.SetActive(true);
    }

    /// <summary>
    /// Called when the "Understood" button in the boss description is pressed.
    /// Hides the boss desription.
    /// </summary>
    public void HideDescription()
    {
        bossDescription.SetActive(false);
    }
}

