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

    /// <summary>
    /// Called on awake.
    /// Checks if talents are picked that modify the skill.
    /// Set variables in base class.
    /// </summary>
    void Awake()
    {
        base.impactSound = Resources.Load("DispellCast", typeof(AudioClip)) as AudioClip;
        base.cooldown = COOLDOWN;
        base.manaCost = MANACOST;
        base.castTime = CASTTIME;
        base.spellName = SPELLNAME;
    }

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
                manager.DeregisterBuff(buff); //remove buff if it is dispellable
                buff.Destroy();
            }
            else if (buff.IsBuff() && GameControl.control.talente[6])
            {
                buff.Reset(); //reset buff if the "dispell reset" talent is picked
            }
        }
    }
}
