using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// A buff that heals the raider with every tick.
/// </summary>
public class HymnBuff : BuffTicking
{
    private readonly float DURATION = 10f;
    private readonly string MATERIALNAME = "Hymne_Buff";
    private readonly int TICKS = 5;

    private float healPerTick = 5f;

    /// <summary>
    /// Called with every tick of the buff. Only used when ticks is greater than 1.
    /// </summary>
    public override void OnTick()
    {
        GetRaider().Heal(healPerTick);
    }

    /// <summary>
    /// Determines whether this instance is a buff or a debuff.
    /// </summary>
    /// <returns>
    ///   <c>true</c> if this instance is a buff; if this instances is a debuff, <c>false</c>.
    /// </returns>
    public override bool IsBuff()
    {
        return true;
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
    /// Gets the number of ticks.
    /// </summary>
    /// <returns></returns>
    public override int GetNumberTicks()
    {
        return TICKS;
    }
}
