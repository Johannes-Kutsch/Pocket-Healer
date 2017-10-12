using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;
using System;

/// <summary>
/// A Heal that heals the target and the raider with the lowest hp that is not the target.
/// </summary>
public class BindingHeal : Spell
{
    private readonly string SPELLNAME = "Binding Heal";
    private readonly float COOLDOWN = 0f;
    private readonly float MANACOST = 25f;
    private readonly float CASTTIME = 2f;

    private float healAmount = 50f;
    private float numberJumps = 1f;

    /// <summary>
    /// Called on awake.
    /// Set variables in base class.
    /// </summary>
    void Awake()
    {
        base.castSound = Resources.Load("GreaterHealCast", typeof(AudioClip)) as AudioClip;
        base.impactSound = Resources.Load("FlashHealImpact", typeof(AudioClip)) as AudioClip;
        base.cooldown = COOLDOWN;
        base.manaCost = MANACOST;
        base.castTime = CASTTIME;
        base.spellName = SPELLNAME;
    }

    /// <summary>
    /// Called when a cast is sucesfully finished. Heals the target and another raider.
    /// </summary>
    public override void OnCastSucess()
    {
        //heal target
        GetTarget().Heal(healAmount);

        //get all raider sorted by health
        List<IRaider> raiderDict = RaiderDB.GetInstance().GetAllRaidersSortedByHealth();

        for (int i = 0; i < numberJumps && i < raiderDict.Count(); i++)
        {
            raiderDict.Remove(GetTarget()); //remove original target
            raiderDict.First().Heal(healAmount); //heal lowest raider
        }
    }
}
