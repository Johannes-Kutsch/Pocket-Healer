using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

/// <summary>
/// Applies the Prayer of Mending Buff, which heals the target when it takes damage and jumps to another raidmember.
/// </summary>
public class Prayer : Spell {
    private readonly string SPELLNAME = "Prayer of Mending";
    private readonly float MANACOST = 30f;
    private readonly float CASTTIME = 0f;
    private readonly float COOLDOWN = 10f;
    
    /// <summary>
    /// Called on awake.
    /// Set variables in base class.
    /// </summary>
    void Awake()
    {
        base.impactSound = Resources.Load("PrayerCast", typeof(AudioClip)) as AudioClip;
        base.cooldown = COOLDOWN;
        base.manaCost = MANACOST;
        base.castTime = CASTTIME;
        base.spellName = SPELLNAME;
    }

    /// <summary>
    /// Called when a cast is sucesfully finished. Applies the PrayerBuff to the target.
    /// </summary>
    public override void OnCastSucess()
    {
        IRaider target = GetTarget();

        if (!target.GetGameObject().GetComponent<PrayerBuff>())
        {
            target.GetGameObject().AddComponent<PrayerBuff>();
        }
        else
        {
            target.GetGameObject().GetComponent<PrayerBuff>().Reset();
        }
    }
}
