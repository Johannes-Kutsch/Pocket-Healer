using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// Loads the images for spells select by the player during spellselect.
/// </summary>
public class SpellBarManagerSelect : MonoBehaviour {
    public GameObject buttonOne;
    public GameObject buttonTwo;
    public GameObject buttonThree;
    public GameObject buttonFour;

    /// <summary>
    /// Called on Start, updates all buttonimages.
    /// </summary>
    void Start()
    {
        UpdateAllButtons();
    }

    /// <summary>
    /// Called when a button in the Spellbar is clicked (i.e. a new spell is selected), updates all buttonimages.
    /// </summary>
    public void UpdateAllButtons()
    {
        UpdateButton(buttonOne, GameControl.control.spellId[0]);
        UpdateButton(buttonTwo, GameControl.control.spellId[1]);
        UpdateButton(buttonThree, GameControl.control.spellId[2]);
        UpdateSpecialButton(buttonFour, GameControl.control.spellId[3]);
    }

    /// <summary>
    /// Updates the spellimage for a "normal" button.
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
    /// Updates the spellimage for a "special" button.
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
    /// Loads the "Renew" image for a button.
    /// </summary>
    /// <param name="button">The button.</param>
    private void AddSpellOne(GameObject button)
    {
        button.GetComponent<MeshRenderer>().material = Resources.Load("Renew", typeof(Material)) as Material;
    }

    /// <summary>
    /// Loads the "GreaterHeal" image for a button.
    /// </summary>
    /// <param name="button">The button.</param>
    private void AddSpellTwo(GameObject button)
    {
        button.GetComponent<MeshRenderer>().material = Resources.Load("GreaterHeal", typeof(Material)) as Material;
    }

    /// <summary>
    /// Loads the "CircleOfHealing" image for a button.
    /// </summary>
    /// <param name="button">The button.</param>
    private void AddSpellThree(GameObject button)
    {
        button.GetComponent<MeshRenderer>().material = Resources.Load("CircleOfHealing", typeof(Material)) as Material;
    }

    /// <summary>
    /// Loads the "PrayerOfMending" image for a button.
    /// </summary>
    /// <param name="button">The button.</param>
    private void AddSpellFour(GameObject button)
    {
        button.GetComponent<MeshRenderer>().material = Resources.Load("PrayerOfMending", typeof(Material)) as Material;
    }

    /// <summary>
    /// Loads the "HealingWell" image for a button.
    /// </summary>
    /// <param name="button">The button.</param>
    private void AddSpellFive(GameObject button)
    {
        button.GetComponent<MeshRenderer>().material = Resources.Load("HealingWell", typeof(Material)) as Material;
    }

    /// <summary>
    /// Loads the "Flamme" image for a button.
    /// </summary>
    /// <param name="button">The button.</param>
    private void AddSpellSix(GameObject button)
    {
        button.GetComponent<MeshRenderer>().material = Resources.Load("Flamme", typeof(Material)) as Material;
    }

    /// <summary>
    /// Loads the "Hymne" image for a button.
    /// </summary>
    /// <param name="button">The button.</param>
    private void AddSpellSeven(GameObject button)
    {
        button.GetComponent<MeshRenderer>().material = Resources.Load("Hymne", typeof(Material)) as Material;
    }

    /// <summary>
    /// Loads the "Dispell" image for a button.
    /// </summary>
    /// <param name="button">The button.</param>
    private void AddSpellEight(GameObject button)
    {
        button.GetComponent<MeshRenderer>().material = Resources.Load("Dispell", typeof(Material)) as Material;
    }

    /// <summary>
    /// Loads the "FlashHeal" image for a button.
    /// </summary>
    /// <param name="button">The button.</param>
    private void AddSpellNine(GameObject button)
    {
        button.GetComponent<MeshRenderer>().material = Resources.Load("FlashHeal", typeof(Material)) as Material;
    }

    /// <summary>
    /// Loads the "Shield" image for a button.
    /// </summary>
    /// <param name="button">The button.</param>
    private void AddSpellTen(GameObject button)
    {
        button.GetComponent<MeshRenderer>().material = Resources.Load("Shield", typeof(Material)) as Material;
    }

    /// <summary>
    /// Loads the "BindingHeal" image for a button.
    /// </summary>
    /// <param name="button">The button.</param>
    private void AddSpellEleven(GameObject button)
    {
        button.GetComponent<MeshRenderer>().material = Resources.Load("BindingHeal", typeof(Material)) as Material;
    }

    /// <summary>
    /// Loads the "Schutzgeist" image for a button.
    /// </summary>
    /// <param name="button">The button.</param>
    private void AddSpellTwelve(GameObject button)
    {
        button.GetComponent<MeshRenderer>().material = Resources.Load("Schutzgeist", typeof(Material)) as Material;
    }

    /// <summary>
    /// Loads the "Empty_Skill" image for a button.
    /// </summary>
    /// <param name="button">The button.</param>
    private void AddSpellDefault(GameObject button)
    {
        button.GetComponent<MeshRenderer>().material = Resources.Load("Empty_Skill", typeof(Material)) as Material;
    }

    /// <summary>
    /// Loads the "Empty_Ultimate" image for a button.
    /// </summary>
    /// <param name="button">The button.</param>
    private void AddSpellDefaultSpecial(GameObject button)
    {
        button.GetComponent<MeshRenderer>().material = Resources.Load("Empty_Ultimate", typeof(Material)) as Material;
    }
}

