using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

/// <summary>
/// Applies the Prayer of Mending Buff, which heals the target when it takes damage and jumps to another raidmember.
/// </summary>
public class Prayer : Spell {
    private readonly string SPELLNAME = "Prayer of Mending";
    private readonly float MANACOST = 30f;
    private readonly float CASTTIME = 0f;
    private readonly float COOLDOWN = 10f;
    private readonly string CASTSOUNDNAME = null;
    private readonly string IMPACTSOUNDNAME = "PrayerCast";

    /// <summary>
    /// Called when a cast is sucesfully finished. Applies the PrayerBuff to the target.
    /// </summary>
    public override void OnCastSucess()
    {
        Raider target = GetTarget();

        if (!target.GetGameObject().GetComponent<PrayerBuff>())
        {
            target.GetGameObject().AddComponent<PrayerBuff>();
        }
        else
        {
            target.GetGameObject().GetComponent<PrayerBuff>().Reset();
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
