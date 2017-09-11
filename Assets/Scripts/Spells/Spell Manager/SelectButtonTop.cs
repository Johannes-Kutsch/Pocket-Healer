using UnityEngine;
using System.Collections;

/// <summary>
/// Script attached to a spell button (the 10 buttons in the main screen) during talentselection.
/// </summary>
public class SelectButtonTop : MonoBehaviour {
    public int spellId;
    public int unlockedAt;

    /// <summary>
    /// Called on start, loads the "black_white" picture and destroys this script if the spell is not yet unlocked.
    /// </summary>
    void Start()
    {
        if(unlockedAt > GameControl.control.maxLevelIdUnlocked)
        {
            GetComponent<MeshRenderer>().material = Resources.Load("UnknownSkill_BW", typeof(Material)) as Material;
            Destroy(this);
        }
    }

    /// <summary>
    /// Called when the spell button is clicked, updates the tooltip and selects the talent.
    /// </summary>
    void OnMouseDown()
    {
        GameControl.control.selectedSpellId = spellId;
        Tooltips.tooltips.UpdateTooltip(spellId);
    }
}
