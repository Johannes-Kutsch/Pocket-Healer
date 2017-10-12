using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

/// <summary>
/// Applies a buff that absorbs damage.
/// </summary>
public class Shield : Spell
{
    private readonly string SPELLNAME = "Shield";
    private readonly float MANACOST = 25f;
    private readonly float CASTTIME = 0f;
    private readonly float COOLDOWN = 3.5f;

    /// <summary>
    /// Called on awake.
    /// Set variables in base class. 
    /// </summary>
    void Awake()
    {
        base.impactSound = Resources.Load("ShieldImpact", typeof(AudioClip)) as AudioClip;
        base.cooldown = COOLDOWN;
        base.manaCost = MANACOST;
        base.castTime = CASTTIME;
        base.spellName = SPELLNAME;
    }

    /// <summary>
    /// Called when a cast is sucesfully finished. Applies the ShieldBuff to the target.
    /// </summary>
    public override void OnCastSucess()
    {
        IRaider target = GetTarget();

        if (!target.GetGameObject().GetComponent<ShieldBuff>())
        {
            target.GetGameObject().AddComponent<ShieldBuff>();
        }
        else
        {
            target.GetGameObject().GetComponent<ShieldBuff>().Reset();
        }
    }
}
