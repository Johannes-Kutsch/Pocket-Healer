using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

/// <summary>
/// Heals the current target.
/// </summary>
public class GreaterHeal : Spell
{
    private readonly string SPELLNAME = "Greater Heal";
    private readonly float COOLDOWN = 0f;
    private float manacost = 10f;
    private float castTime = 2f;
    private readonly string CASTSOUNDNAME = "GreaterHealCast";
    private readonly string IMPACTSOUNDNAME = "GreaterHealImpact";

    private float healAmount = 50f;

    /// <summary>
    /// Called on Awake
    /// </summary>
    private void Awake()
    {
        if (GameControl.control.talente[1])
        {
            castTime *= 0.75f;
        }
        if (GameControl.control.talente[4])
        {
            healAmount *= 0.75f;
        }
    }

    /// <summary>
    /// Called when a cast is sucesfully finished.
    /// Heals the target by healAmount.
    /// </summary>
    public override void OnCastSucess()
    {
        GetTarget().Heal(healAmount);
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
        return manacost;
    }

    /// <summary>
    /// Gets the cast time.
    /// </summary>
    /// <returns>
    /// the cast time
    /// </returns>
    public override float GetCastTime()
    {
        return castTime;
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