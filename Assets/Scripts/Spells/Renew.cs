using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

/// <summary>
/// Applies a buff that heals the target over time.
/// </summary>
public class Renew : Spell
{
    private readonly string SPELLNAME = "Renew";
    private readonly float MANACOST = 10f;
    private readonly float CASTTIME = 0f;
    private readonly float COOLDOWN = 0f;

    /// <summary>
    /// Called on awake.
    /// Set variables in base class.
    /// </summary>
    void Awake()
    {
        base.impactSound = Resources.Load("RenewCast", typeof(AudioClip)) as AudioClip;
        base.cooldown = COOLDOWN;
        base.manaCost = MANACOST;
        base.castTime = CASTTIME;
        base.spellName = SPELLNAME;
    }

    /// <summary>
    /// Called when a cast is sucesfully finished. Applies the RenewBuff to the target.
    /// </summary>
    public override void OnCastSucess()
    {
        IRaider target = GetTarget();

        if (!target.GetGameObject().GetComponent<RenewHot>())
        {
            RenewHot hot = target.GetGameObject().AddComponent<RenewHot>();
            target.GetGameObject().GetComponent<BuffManager>().RegisterBuff(hot);
        }
        else
        {
            target.GetGameObject().GetComponent<RenewHot>().Reset();
        }
    }
}
