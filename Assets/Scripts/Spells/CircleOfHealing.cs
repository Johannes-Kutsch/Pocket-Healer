using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;

/// <summary>
/// A Heal that heals the 5 lowest raiders.
/// </summary>
public class CircleOfHealing : Spell {
    private readonly string SPELLNAME = "Circle of Healing";
    private float cooldown = 8f;
    private readonly float MANACOST = 80f;
    private float castTime = 0f;
    private readonly string CASTSOUNDNAME = "GreaterHealCast";
    private readonly string IMPACTSOUNDNAME = "CircleCast";

    public float healAmount = 50f;
    public float numberTargets = 5f;

    /// <summary>
    /// Called on awake.
    /// Checks if talents are picked that modify the skill.
    /// Set variables in base class.
    /// </summary>
    public void Awake()
    {
        if (GameControl.control.talente[2]) //7 targets talent
        {
            numberTargets += 2;
        }

        if (GameControl.control.talente[7]) //no cooldown but casttime talent
        {
            cooldown = 0f;
            castTime = 2f;
        }
    }

    /// <summary>
    /// Called when a cast is sucesfully finished. Heals the numberTargets lowest raiders.
    /// </summary>
    public override void OnCastSucess()
    {
        List<Raider> raiderDict = RaiderDB.GetInstance().GetAllRaidersSortedByHealth();

        if (raiderDict.Count() < numberTargets) //if less raiders are alive than we can target, decrease number of targets
        {
            numberTargets = raiderDict.Count();
        }

        for (int i = 0; i < numberTargets; i++) //heal numberTargets raider
        {
            Raider target = raiderDict.First();
            if (target.IsAlive())
            {
                target.Heal(healAmount);
            }
            raiderDict.Remove(target);
        }
    }

    /// <summary>
    /// Gets the spellname.
    /// </summary>
    /// <returns>
    /// the spellname
    /// </returns>
    public override string GetSpellname()
    {
        return SPELLNAME;
    }

    /// <summary>
    /// Gets the cooldown.
    /// </summary>
    /// <returns>
    /// the cooldown
    /// </returns>
    public override float GetCooldown()
    {
        return cooldown;
    }

    /// <summary>
    /// Gets the manacost.
    /// </summary>
    /// <returns>
    /// the manacost
    /// </returns>
    public override float GetManacost()
    {
        return MANACOST;
    }

    /// <summary>
    /// Gets the cast time.
    /// </summary>
    /// <returns>
    /// the cast time
    /// </returns>
    public override float GetCastTime()
    {
        return castTime;
    }

    /// <summary>
    /// Gets the name of the cast sound.
    /// </summary>
    /// <returns>
    /// the name of the cast sound
    /// </returns>
    public override string GetCastSoundName()
    {
        return CASTSOUNDNAME;
    }

    /// <summary>
    /// Gets the name of the impact sound.
    /// </summary>
    /// <returns>
    /// the name of the impact sound
    /// </returns>
    public override string GetImpactSoundName()
    {
        return IMPACTSOUNDNAME;
    }
}
