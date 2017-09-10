using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class Gamestate : MonoBehaviour {
    public static Gamestate gamestate;
    public IRaider target;
    private CastBar castBar;
    private ManaBar manaBar;
    private GcdBar gcdBar;
    private Boss currentBoss;
    private List<ISpell> spellDict = new List<ISpell>();
    public GameObject Popup;
    private bool finished = false;
    public bool paused;
    private float maxMana = 1000f;
    public float currentMana;

    void Awake()
    {
        if (gamestate == null)
        {
            gamestate = this;
        }
        else if (gamestate != this)
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        if (GameControl.control.talente[19])
            maxMana *= 1.1f;
        currentMana = maxMana;
        if (SceneManager.GetActiveScene().name == "Tutorial")
        {
            paused = true;
            castBar.disableCasting(true);
        }
    }

    public void ContinueButton()
    {
        GameControl.control.Save();
        SceneManager.LoadScene("Skill_Selector");
    }

    void FixedUpdate()
    {
        if (SceneManager.GetActiveScene().name == "Tutorial")
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                SceneManager.LoadScene("MainMenu");
            }
            else if (RaiderDB.GetInstance().GetAllRaider().Count == 0 && !finished)
            {
                Popup.GetComponentInChildren<Text>().text = "Deine Gruppe wurde vernichtet bevor du " + currentBoss.name + " töten konntest. Versuche es erneut.";
                finished = true;
                Destroy(currentBoss.gameObject);
                FindObjectOfType<SpellBarManagerTutorial>().DisableButtons();
                Popup.SetActive(true);
            }
            else if (currentBoss.GetCurrentHp() <= 0 && !finished)
            {
                string textPopup = "Du hast " + currentBoss.name + " getötet.";
                Popup.GetComponentInChildren<Text>().text = textPopup;
                finished = true;
                Destroy(currentBoss.gameObject);
                FindObjectOfType<SpellBarManagerTutorial>().DisableButtons();
                Popup.SetActive(true);
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                SceneManager.LoadScene("Skill_Selector");
            }
            else if (RaiderDB.GetInstance().GetAllRaider().Count == 0 && !finished)
            {
                Popup.GetComponentInChildren<Text>().text = "Deine Gruppe wurde vernichtet bevor du " + currentBoss.name + " töten konntest. Versuche es erneut.";
                finished = true;
                Destroy(currentBoss.gameObject);
                FindObjectOfType<SpellBarManager>().DisableButtons();
                Popup.SetActive(true);
            }
            else if (currentBoss.GetCurrentHp() <= 0 && !finished)
            {
                Skip();
            }
        }
    }

    public void Skip()
    {
        
        string textPopup = "Du hast " + currentBoss.name + " getötet.";
        if (GameControl.control.currentLevelId == 2 || GameControl.control.currentLevelId == 4 || GameControl.control.currentLevelId == 7 || GameControl.control.currentLevelId == 9)
        {
            textPopup += " Du hast eine neue Reihe Talente freigeschaltet.";
        }
        if (GameControl.control.currentLevelId == 1 || GameControl.control.currentLevelId == 2 || GameControl.control.currentLevelId == 3 || GameControl.control.currentLevelId == 4 || GameControl.control.currentLevelId == 5 || GameControl.control.currentLevelId == 6 || GameControl.control.currentLevelId == 8 || GameControl.control.currentLevelId == 10)
        {
            textPopup += " Du hast einen neuen Zauber freigeschaltet.";
        }
        if (GameControl.control.currentLevelId == 11)
        {
            textPopup += " Das war der letzte Boss, die Welt ist nun wieder sicher.";
        }
        GameControl.control.LevelDone();
        Popup.GetComponentInChildren<Text>().text = textPopup;
        finished = true;
        Destroy(currentBoss.gameObject);
        FindObjectOfType<SpellBarManager>().DisableButtons();
        Popup.SetActive(true);
    }

    public void IncreaseMana(float mana)
    {
        if (mana > maxMana - currentMana)
        {
            currentMana = maxMana;
        }
        else
        {
            currentMana += mana;
        }
        manaBar.setManaBar(maxMana, currentMana);
    }

    public bool DecreaseMana(float mana)
    {
        if (mana <= currentMana)
        {
            currentMana -= mana;
            manaBar.setManaBar(maxMana, currentMana);
            return true;
        }
        return false;
    }


    public IRaider GetTarget()
    {
        return target;
    }

    public bool HasTarget()
    {
        return target != null;
    }

    public void SetTarget(IRaider newTarget)
    {
        target = newTarget;
    }

    public void SetBoss(Boss boss)
    {
        currentBoss = boss;
    }

    public Boss GetBoss()
    {
        return currentBoss;
    }

    public void SetCastBar(CastBar Bar)
    {
        castBar = Bar;
    }

    public CastBar GetCastBar()
    {
        return castBar;
    }

    public void SetManaBar(ManaBar Bar)
    {
        manaBar = Bar;
    }

    public ManaBar GetManaBar()
    {
        return manaBar;
    }

    public void SetGcdBar(GcdBar Bar)
    {
        gcdBar = Bar;
    }

    public GcdBar GetGcdBar()
    {
        return gcdBar;
    }

    public void AddSpell(ISpell spell)
    {
        spellDict.Add(spell);
    }

    public List<ISpell> GetSpells()
    {
        return spellDict;
    } 
}
