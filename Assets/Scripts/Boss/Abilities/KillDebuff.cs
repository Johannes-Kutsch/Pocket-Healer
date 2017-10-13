using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// A Debuff that kills the raider after a fixed duration if not dispelled
/// </summary>
public class KillDebuff : Buff {
    private readonly float DURATION = 10f;
    private readonly string MATERIALNAME = "Kill_Debuff";

    private Color32 debuffColor = new Color32(170, 0, 255, 255);

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
    /// Called when the duration is greater than the runtime i.e. the buff has timed out.
    /// </summary>
    public override void OnRuntimeOver()
    {
        GetRaider().Die();
    }

    /// <summary>
    /// Called when the buff is destroyed.
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
    /// Determines whether this instance is a buff or a debuff.
    /// </summary>
    /// <returns>
    ///   <c>true</c> if this instance is a buff; if this instances is a debuff, <c>false</c>.
    /// </returns>
    public override bool IsBuff()
    {
        return false;
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
}
