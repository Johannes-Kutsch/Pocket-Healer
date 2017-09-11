using UnityEngine;
using System.Collections;

/// <summary>
/// Script attached to the "special" spell button in the actionbar during talentselection.
/// </summary>
public class SelectButtonSpecial : MonoBehaviour {
    public int buttonId;
    public SpellBarManagerSelect manager;

    /// <summary>
    /// Called when the spell button is clicked, if a special skill is selected, set it as the new special skill.
    /// </summary>
    void OnMouseDown()
    {
        if (GameControl.control.selectedSpellId == 7 || GameControl.control.selectedSpellId == 12)
        {
            GameControl.control.spellId[buttonId] = GameControl.control.selectedSpellId;
            manager.UpdateAllButtons();
        }
    }
}
