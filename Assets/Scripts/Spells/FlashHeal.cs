using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

/// <summary>
/// Heals the current target.
/// </summary>
public class FlashHeal : Spell
{
    private readonly string SPELLNAME = "Flash Heal";
    private readonly float COOLDOWN = 0f;
    private readonly float MANACOST = 20f;
    private readonly float CASTTIME = 1f;
    private readonly string CASTSOUNDNAME = "GreaterHealCast";
    private readonly string IMPACTSOUNDNAME = "FlashHealImpact";

    public readonly float HEALAMOUNT = 50f;

    /// <summary>
    /// Called when a cast is sucesfully finished.
    /// Heals the target by healAmount.
    /// </summary>
    public override void OnCastSucess()
    {
        GetTarget().Heal(HEALAMOUNT);
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
