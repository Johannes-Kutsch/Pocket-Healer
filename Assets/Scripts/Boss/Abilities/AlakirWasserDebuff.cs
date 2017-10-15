using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// An undispellable debuff without duration that reduces the Healing taken. The debuff gets stronger over time.
/// </summary>
public class AlakirWasserDebuff : BuffTicking
{
    private readonly string MATERIALNAME = "Luft";
    private readonly float DURATION = float.PositiveInfinity;
    private readonly float INTERVALLTICKS = 1f;

    private float currentHealMultiplier = 0.95f;
    private float healMultiplierIncrease = 0.05f;

    /// <summary>
    /// Called on awake.
    /// </summary>
    void Awake()
    {
        if (GameControl.control.difficulty == 0)
        {
            healMultiplierIncrease *= GameControl.control.easyMultiplier;
        }
    }

    /// <summary>
    /// Called with every tick of the buff.
    /// </summary>
    public override void OnTick()
    {
        if (currentHealMultiplier - healMultiplierIncrease >= 0)
        {
            currentHealMultiplier -= healMultiplierIncrease;
        }
        else
        {
            currentHealMultiplier = 0;
        }
    }

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
        return amount * currentHealMultiplier;
    }

    /// <summary>
    /// Gets the healmultiplier.
    /// </summary>
    /// <returns>The healmultiplier</returns>
    public float getHealMultiplier()
    {
        return currentHealMultiplier;
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
