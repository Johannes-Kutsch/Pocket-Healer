using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// A dispellable debuff that slowly damages the target.
/// </summary>
public class DotDebuff : BuffTicking
{
    private readonly string MATERIALNAME = "Dot_Debuff";
    private readonly float DURATION = 12f;
    private readonly float INTERVALLTICKS = 1.5f;

    private float damagePerTick = 30f;
    private Color32 debuffColor = new Color32(170, 0, 255, 255);

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
    /// Called when the buff finished initialisation.
    /// </summary>
    public override void OnStart()
    {
        GetRaider().ChangeBackgroundColor(debuffColor);
    }

    /// <summary>
    /// Called with every update.
    /// </summary>
    void Update()
    {
        if (!(raider == FindObjectOfType<Gamestate>().GetTarget()))
        {
            raider.ChangeBackgroundColor(debuffColor);
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
    /// Destroys this buff.
    /// </summary>
    public override void OnDestroy()
    {
        if (GetRaider().IsAlive())
        {
            if (raider == FindObjectOfType<Gamestate>().GetTarget())
            {
                raider.ChangeBackgroundColor(raider.GetTargetColor());
            }
            else
            {
                raider.ChangeBackgroundColor(raider.GetNotTargetColor());
            }
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
        return true;
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
