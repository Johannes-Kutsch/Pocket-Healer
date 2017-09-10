using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class GameControl : MonoBehaviour {
    public static GameControl control;
    public int[] spellId = new int[4];
    public bool[] talente = new bool[22];
    public string currentLevel = "Fight_1";
    private static int maxLevelId = 11;
    public int maxLevelIdUnlocked = 1;
    public int currentLevelId = 1;
    public float soundMultiplyer = 1;
    public AudioSource source;
    public int selectedSpellId = 1;
    public int difficulty = 1;
    public float easyMultiplyer = 0.75f; //damage Multiplyer for easy setting
    
    void Awake()
    {
        if (control == null)
        {
            DontDestroyOnLoad(gameObject);
            control = this;
        }
        else if (control != this)
        {
            Destroy(gameObject);
        }
        source = GetComponent<AudioSource>();
        source.volume = soundMultiplyer;
    }

    void Start()
    {
        Screen.orientation = ScreenOrientation.Portrait;
    }

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

    public void SwitchCurrentLevel(int Id)
    {
        switch (Id)
        {
            case 1:
                currentLevel = "Fight_1";
                break;
            case 2:
                currentLevel = "Fight_2";
                break;
            case 3:
                currentLevel = "Fight_3";
                break;
            case 4:
                currentLevel = "Fight_4";
                break;
            case 5:
                currentLevel = "Fight_5";
                break;
            case 6:
                currentLevel = "Fight_6";
                break;
            case 7:
                currentLevel = "Fight_7";
                break;
            case 8:
                currentLevel = "Fight_8";
                break;
            case 9:
                currentLevel = "Fight_9";
                break;
            case 10:
                currentLevel = "Fight_10";
                break;
            case 11:
                currentLevel = "Fight_11";
                break;
            case 12:
                currentLevel = "Fight_12";
                break;
        }
    }

    void OnEnable()
    {
        Load();
    } 

    void OnDisable()
    {
        Save();
    }  

    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/playerInfo.dat");
        PlayerData data = new PlayerData();

        data.spellId = spellId;
        data.currentLevel = currentLevel;
        data.currentLevelId = currentLevelId;
        data.soundMultiplyer = soundMultiplyer;
        data.maxLevelIdUnlocked = maxLevelIdUnlocked;
        data.talente = talente;
        data.difficulty = difficulty;


        bf.Serialize(file, data);
        file.Close();
    }

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
            soundMultiplyer = data.soundMultiplyer;
            maxLevelIdUnlocked = data.maxLevelIdUnlocked;
            talente = data.talente;
            source.volume = soundMultiplyer;
            difficulty = data.difficulty;
        }
    }
}

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
