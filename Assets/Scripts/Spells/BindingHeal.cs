using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;
using System;

/// <summary>
/// A Heal that heals the target and the raider with the lowest hp that is not the target.
/// </summary>
public class BindingHeal : Spell
{
    private readonly string SPELLNAME = "Binding Heal";
    private readonly float COOLDOWN = 0f;
    private readonly float MANACOST = 25f;
    private readonly float CASTTIME = 2f;
    private readonly string CASTSOUNDNAME = "GreaterHealCast";
    private readonly string IMPACTSOUNDNAME = "FlashHealImpact";

    private float healAmount = 50f;
    private float numberJumps = 1f;

    /// <summary>
    /// Called when a cast is sucesfully finished. Heals the target and another raider.
    /// </summary>
    public override void OnCastSucess()
    {
        //heal target
        GetTarget().Heal(healAmount);

        //get all raider sorted by health
        List<IRaider> raiderDict = RaiderDB.GetInstance().GetAllRaidersSortedByHealth();

        for (int i = 0; i < numberJumps && i < raiderDict.Count(); i++)
        {
            raiderDict.Remove(GetTarget()); //remove original target
            raiderDict.First().Heal(healAmount); //heal lowest raider
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
