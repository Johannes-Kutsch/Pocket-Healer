using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class TalentsButtons : MonoBehaviour
{
    private bool spellSelected = false;

    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("MainMenu");
        }
    }

    public void SwitchToFightScene()
    {
        GameControl.control.SwitchCurrentLevel(GameControl.control.currentLevelId);
        GameControl.control.Save();
        SceneManager.LoadScene(GameControl.control.currentLevel);
    }

    public void SwitchToSkillSelectorScene()
    {
        GameControl.control.Save();
        SceneManager.LoadScene("Skill_Selector");
    }
}
