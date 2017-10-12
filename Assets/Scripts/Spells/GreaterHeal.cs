using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

/// <summary>
/// Heals the current target.
/// </summary>
public class GreaterHeal : Spell
{
    private readonly string SPELLNAME = "Greater Heal";
    private readonly float COOLDOWN = 0f;
    private readonly float MANACOST = 20f;
    private readonly float CASTTIME = 2f;

    private float healAmount = 50f;


    /// <summary>
    /// Called on awake.
    /// Checks if talents are picked that modify the skill.
    /// Set variables in base class.
    /// </summary>
    void Awake()
    {
        base.castSound = Resources.Load("GreaterHealCast", typeof(AudioClip)) as AudioClip;
        base.impactSound = Resources.Load("GreaterHealImpact", typeof(AudioClip)) as AudioClip;
        base.cooldown = COOLDOWN;
        base.manaCost = MANACOST;
        base.castTime = CASTTIME;
        base.spellName = SPELLNAME;
    }

    /// <summary>
    /// Called when a cast is sucesfully finished.
    /// Heals the target by healAmount.
    /// </summary>
    public override void OnCastSucess()
    {
        GetTarget().Heal(healAmount);
    }
}