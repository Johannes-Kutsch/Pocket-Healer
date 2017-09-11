using UnityEngine;
using System.Collections;

/// <summary>
/// Script attached to a "normal" spell button in the actionbar during talentselection.
/// </summary>
public class SelectButtonBottom : MonoBehaviour {
    public int buttonId;
    public SpellBarManagerSelect manager;
    bool change = true;

    /// <summary>
    /// Called when the spell button is clicked, if a normal skill is selected, set it as the new special skill.
    /// </summary>
    void OnMouseDown()
    {
        for(int i = 0; i <4; i++)
        {
            if(GameControl.control.spellId[i] == GameControl.control.selectedSpellId)
            {
                change = false; //the Spell is already in use
            }
        }
        if(GameControl.control.selectedSpellId == 7 || GameControl.control.selectedSpellId == 12)
        {
            change = false; //the spell is a special spell
        }
        else if(change == true)
        {
            GameControl.control.spellId[buttonId] = GameControl.control.selectedSpellId;
        }
        manager.UpdateAllButtons();
        change = true;
    }
}
