using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

/// <summary>
/// A short ticking Debuff that deals raidwide damage if the target is healed to full or dies.
/// </summary>
public class StoneOne : BuffTicking
{
    private readonly string MATERIALNAME = "Stein_1";
    private readonly float DURATION = 10;
    public readonly float INTERVALLTICKS = 3f;

    public float damagePerTick = 30f;
    public float exploDmg = 90f;

    /// <summary>
    /// Called on awake.
    /// </summary>
    void Awake()
    {
        if (GameControl.control.difficulty == 0)
        {
            damagePerTick *= GameControl.control.easyMultiplier;
            exploDmg *= GameControl.control.easyMultiplier;
        }
    }

    /// <summary>
    /// Called with every fixed update
    /// </summary>
    public override void OnFixedUpdate()
    {
        if (raider.GetHealth() >= 1f)
        {
            List<Raider> targetDict = RaiderDB.GetInstance().GetAllRaiders();

            Destroy();

            foreach (Raider target in targetDict)
            {
                target.Damage(exploDmg);
            }

            Debug.Log("StoneOne Exploded with at 100% Health.");
        }
    }

    /// <summary>
    /// Called with every tick of the buff. Only used when ticks is greater than 1.
    /// </summary>
    public override void OnTick()
    {
        GetRaider().Damage(damagePerTick);
    }

    /// <summary>
    /// Gets called when the raider the buff is attached to receives fatal damage but bevore the damage is applied.
    /// This is i.e. used to proc guardian spirit.
    /// </summary>
    /// <param name="amount">The amount.</param>
    /// <returns>
    /// the new damage taken amount
    /// </returns>
    public override float OnFatalDamage(float amount)
    {
        bool hasGuardianSpirit = false;
        foreach (IBuff buff in GetComponent<BuffManager>().GetAllBuffsSortetByDuration())
        {
            if (buff.GetType() == typeof(GuardianSpiritBuff) || buff.GetType() == typeof(GuardianSpiritBuffInvis))
            {
                hasGuardianSpirit = true;
            }
        }

        if (!hasGuardianSpirit) //only damage when no guardian spirit
        {
            List<Raider> targetDict = RaiderDB.GetInstance().GetAllRaiders();
            targetDict.Remove(GetRaider());

            Destroy();

            foreach (Raider target in targetDict)
            {
                target.Damage(exploDmg);
            }

            Debug.Log("StoneOne Exploded with Fatal Damage.");
        }

        return amount;
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

    public override bool IsBuff()
    {
        return false;
    }
}
