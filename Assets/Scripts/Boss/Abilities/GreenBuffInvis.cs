using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// The invisible buff applied by the green dragon. Splits half of you healing evengly among all raiders.
/// </summary>
public class GreenBuffInvis : Buff
{
    private readonly string MATERIALNAME = null;
    private readonly float DURATION = float.PositiveInfinity;

    /// <summary>
    /// Gets called when the raider the buff is attached to takes healing.
    /// The amount can be modivied here, i.e. if the buff increses the healing taken by 20% we just return amount * 1.2.
    /// If the heal amount should not be modified we just return the original value.
    /// </summary>
    /// <param name="amount">The amount.</param>
    /// <returns>
    /// the new healamount
    /// </returns>
    public override float OnHealingTaken(float amount)
    {
        List<Raider> raiderDict = RaiderDB.GetInstance().GetAllRaiders();
        float healAmount = amount * 0.05f;

        foreach (Raider target in raiderDict)
        {
            target.HealSimple(healAmount, false);
        }

        return amount;
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