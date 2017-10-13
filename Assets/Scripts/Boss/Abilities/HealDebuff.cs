using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// A undispellable debuff that prevents the target from taking healing and slowly damages it.
/// </summary>
public class HealDebuff : BuffTicking
{
    private readonly string MATERIALNAME = "Heal_Debuff";
    private readonly float DURATION = 12f;
    private readonly float INTERVALLTICKS = 1.5f;

    private float damagePerTick = 20f;

    /// <summary>
    /// Called on awake.
    /// </summary>
    void Awake()
    {
        if (GameControl.control.difficulty == 0)
        {
            damagePerTick *= GameControl.control.easyMultiplyer;
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
        return 0;
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
