using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

/// <summary>
/// Consistent across alls Scenes.
/// Has informations about choosen talents, choosen spells, unlocked levels, current settings...
/// </summary>
public class GameControl : MonoBehaviour {
    public static GameControl control;
    public int[] spellId = new int[4];
    public bool[] talente = new bool[22];
    public string currentLevel = "Fight_1";
    private static int maxLevelId = 11;
    public int maxLevelIdUnlocked = 1;
    public int currentLevelId = 1;
    public float soundMultiplier = 1;
    public AudioSource source;
    public int selectedSpellId = 1;
    public int difficulty = 1;
    public readonly float easyMultiplier = 0.75f; //damage multiplyer for easy setting


    /// <summary>
    /// Called on Awake.
    /// </summary>
    void Awake()
    {
        if (Application.platform.ToString() == "WindowsPlayer")
        {
            Screen.SetResolution(500, 800, false); //Workaorund for correct ratio on Windows Clients
        }

        if (control == null) //only 1 Gamecontrol
        {
            DontDestroyOnLoad(gameObject);
            control = this;
        }
        else if (control != this)
        {
            Destroy(gameObject);
        }

        source = GetComponent<AudioSource>();
        source.volume = soundMultiplier;
    }

    /// <summary>
    /// Called on Start.
    /// </summary>
    void Start()
    {
        Screen.orientation = ScreenOrientation.Portrait;
    }

    /// <summary>
    /// Resets the current gameprogress. (unlocked levels/skills/talents)
    /// </summary>
    public void ResetProgress()
    {
        maxLevelIdUnlocked = 1;
        currentLevelId = 1;
        SwitchCurrentLevel(currentLevelId);
        for(int i = 0; i < 22; i++)
        {
            talente[i] = false;
        }
        for (int i = 0; i < 4; i++)
        {
            spellId[i] = 0;
        }
    }

    /// <summary>
    /// Called when a level was sucessfully done. Changes the current and max levelid.
    /// </summary>
    public void LevelDone()
    {
        if(currentLevelId < maxLevelId)
        {
            currentLevelId += 1;
            if(currentLevelId > maxLevelIdUnlocked)
            {
                maxLevelIdUnlocked = currentLevelId;
            }
        }
        SwitchCurrentLevel(currentLevelId);
    }

    /// <summary>
    /// Switches the current level. (Used during levelselect)
    /// </summary>
    /// <param name="Id">The identifier of the new level.</param>
    public void SwitchCurrentLevel(int Id)
    {
        currentLevel = "Fight_" + Id;
    }

    /// <summary>
    /// Called on enable, loads the current playerdata.
    /// </summary>
    void OnEnable()
    {
        Load();
    }

    /// <summary>
    /// Called on disable, loads the current playerdata.
    /// </summary>
    void OnDisable()
    {
        Save();
    }

    /// <summary>
    /// Saves the current playerdata.
    /// </summary>
    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/playerInfo.dat");
        PlayerData data = new PlayerData();

        data.spellId = spellId;
        data.currentLevel = currentLevel;
        data.currentLevelId = currentLevelId;
        data.soundMultiplyer = soundMultiplier;
        data.maxLevelIdUnlocked = maxLevelIdUnlocked;
        data.talente = talente;
        data.difficulty = difficulty;


        bf.Serialize(file, data);
        file.Close();
    }

    /// <summary>
    /// Loads the current playerdata.
    /// </summary>
    public void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/playerInfo.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);
            PlayerData data = (PlayerData)bf.Deserialize(file);
            file.Close();

            spellId = data.spellId;
            currentLevel = data.currentLevel;
            currentLevelId = data.currentLevelId;
            soundMultiplier = data.soundMultiplyer;
            maxLevelIdUnlocked = data.maxLevelIdUnlocked;
            talente = data.talente;
            source.volume = soundMultiplier;
            difficulty = data.difficulty;
        }
    }
}

/// <summary>
/// Container for savegamedata.
/// </summary>
[Serializable]
class PlayerData
{
    public int[] spellId = new int[3];
    public string currentLevel;
    public int currentLevelId;
    public float soundMultiplyer;
    public int maxLevelIdUnlocked;
    public bool[] talente = new bool[22];
    public int difficulty;
}
