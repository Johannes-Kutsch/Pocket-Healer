using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

/// <summary>
/// Main controllelement for the gameflow.
/// Manages mana and the current target.
/// Manages Popups when winning/losing a level ans Scentransitions during a level.
/// Can (sort of) pause the game (during tutorial).
/// </summary>
public class Gamestate : MonoBehaviour {
    public static Gamestate gamestate;
    public GameObject Popup;

    private Raider target;
    private CastBar castBar;
    private ManaBar manaBar;
    private GcdBar gcdBar;
    private Boss Boss;
    private List<Spell> spellDict = new List<Spell>(); //List containing all currently used Spells
    private bool finished = false; //true if the current boss is dead
    private bool paused; //Stops boss autoattacks and raider autoattacks, does currently not stop boss abilitys
    private float maxMana = 1000f;
    private float currentMana;

    /// <summary>
    /// Called on awake.
    /// </summary>
    void Awake()
    {
        if (gamestate == null) //there can only be one gamestate
        {
            gamestate = this;
        }
        else if (gamestate != this)
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Called on start.
    /// </summary>
    void Start()
    {
        if (GameControl.control.talente[19]) //Mana increase Talent
            maxMana *= 1.1f;

        currentMana = maxMana;

        if (SceneManager.GetActiveScene().name == "Tutorial") //Tutorial is paused in the beginning
        {
            paused = true;
            castBar.disableCasting(true);
        }
    }

    /// <summary>
    /// Called with every update.
    /// Checks if the current level is lost/won.
    /// Detects when the Escape/Back button is pressed.
    /// </summary>
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "Tutorial") //slightly different behaviour for the tutorial
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                SceneManager.LoadScene("MainMenu");
            }
            else if (RaiderDB.GetInstance().GetAllRaiders().Count == 0 && !finished)
            {
                Popup.GetComponentInChildren<Text>().text = "Your group was destroyed before you could kill " + Boss.name + ". Try again.";
                finished = true;
                Destroy(Boss.gameObject);
                FindObjectOfType<SpellBarManagerTutorial>().DisableButtons();
                Popup.SetActive(true);
            }
            else if (Boss.GetCurrentHp() <= 0 && !finished)
            {
                string textPopup = "You killed " + Boss.name;
                Popup.GetComponentInChildren<Text>().text = textPopup;
                finished = true;
                Destroy(Boss.gameObject);
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
            else if (RaiderDB.GetInstance().GetAllRaiders().Count == 0 && !finished)
            {
                Popup.GetComponentInChildren<Text>().text = "Your group was destroyed before you could kill " + Boss.name + ". Try again.";
                finished = true;
                Destroy(Boss.gameObject);
                FindObjectOfType<SpellBarManager>().DisableButtons();
                Popup.SetActive(true);
            }
            else if (Boss.GetCurrentHp() <= 0 && !finished)
            {
                OnBossKill();
            }
        }
    }

    /// <summary>
    /// Called when the continue button is pressed after a level is won.
    /// </summary>
    public void ContinueButton()
    {
        GameControl.control.Save();
        SceneManager.LoadScene("Skill_Selector");
    }

    /// <summary>
    /// Gets called when a Boss is killed.
    /// Responsible for the Message displayed after killing a Boss, destroying the Boss, increasing the maxlevel, disabling the spellbar...
    /// </summary>
    public void OnBossKill()
    {
        string textPopup = "You killed " + Boss.name + ".";
        if (GameControl.control.currentLevelId == 2 || GameControl.control.currentLevelId == 4 || GameControl.control.currentLevelId == 7 || GameControl.control.currentLevelId == 9)
        {
            textPopup += " You unlocked a new row of talents.";
        }
        if (GameControl.control.currentLevelId == 1 || GameControl.control.currentLevelId == 2 || GameControl.control.currentLevelId == 3 || GameControl.control.currentLevelId == 4 || GameControl.control.currentLevelId == 5 || GameControl.control.currentLevelId == 6 || GameControl.control.currentLevelId == 8 || GameControl.control.currentLevelId == 10)
        {
            textPopup += " You unlocked a new spell.";
        }
        if (GameControl.control.currentLevelId == 11)
        {
            textPopup += " This was the last boss. The world is now save again.";
        }
        GameControl.control.LevelDone();
        Popup.GetComponentInChildren<Text>().text = textPopup;
        finished = true;
        Destroy(Boss.gameObject);
        FindObjectOfType<SpellBarManager>().DisableButtons();
        Popup.SetActive(true);
    }

    /// <summary>
    /// Increases the currentMana.
    /// </summary>
    /// <param name="mana">The mana.</param>
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
        manaBar.SetManaBar(maxMana, currentMana);
    }

    /// <summary>
    /// Decreases the currentMana.
    /// </summary>
    /// <param name="mana">The mana.</param>
    /// <returns>true if mana >= currentMana (i.e. there is enough currentMana to cast the spell), false if not</returns>
    public bool DecreaseMana(float mana)
    {
        if (mana <= currentMana)
        {
            currentMana -= mana;
            manaBar.SetManaBar(maxMana, currentMana);
            return true;
        }
        return false;
    }

    /// <summary>
    /// Sets the currentMana.
    /// </summary>
    /// <param name="mana">The mana.</param>
    public void SetMana(float mana)
    {
        currentMana = mana;
        manaBar.SetManaBar(maxMana, currentMana);
    }

    /// <summary>
    /// Gets the currentMana.
    /// </summary>
    /// <returns></returns>
    public float getMana()
    {
        return currentMana;
    }

    /// <summary>
    /// Sets the target.
    /// </summary>
    /// <param name="target">The target.</param>
    public void SetTarget(Raider target)
    {
        this.target = target;
    }

    /// <summary>
    /// Gets the target.
    /// </summary>
    /// <returns>the target</returns>
    public Raider GetTarget()
    {
        return target;
    }

    /// <summary>
    /// Determines whether this instance has target.
    /// </summary>
    /// <returns>
    ///   <c>true</c> if this instance has a target; otherwise, <c>false</c>.
    /// </returns>
    public bool HasTarget()
    {
        return target != null;
    }

    /// <summary>
    /// Sets the boss.
    /// </summary>
    /// <param name="boss">The boss.</param>
    public void SetBoss(Boss boss)
    {
        Boss = boss;
    }

    /// <summary>
    /// Gets the boss.
    /// </summary>
    /// <returns></returns>
    public Boss GetBoss()
    {
        return Boss;
    }

    /// <summary>
    /// Sets the castBar.
    /// </summary>
    /// <param name="Bar">The castBar.</param>
    public void SetCastBar(CastBar bar)
    {
        castBar = bar;
    }

    /// <summary>
    /// Gets the cast bar.
    /// </summary>
    /// <returns>the castBar</returns>
    public CastBar GetCastBar()
    {
        return castBar;
    }

    /// <summary>
    /// Sets the manaBar.
    /// </summary>
    /// <param name="bar">The bar.</param>
    public void SetManaBar(ManaBar bar)
    {
        manaBar = bar;
    }

    /// <summary>
    /// Gets the manaBar.
    /// </summary>
    /// <returns>the manaBar</returns>
    public ManaBar GetManaBar()
    {
        return manaBar;
    }

    /// <summary>
    /// Sets the gcdBar.
    /// </summary>
    /// <param name="Bar">The bar.</param>
    public void SetGcdBar(GcdBar bar)
    {
        gcdBar = bar;
    }

    /// <summary>
    /// Gets the gcdBar.
    /// </summary>
    /// <returns>the gcdBar</returns>
    public GcdBar GetGcdBar()
    {
        return gcdBar;
    }

    /// <summary>
    /// Adds a spell to the spellDict.
    /// </summary>
    /// <param name="spell">The spell.</param>
    public void AddSpell(Spell spell)
    {
        spellDict.Add(spell);
    }

    /// <summary>
    /// Get the spellDict.
    /// </summary>
    /// <returns>the spellDict</returns>
    public List<Spell> GetSpells()
    {
        return spellDict;
    }

    /// <summary>
    /// Gets the paused status. (Stops boss autoattacks and raider autoattacks, does currently not stop boss abilitys)
    /// </summary>
    /// <returns></returns>
    public bool GetPaused()
    {
        return paused;
    }

    /// <summary>
    /// Sets the paused status. (Stops boss autoattacks and raider autoattacks, does currently not stop boss abilitys)
    /// </summary>
    /// <param name="paused">if set to <c>true</c> [paused].</param>
    public void SetPaused(bool paused)
    {
        this.paused = paused;
    }
}
