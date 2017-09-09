using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuButtons : MonoBehaviour {
    public GameObject options;
    public Button easy;
    public Button hard;
    public Slider volume;
    private bool reset = false;

    void Start()
    {
        volume.value = GameControl.control.soundMultiplyer;
    }

    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GameControl.control.Save();
            Application.Quit();
        }
        GameControl.control.source.volume = volume.value;
    }

    public void LoadSkillSelector()
    {
        GameControl.control.currentLevelId = GameControl.control.maxLevelIdUnlocked;
        GameControl.control.Save();
        SceneManager.LoadScene("Skill_Selector");
    }

    public void LoadTalents()
    {
        GameControl.control.Save();
        SceneManager.LoadScene("Talents");
    }

    public void LoadTutorial()
    {
        GameControl.control.Save();
        SceneManager.LoadScene("Tutorial");
    }

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
        volume.value = GameControl.control.soundMultiplyer;
    }

    public void Reset()
    {
        reset = true;
    }

    public void OptionsSave()
    {
        options.SetActive(false);
        if (reset)
        {
            GameControl.control.ResetProgress();
            reset = false;
        }
        GameControl.control.soundMultiplyer = volume.value;
        GameControl.control.Save();
    }

    public void OptionsDiscard()
    {
        volume.value = GameControl.control.soundMultiplyer;
        reset = false;
        options.SetActive(false);
    }

    public void OptionsHard()
    {
        GameControl.control.difficulty = 1;
        hard.image.color = new Color32(150, 150, 150, 255);
        easy.image.color = new Color32(130, 130, 130, 255);
    }

    public void OptionsEasy()
    {
        GameControl.control.difficulty = 0;
        easy.image.color = new Color32(150, 150, 150, 255);
        hard.image.color = new Color32(130, 130, 130, 255);
    }

    public void SelectLevel()
    {
        GameControl.control.Save();
        SceneManager.LoadScene("Level_Selector");
    }

    public void Exit()
    {
        GameControl.control.Save();
        Application.Quit();
    }
}
