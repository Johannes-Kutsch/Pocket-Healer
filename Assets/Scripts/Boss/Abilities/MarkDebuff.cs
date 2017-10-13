using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// A Debuff that deals damage to a single raider. The damage is doubled after each tick, up to a maximum. Every time the Debuff ticks it also damages the Boss.
/// </summary>
public class MarkDebuff : BuffTicking
{
    private readonly string MATERIALNAME = "Marked_Debuff";
    private readonly float DURATION = 18f;
    private readonly float INTERVALLTICKS = 1f;

    private float damagePerTick = 2f;
    private float maxDamagePerTick = 32f;
    private float multiplier = 2f;
    private float bossDamage = 150f;
    private Boss boss;

    /// <summary>
    /// Called on awake.
    /// </summary>
    void Awake()
    {
        if (GameControl.control.difficulty == 0)
        {
            damagePerTick *= GameControl.control.easyMultiplyer;
            maxDamagePerTick *= GameControl.control.easyMultiplyer;
        }
    }

    /// <summary>
    /// Called when the buff finished initialisation.
    /// </summary>
    public override void OnStart()
    {
        boss = FindObjectOfType<Boss>();
    }

    /// <summary>
    /// Called with every tick of the buff.
    /// </summary>
    public override void OnTick()
    {
        if (raider != null)
        {
            raider.Damage(damagePerTick);
            boss.TakeDamage(bossDamage);
        }
        if (damagePerTick * multiplier > maxDamagePerTick)
        {
            damagePerTick = maxDamagePerTick;
        }
        else
        {
            bossDamage *= multiplier;
            damagePerTick *= multiplier;
        }
    }

    /// <summary>
    /// Determines whether this instance is dispellable.
    /// </summary>
    /// <returns>
    ///   <c>true</c> if this instance is dispellable; otherwise, <c>false</c>.
    /// </returns>
    public override bool IsDispellable()
    {
        return false;
    }

    /// <summary>
    /// Gets the number of ticks.
    /// </summary>
    /// <returns></returns>
    public override float GetIntervallTicks()
    {
        return INTERVALLTICKS;
    }

    /// <summary>
    /// Gets the material name.
    /// </summary>
    /// <returns>
    /// the material name
    /// </returns>
    public override string GetMaterialName()
    {
        return MATERIALNAME;
    }

    /// <summary>
    /// Gets the real duration (the time after which the debuff should be removed).
    /// </summary>
    /// <returns>
    /// the real duration
    /// </returns>
    public override float GetRealDuration()
    {
        return DURATION;
    }

    /// <summary>
    /// Determines whether this instance is a buff or a debuff.
    /// </summary>
    /// <returns>
    ///   <c>true</c> if this instance is a buff; if this instances is a debuff, <c>false</c>.
    /// </returns>
    public override bool IsBuff()
    {
        return false;
    }
}