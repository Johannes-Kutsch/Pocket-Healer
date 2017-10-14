using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

/// <summary>
/// Applies a buff that heals the target over time.
/// </summary>
public class Renew : Spell
{
    private readonly string SPELLNAME = "Renew";
    private readonly float MANACOST = 10f;
    private readonly float CASTTIME = 0f;
    private float cooldown = 3.5f;
    private readonly string CASTSOUNDNAME = null;
    private readonly string IMPACTSOUNDNAME = "RenewCast";

    public void Awake()
    {
        if (GameControl.control.talente[0])
            cooldown = 0f;
    }

    /// <summary>
    /// Called when a cast is sucesfully finished. Applies the RenewBuff to the target.
    /// </summary>
    public override void OnCastSucess()
    {
        IRaider target = GetTarget();

        if (!target.GetGameObject().GetComponent<RenewHot>())
        {
            target.GetGameObject().AddComponent<RenewHot>();
        }
        else
        {
            target.GetGameObject().GetComponent<RenewHot>().Reset();
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
        return cooldown;
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
