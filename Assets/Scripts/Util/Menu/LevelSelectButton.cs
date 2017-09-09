using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelSelectButton : MonoBehaviour {
    public int levelID;
    private string levelName;

    void Start() {
        if(GameControl.control.maxLevelIdUnlocked < levelID)
        {
            Destroy(gameObject);
        }
    }

    public void Click() {
        GameControl.control.currentLevelId = levelID;
        GameControl.control.Save();
        SceneManager.LoadScene("Skill_Selector");
    }

}
