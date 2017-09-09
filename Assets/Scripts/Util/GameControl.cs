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
    public float easyMultiplyer = 0.75f;
    /*
    Spell ID's:
    1: Renew (1)
    2: Große Heilung (1)
    3: Circle of Healing (2)
    4: Prayer (9)
    7: Hymne der Hoffnung (3)
    8: Dispell (4)
    9: Flashheal (5)
    10: Shield (7)
    11: Binding Heal (11)
    12: Schutzgeist (6)
        */

    /*
    TALENTE:
        Ab Level 3
        0: Renew kein cd
        1: Greater Heal + 25% casttime
        2: Circle +2 Targets
        18: +5% Heal
        Ab Level 5:
        3: Renew jump wenn ausläuft
        4: Greater Heal +5 Mana/nurnoch 75% der healvalue
        5: Hymne hinterlässt hot der 25 in 10 sek heilt
        19: +10% Mana
        Ab Level 8:
        6: dispell resettet alle buffs auf ursprung
        7: Circle kein cd, 1sek castzeit
        8: Schutzgeist beschützt jeden, nicht nur ziel auf dem er ist
        20: 10% Heal auf Targets <= 30% Hp
        Ab Level 10 Passiv
        9: flamme
        10: healingburst totem
        11: brunnen
        Ab Level 12 
        12: 
        13: Schild heilt beim setzen
        14: Binding Heal drei targets
        15: Prayer Jump auslaufen

    Boss 1: Gnoll
    Boss 2: Rhino
    Boss 3: Drache
    Boss 4: Hexenmeister
    Boss 5: Schurke
    Boss 6: Krieger
    Boss 7: Steintyp
    Boss 8: Pilztyp
    Boss 9: Schurke
    Boss 10: Elementar
    Boss 11: Drache 
    Boss 12: Magier
    */


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
