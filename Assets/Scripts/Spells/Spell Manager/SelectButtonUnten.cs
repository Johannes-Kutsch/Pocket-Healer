using UnityEngine;
using System.Collections;

public class SelectButtonUnten : MonoBehaviour {
    public int buttonId;
    public SpellBarManagerSelect manager;
    bool change = true;

    void OnMouseDown()
    {
        for(int i = 0; i <4; i++)
        {
            if(GameControl.control.spellId[i] == GameControl.control.selectedSpellId)
            {
                change = false;
            }
        }
        if(GameControl.control.selectedSpellId == 7 || GameControl.control.selectedSpellId == 12)
        {
            change = false;
        }
        else if(change == true)
        {
            GameControl.control.spellId[buttonId] = GameControl.control.selectedSpellId;
        }
        manager.UpdateAllButtons();
        change = true;
    }
}
