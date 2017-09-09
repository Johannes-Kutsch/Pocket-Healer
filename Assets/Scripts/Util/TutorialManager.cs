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

    void Update()
    {
        if (Gamestate.gamestate.GetTarget() != null && !buttonThreeClicked && buttonTwoClicked)
        {
            buttonThreeClicked = true;
            ButtonThree();
        } else if(Gamestate.gamestate.GetGcdBar().GetGcd() && !buttonFourClicked && buttonThreeClicked)
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
        RaiderDB.GetInstance().GetAllTanks().First().ReduceHP(40);
    }
    public void ButtonThree()
    {
        popupThree.SetActive(false);
        popupFour.SetActive(true);
        Gamestate.gamestate.GetCastBar().SetzeCasting(false);
    }
    public void ButtonFour()
    {
        popupFour.SetActive(false);
    }
}
