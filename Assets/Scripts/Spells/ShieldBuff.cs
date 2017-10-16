using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Applies a Buff that absorbs damage.
/// </summary>
public class ShieldBuff : Buff
{
    public readonly float DURATION = 15f;
    private readonly string MATERIALNAME = "Shield_Buff";

    public float absorbAmount = 80f;

    /// <summary>
    /// Called on awake.
    /// Set variables in base class.
    /// </summary>
    void Awake()
    {
        base.resetable = true;
    }

    /// <summary>
    /// Called when the Reset Method is executet, independetly from the resetable variable.
    /// </summary>
    public override void OnReset()
    {
        absorbAmount = 80f;
    }

    /// <summary>
    /// Gets called when the raider the buff is attached to takes damage.
    /// The amount can be modivied here, i.e. if the buff decrases the damage taken by 20% we just return amount * 0.8.
    /// If the damage amount should not be modified we just return the original value.
    /// </summary>
    /// <param name="amount">the amount.</param>
    /// <returns>
    /// the new damage taken amount
    /// </returns>
    public override float OnDamageTaken(float amount)
    {
        absorbAmount -= amount;

        if (absorbAmount <= 0)
        {
            Destroy();
            return absorbAmount * -1;
        }
        else
        {
            return 0;
        }
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

}
