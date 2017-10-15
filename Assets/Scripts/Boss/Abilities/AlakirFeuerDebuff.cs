using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// An undispellable, endless lasting debuff that deals damage in a set intervall.
/// </summary>
public class AlakirFeuerDebuff : BuffTicking
{
    private readonly string MATERIALNAME = "Feuer_Debuff";
    private readonly float DURATION = float.PositiveInfinity;
    private readonly float INTERVALLTICKS = 1.5f;

    private float damagePerTick = 20f;

    /// <summary>
    /// Called on awake.
    /// </summary>
    void Awake()
    {
        if (GameControl.control.difficulty == 0)
        {
            damagePerTick *= GameControl.control.easyMultiplier;
        }
    }

    /// <summary>
    /// Called with every tick of the buff.
    /// </summary>
    public override void OnTick()
    {
        GetRaider().Damage(damagePerTick);
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
