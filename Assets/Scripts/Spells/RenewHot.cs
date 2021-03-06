﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

/// <summary>
/// A buff that heals the raider with every tick.
/// </summary>
public class RenewHot : BuffTicking
{
    public readonly float DURATION = 10f;
    public readonly float INTERVALLTICKS = 2;
    private readonly string MATERIALNAME = "Renew_Buff";
    private readonly string MATERIALNAME_2 = "Renew_Buff_2";

    public readonly float HEALPERTICK = 10f;
    private float jumpsLeft = 1;

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
        jumpsLeft = 1;
        LoadMaterialImage();
    }

    /// <summary>
    /// Called with every tick of the buff. Only used when ticks is greater than 1.
    /// </summary>
    public override void OnTick()
    {
        GetRaider().Heal(HEALPERTICK);
    }

    /// <summary>
    /// Called when the buff is destroyed.
    /// </summary>
    public override void OnDestroy()
    {
        if (GameControl.control.talente[3])
        {
            if (jumpsLeft > 0)
            {
                List<Raider> raiderDict = RaiderDB.GetInstance().GetAllRaidersSortedByHealth();
                raiderDict.Remove(GetRaider());
                
                int countRaider = raiderDict.Count;
                for (int i = 0; i < countRaider; i++)
                {
                    Raider target = raiderDict.First();

                    if (!target.GetGameObject().GetComponent<RenewHot>())
                    {
                        target = raiderDict.First();
                        RenewHot buff = target.GetGameObject().AddComponent<RenewHot>();
                        buff.jumpsLeft = jumpsLeft - 1;
                        break;
                    }

                    raiderDict.Remove(target);
                }
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
        if (jumpsLeft == 1)
        {
            return MATERIALNAME;
        } else
        {
            return MATERIALNAME_2;
        }
    }

    /// <summary>
    /// Gets the number of ticks.
    /// </summary>
    /// <returns></returns>
    public override float GetIntervallTicks()
    {
        return INTERVALLTICKS;
    }
}
