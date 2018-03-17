using UnityEngine;
using System.Collections;

/// <summary>
/// Loads the images and scripts for spells used during normal levels.
/// </summary>
public class SpellBarManagerOne : MonoBehaviour {
    public GameObject buttonOne;
    public GameObject buttonTwo;
    public GameObject buttonThree;
    public GameObject buttonFour;
    public GameObject passiveButton;
    public Spell spellOne;
    public Spell spellTwo;
    public Spell spellThree;
    public Spell spellFour;

    /// <summary>
    /// Called on Start, updates all buttons.
    /// </summary>
    void Start()
    {
        UpdateButtons();
        UpdateSpells();
    }

    /// <summary>
    /// Called with every update. Checks if a key corresponding to a spell is pressed.
    /// </summary>
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            spellOne.StartCast();
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            spellTwo.StartCast();
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            spellThree.StartCast();
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            spellFour.StartCast();
        }
    }

    /// <summary>
    /// Updates all buttonimages and attaches the spellscript to these buttons.
    /// </summary>
    public void UpdateButtons()
    {
        UpdateButton(buttonOne, GameControl.control.spellId[0]);
        UpdateButton(buttonTwo, GameControl.control.spellId[1]);
        UpdateButton(buttonThree, GameControl.control.spellId[2]);
        UpdateSpecialButton(buttonFour, GameControl.control.spellId[3]);
        UpdatePassiveButton(passiveButton);
    }

    /// <summary>
    /// Gets the spellscript of each button and asigns it to a variable
    /// </summary>
    public void UpdateSpells()
    {
        spellOne = buttonOne.GetComponent<Spell>();
        spellTwo = buttonTwo.GetComponent<Spell>();
        spellThree = buttonThree.GetComponent<Spell>();
        spellFour = buttonFour.GetComponent<Spell>();
    }

    /// <summary>
    /// Disables all buttons.
    /// </summary>
    public void DisableButtons()
    {
        buttonOne.SetActive(false);
        buttonTwo.SetActive(false);
        buttonThree.SetActive(false);
        buttonFour.SetActive(false);
    }

    /// <summary>
    /// Updates the spellimage and script for a "normal" button.
    /// </summary>
    /// <param name="button">The button.</param>
    /// <param name="spellId">The spell id.</param>
    public void UpdateButton(GameObject button, int spellId)
    {
        switch (spellId)
        {
            case 1:
                AddSpellOne(button);
                break;
            case 2:
                AddSpellTwo(button);
                break;
            case 3:
                AddSpellThree(button);
                break;
            case 4:
                AddSpellFour(button);
                break;
            case 5:
                AddSpellFive(button);
                break;
            case 6:
                AddSpellSix(button);
                break;
            case 8:
                AddSpellEight(button);
                break;
            case 9:
                AddSpellNine(button);
                break;
            case 10:
                AddSpellTen(button);
                break;
            case 11:
                AddSpellEleven(button);
                break;
            default:
                AddSpellDefault(button);
                break;
        }
    }

    /// <summary>
    /// Updates the spellimage and script for a "special" button.
    /// </summary>
    /// <param name="button">The button.</param>
    /// <param name="spellId">The spell id.</param>
    public void UpdateSpecialButton(GameObject button, int spellId)
    {
        switch (spellId)
        {
            case 7:
                AddSpellSeven(button);
                break;
            case 12:
                AddSpellTwelve(button);
                break;
            default:
                AddSpellDefaultSpecial(button);
                break;
        }
    }

    /// <summary>
    /// Activates a button and attaches the "Renew" image and script to it.
    /// </summary>
    /// <param name="button">The button.</param>
    private void AddSpellOne(GameObject button)
    {
        button.SetActive(true);
        button.GetComponent<MeshRenderer>().material = Resources.Load("Renew", typeof(Material)) as Material;
        button.AddComponent<Renew>();
    }

    /// <summary>
    /// Activates a button and attaches the "GreaterHeal" image and script to it.
    /// </summary>
    /// <param name="button">The button.</param>
    private void AddSpellTwo(GameObject button)
    {
        button.SetActive(true);
        button.GetComponent<MeshRenderer>().material = Resources.Load("GreaterHeal", typeof(Material)) as Material;
        button.AddComponent<GreaterHeal>();
    }

    /// <summary>
    /// Activates a button and attaches the "CircleOfHealing" image and script to it.
    /// </summary>
    /// <param name="button">The button.</param>
    private void AddSpellThree(GameObject button)
    {
        button.SetActive(true);
        button.GetComponent<MeshRenderer>().material = Resources.Load("CircleOfHealing", typeof(Material)) as Material;
        button.AddComponent<CircleOfHealing>();
    }

    /// <summary>
    /// Activates a button and attaches the "PrayerOfMending" image and script to it.
    /// </summary>
    /// <param name="button">The button.</param>
    private void AddSpellFour(GameObject button)
    {
        button.SetActive(true);
        button.GetComponent<MeshRenderer>().material = Resources.Load("PrayerOfMending", typeof(Material)) as Material;
        button.AddComponent<Prayer>();
    }

    /// <summary>
    /// Activates a button and attaches the "HealingWell" image and script to it.
    /// </summary>
    /// <param name="button">The button.</param>
    private void AddSpellFive(GameObject button)
    {
        button.SetActive(true);
        button.GetComponent<MeshRenderer>().material = Resources.Load("HealingWell", typeof(Material)) as Material;
        button.AddComponent<Well>();
    }

    /// <summary>
    /// Activates a button and attaches the "Flamme" image and script to it.
    /// </summary>
    /// <param name="button">The button.</param>
    private void AddSpellSix(GameObject button)
    {
        button.SetActive(true);
        button.GetComponent<MeshRenderer>().material = Resources.Load("Flamme", typeof(Material)) as Material;
        button.AddComponent<Flamme>();
    }

    /// <summary>
    /// Activates a button and attaches the "Hymne" image and script to it.
    /// </summary>
    /// <param name="button">The button.</param>
    private void AddSpellSeven(GameObject button)
    {
        button.SetActive(true);
        button.GetComponent<MeshRenderer>().material = Resources.Load("Hymne", typeof(Material)) as Material;
        button.AddComponent<Hymn>();
    }

    /// <summary>
    /// Activates a button and attaches the "Dispell" image and script to it.
    /// </summary>
    /// <param name="button">The button.</param>
    private void AddSpellEight(GameObject button)
    {
        button.SetActive(true);
        button.GetComponent<MeshRenderer>().material = Resources.Load("Dispell", typeof(Material)) as Material;
        button.AddComponent<Dispell>();
    }

    /// <summary>
    /// Activates a button and attaches the "FlashHeal" image and script to it.
    /// </summary>
    /// <param name="button">The button.</param>
    private void AddSpellNine(GameObject button)
    {
        button.SetActive(true);
        button.GetComponent<MeshRenderer>().material = Resources.Load("FlashHeal", typeof(Material)) as Material;
        button.AddComponent<FlashHeal>();
    }

    /// <summary>
    /// Activates a button and attaches the "Shield" image and script to it.
    /// </summary>
    /// <param name="button">The button.</param>
    private void AddSpellTen(GameObject button)
    {
        button.SetActive(true);
        button.GetComponent<MeshRenderer>().material = Resources.Load("Shield", typeof(Material)) as Material;
        button.AddComponent<Shield>();
    }

    /// <summary>
    /// Activates a button and attaches the "BindingHeal" image and script to it.
    /// </summary>
    /// <param name="button">The button.</param>
    private void AddSpellEleven(GameObject button)
    {
        button.SetActive(true);
        button.GetComponent<MeshRenderer>().material = Resources.Load("BindingHeal", typeof(Material)) as Material;
        button.AddComponent<BindingHeal>();
    }

    /// <summary>
    /// Activates a button and attaches the "Schutzgeist" image and script to it.
    /// </summary>
    /// <param name="button">The button.</param>
    private void AddSpellTwelve(GameObject button)
    {
        button.SetActive(true);
        button.GetComponent<MeshRenderer>().material = Resources.Load("Schutzgeist", typeof(Material)) as Material;
        button.AddComponent<GuardianSpirit>();
    }

    /// <summary>
    /// Activates a button and attaches the "Empty_Skill" image to it.
    /// </summary>
    /// <param name="button">The button.</param>
    private void AddSpellDefault(GameObject button)
    {
        button.SetActive(true);
        button.GetComponent<MeshRenderer>().material = Resources.Load("Empty_Skill", typeof(Material)) as Material;
    }

    /// <summary>
    /// Activates a button and attaches the "Empty_Ultimate" image to it.
    /// </summary>
    /// <param name="button">The button.</param>
    private void AddSpellDefaultSpecial(GameObject button)
    {
        button.SetActive(true);
        button.GetComponent<MeshRenderer>().material = Resources.Load("Empty_Ultimate", typeof(Material)) as Material;
    }

    /// <summary>
    /// Activates the talent button and attaches the "correct" image and script to it.
    /// </summary>
    /// <param name="button">The button.</param>
    public void UpdatePassiveButton(GameObject button)
    {
        if (GameControl.control.talente[9])
        {
            button.SetActive(true);
            button.GetComponent<MeshRenderer>().material = Resources.Load("Flamme", typeof(Material)) as Material;
            button.AddComponent<Flamme>();
        }
        else if (GameControl.control.talente[10])
        {
            button.SetActive(true);
            button.GetComponent<MeshRenderer>().material = Resources.Load("CloudburstTotem", typeof(Material)) as Material;
            button.AddComponent<Cloudburst>();
        }
        else if (GameControl.control.talente[11])
        {
            button.SetActive(true);
            button.GetComponent<MeshRenderer>().material = Resources.Load("HealingWell", typeof(Material)) as Material;
            button.AddComponent<Well>();
        }
    }
}
