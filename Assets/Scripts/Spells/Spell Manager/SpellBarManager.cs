using UnityEngine;
using System.Collections;

public class SpellBarManager : MonoBehaviour {
    public GameObject buttonOne;
    public GameObject buttonTwo;
    public GameObject buttonThree;
    public GameObject buttonFour;
    public GameObject passiveButton;

    void Start()
    {
        UpdateButtons();
    }

    public void UpdateButtons()
    {
        UpdateButton(buttonOne, GameControl.control.spellId[0]);
        UpdateButton(buttonTwo, GameControl.control.spellId[1]);
        UpdateButton(buttonThree, GameControl.control.spellId[2]);
        UpdateSpecialButton(buttonFour, GameControl.control.spellId[3]);
        UpdatePassiveButton(passiveButton);
    }

    public void DisableButtons()
    {
        buttonOne.SetActive(false);
        buttonTwo.SetActive(false);
        buttonThree.SetActive(false);
        buttonFour.SetActive(false);
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
        button.SetActive(true);
        button.GetComponent<MeshRenderer>().material = Resources.Load("Renew", typeof(Material)) as Material;
        button.AddComponent<Renew>();
    }

    private void AddSpellTwo(GameObject button)
    {
        button.SetActive(true);
        button.GetComponent<MeshRenderer>().material = Resources.Load("GreaterHeal", typeof(Material)) as Material;
        button.AddComponent<GroßeHeilung>();
    }

    private void AddSpellThree(GameObject button)
    {
        button.SetActive(true);
        button.GetComponent<MeshRenderer>().material = Resources.Load("CircleOfHealing", typeof(Material)) as Material;
        button.AddComponent<CircleOfHealing>();
    }

    private void AddSpellFour(GameObject button)
    {
        button.SetActive(true);
        button.GetComponent<MeshRenderer>().material = Resources.Load("PrayerOfMending", typeof(Material)) as Material;
        button.AddComponent<Prayer>();
    }

    private void AddSpellFive(GameObject button)
    {
        button.SetActive(true);
        button.GetComponent<MeshRenderer>().material = Resources.Load("HealingWell", typeof(Material)) as Material;
        button.AddComponent<Well>();
    }

    private void AddSpellSix(GameObject button)
    {
        button.SetActive(true);
        button.GetComponent<MeshRenderer>().material = Resources.Load("Flamme", typeof(Material)) as Material;
        button.AddComponent<Flamme>();
    }

    private void AddSpellSeven(GameObject button)
    {
        button.SetActive(true);
        button.GetComponent<MeshRenderer>().material = Resources.Load("Hymne", typeof(Material)) as Material;
        button.AddComponent<Hymne>();
    }

    private void AddSpellEight(GameObject button)
    {
        button.SetActive(true);
        button.GetComponent<MeshRenderer>().material = Resources.Load("Dispell", typeof(Material)) as Material;
        button.AddComponent<Dispell>();
    }

    private void AddSpellNine(GameObject button)
    {
        button.SetActive(true);
        button.GetComponent<MeshRenderer>().material = Resources.Load("FlashHeal", typeof(Material)) as Material;
        button.AddComponent<FlashHeal>();
    }

    private void AddSpellTen(GameObject button)
    {
        button.SetActive(true);
        button.GetComponent<MeshRenderer>().material = Resources.Load("Shield", typeof(Material)) as Material;
        button.AddComponent<Shield>();
    }

    private void AddSpellTwelve(GameObject button)
    {
        button.SetActive(true);
        button.GetComponent<MeshRenderer>().material = Resources.Load("Schutzgeist", typeof(Material)) as Material;
        button.AddComponent<Schutzgeist>();
    }

    private void AddSpellEleven(GameObject button)
    {
        button.SetActive(true);
        button.GetComponent<MeshRenderer>().material = Resources.Load("BindingHeal", typeof(Material)) as Material;
        button.AddComponent<BindingHeal>();
    }

    private void AddSpellDefault(GameObject button)
    {
        button.SetActive(true);
        button.GetComponent<MeshRenderer>().material = Resources.Load("Empty_Skill", typeof(Material)) as Material;
    }

    private void AddSpellDefaultSpecial(GameObject button)
    {
        button.SetActive(true);
        button.GetComponent<MeshRenderer>().material = Resources.Load("Empty_Ultimate", typeof(Material)) as Material;
    }

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
