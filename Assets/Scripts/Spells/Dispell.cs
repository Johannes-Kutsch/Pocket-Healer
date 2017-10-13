using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

/// <summary>
/// Removes a dispellable debuff from a friendly target.
/// </summary>
public class Dispell : Spell {
    private readonly string SPELLNAME = "Dispell";
    private readonly float MANACOST = 10f;
    private readonly float CASTTIME = 0f;
    private readonly float COOLDOWN = 0f;
    private readonly string CASTSOUNDNAME = null;
    private readonly string IMPACTSOUNDNAME = "DispellCast";

    /// <summary>
    /// Called when a cast is sucesfully finished. Removes all dispellable debuffs from the target.
    /// </summary>
    public override void OnCastSucess()
    {
        BuffManager manager = GetTarget().GetGameObject().GetComponent<BuffManager>();
        List<IBuff> buffDict = manager.GetAllBuffsSortetByDuration(); //get all buffs

        foreach (IBuff buff in buffDict)
        {
            if (buff.IsDispellable())
            {
                buff.Destroy();//remove buff if it is dispellable
            }
            else if (buff.IsBuff() && GameControl.control.talente[6])
            {
                buff.Reset(); //reset buff if the "dispell reset" talent is picked
            }
        }
    }

    /// <summary>
    /// Gets the spellname.
    /// </summary>
    /// <returns>
    /// the spellname
    /// </returns>
    public override string GetSpellname()
    {
        return SPELLNAME;
    }

    /// <summary>
    /// Gets the cooldown.
    /// </summary>
    /// <returns>
    /// the cooldown
    /// </returns>
    public override float GetCooldown()
    {
        return COOLDOWN;
    }

    /// <summary>
    /// Gets the manacost.
    /// </summary>
    /// <returns>
    /// the manacost
    /// </returns>
    public override float GetManacost()
    {
        return MANACOST;
    }

    /// <summary>
    /// Gets the cast time.
    /// </summary>
    /// <returns>
    /// the cast time
    /// </returns>
    public override float GetCastTime()
    {
        return CASTTIME;
    }

    /// <summary>
    /// Gets the name of the cast sound.
    /// </summary>
    /// <returns>
    /// the name of the cast sound
    /// </returns>
    public override string GetCastSoundName()
    {
        return CASTSOUNDNAME;
    }

    /// <summary>
    /// Gets the name of the impact sound.
    /// </summary>
    /// <returns>
    /// the name of the impact sound
    /// </returns>
    public override string GetImpactSoundName()
    {
        return IMPACTSOUNDNAME;
    }
}
