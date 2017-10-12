using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// A buff that heals the raider with every tick.
/// </summary>
public class HymnBuff : Buff
{
    private readonly float Duration = 10f;
    private readonly int TICKS = 5;

    private float healPerTick = 5f;

    /// <summary>
    /// Called on awake.
    /// Set variables in base class.
    /// </summary>
    void Awake()
    {
        base.image = Resources.Load("Hymne_Buff", typeof(Material)) as Material;

        base.duration = Duration;
        base.ticks = TICKS;
    }

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
}
