using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SkillSelectorButtons : MonoBehaviour
{
    private bool spellSelected = false;
    public GameObject popup;
    public GameObject description;

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
            popup.SetActive(true);
        }
    }

    void FixedUpdate()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("MainMenu");
        }
    }

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
            Tooltips.tooltips.SetText("Bitte wähle wenigstens einen Heilungszauber aus, klicke dazu zuerst auf den Zauber und danach auf einen Button in deiner Actionbar.");
        }
        else
        {
            GameControl.control.SwitchCurrentLevel(GameControl.control.currentLevelId);
            GameControl.control.Save();
            SceneManager.LoadScene(GameControl.control.currentLevel);
        }

    }

    public void SwitchToTalentScene()
    {
        GameControl.control.Save();
        SceneManager.LoadScene("Talents");
    }

    public void HidePopup()
    {
        popup.SetActive(false);
    }

    public void ShowDescription()
    {
        description.SetActive(true);
    }

    public void HideDescription()
    {
        description.SetActive(false);
    }
}

