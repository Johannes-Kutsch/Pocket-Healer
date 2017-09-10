using UnityEngine;
using System.Collections;
using System.Linq;

/// <summary>
/// Manages the flow of the tutorial scene
/// </summary>
public class TutorialManager : MonoBehaviour {
    public GameObject popupOne;
    public GameObject popupTwo;
    public GameObject popupThree;
    public GameObject popupFour;
    public bool buttonTwoClicked = false;
    public bool buttonThreeClicked = false;
    public bool buttonFourClicked = false;

    /// <summary>
    /// Called on every Update.
    /// Checks if the contidtions for the 3rd and 4th tutorial popup are fulfilled.
    /// </summary>
    void Update()
    {
        if (Gamestate.gamestate.GetTarget() != null && !buttonThreeClicked && buttonTwoClicked)
        {
            ButtonThree();
        } else if(Gamestate.gamestate.GetGcdBar().GetIsInGcd() && !buttonFourClicked && buttonThreeClicked)
        {
            ButtonFour();
        } 
    }

    /// <summary>
    /// Gets called when popup button 1 is clicked.
    /// </summary>
    public void ButtonOne()
    {
        popupOne.SetActive(false);
        popupTwo.SetActive(true);
    }

    /// <summary>
    /// Gets called when popup button 2 is clicked.
    /// </summary>
    public void ButtonTwo()
    {
        buttonTwoClicked = true;
        popupTwo.SetActive(false);
        popupThree.SetActive(true);
        RaiderDB.GetInstance().GetAllTanks().First().Damage(40);
    }

    /// <summary>
    /// Gets called when the requierements for popup 3 are fulfilled.
    /// </summary>
    public void ButtonThree()
    {
        buttonThreeClicked = true;
        popupThree.SetActive(false);
        popupFour.SetActive(true);
        Gamestate.gamestate.GetCastBar().disableCasting(false);
    }

    /// <summary>
    /// Gets called when the requierements for popup 4 are fulfilled.
    /// </summary>
    public void ButtonFour()
    {
        buttonFourClicked = true;
        Gamestate.gamestate.SetPaused(false);
        popupFour.SetActive(false);
    }
}
