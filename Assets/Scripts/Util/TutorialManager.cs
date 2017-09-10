using UnityEngine;
using System.Collections;
using System.Linq;

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
    /// </summary>
    void Update()
    {
        if (Gamestate.gamestate.GetTarget() != null && !buttonThreeClicked && buttonTwoClicked)
        {
            buttonThreeClicked = true;
            ButtonThree();
        } else if(Gamestate.gamestate.GetGcdBar().GetIsInGcd() && !buttonFourClicked && buttonThreeClicked)
        {
            Gamestate.gamestate.paused = false;
            buttonFourClicked = true;
            ButtonFour();
        } 
    }

    public void ButtonOne()
    {
        popupOne.SetActive(false);
        popupTwo.SetActive(true);
    }
    public void ButtonTwo()
    {
        buttonTwoClicked = true;
        popupTwo.SetActive(false);
        popupThree.SetActive(true);
        RaiderDB.GetInstance().GetAllTanks().First().Damage(40);
    }
    public void ButtonThree()
    {
        popupThree.SetActive(false);
        popupFour.SetActive(true);
        Gamestate.gamestate.GetCastBar().SetCasting(false);
    }
    public void ButtonFour()
    {
        popupFour.SetActive(false);
    }
}
