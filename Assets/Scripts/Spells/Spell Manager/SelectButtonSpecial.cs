using UnityEngine;
using System.Collections;

public class SelectButtonSpecial : MonoBehaviour {
    public int buttonId;
    public SpellBarManagerSelect manager;


    void OnMouseDown()
    {
        if (GameControl.control.selectedSpellId == 7 || GameControl.control.selectedSpellId == 12)
        {
            GameControl.control.spellId[buttonId] = GameControl.control.selectedSpellId;
            manager.UpdateAllButtons();
        }
    }
}
