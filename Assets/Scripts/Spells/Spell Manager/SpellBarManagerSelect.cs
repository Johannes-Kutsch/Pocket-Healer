using UnityEngine;
using System.Collections;
using System;

public class SpellBarManagerSelect : MonoBehaviour {
    public GameObject buttonOne;
    public GameObject buttonTwo;
    public GameObject buttonThree;
    public GameObject buttonFour;

    void Start()
    {
        UpdateAllButtons();
    }

    public void UpdateAllButtons()
    {
        UpdateButton(buttonOne, GameControl.control.spellId[0]);
        UpdateButton(buttonTwo, GameControl.control.spellId[1]);
        UpdateButton(buttonThree, GameControl.control.spellId[2]);
        UpdateSpecialButton(buttonFour, GameControl.control.spellId[3]);
    }

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

    private void AddSpellOne(GameObject button)
    {
        button.GetComponent<MeshRenderer>().material = Resources.Load("Renew", typeof(Material)) as Material;
    }

    private void AddSpellTwo(GameObject button)
    {
        button.GetComponent<MeshRenderer>().material = Resources.Load("GreaterHeal", typeof(Material)) as Material;
    }

    private void AddSpellThree(GameObject button)
    {
        button.GetComponent<MeshRenderer>().material = Resources.Load("CircleOfHealing", typeof(Material)) as Material;
    }

    private void AddSpellFour(GameObject button)
    {
        button.GetComponent<MeshRenderer>().material = Resources.Load("PrayerOfMending", typeof(Material)) as Material;
    }

    private void AddSpellFive(GameObject button)
    {
        button.GetComponent<MeshRenderer>().material = Resources.Load("HealingWell", typeof(Material)) as Material;
    }

    private void AddSpellSix(GameObject button)
    {
        button.GetComponent<MeshRenderer>().material = Resources.Load("Flamme", typeof(Material)) as Material;
    }

    private void AddSpellSeven(GameObject button)
    {
        button.GetComponent<MeshRenderer>().material = Resources.Load("Hymne", typeof(Material)) as Material;
    }

    private void AddSpellEight(GameObject button)
    {
        button.GetComponent<MeshRenderer>().material = Resources.Load("Dispell", typeof(Material)) as Material;
    }

    private void AddSpellNine(GameObject button)
    {
        button.GetComponent<MeshRenderer>().material = Resources.Load("FlashHeal", typeof(Material)) as Material;
    }

    private void AddSpellTen(GameObject button)
    {
        button.GetComponent<MeshRenderer>().material = Resources.Load("Shield", typeof(Material)) as Material;
    }

    private void AddSpellEleven(GameObject button)
    {
        button.GetComponent<MeshRenderer>().material = Resources.Load("BindingHeal", typeof(Material)) as Material;
    }

    private void AddSpellTwelve(GameObject button)
    {
        button.GetComponent<MeshRenderer>().material = Resources.Load("Schutzgeist", typeof(Material)) as Material;
    }

    private void AddSpellDefault(GameObject button)
    {
        button.GetComponent<MeshRenderer>().material = Resources.Load("Empty_Skill", typeof(Material)) as Material;
    }

    private void AddSpellDefaultSpecial(GameObject button)
    {
        button.GetComponent<MeshRenderer>().material = Resources.Load("Empty_Ultimate", typeof(Material)) as Material;
    }
}

