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
    private new float cooldown = 8f;
    private readonly float MANACOST = 80f;
    private new float castTime = 0f;

    private float healAmount = 50f;
    private float numberTargets = 5f;

    /// <summary>
    /// Called on awake.
    /// Checks if talents are picked that modify the skill.
    /// Set variables in base class.
    /// </summary>
    void Awake()
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

        base.castSound = Resources.Load("GreaterHealCast", typeof(AudioClip)) as AudioClip;
        base.impactSound = Resources.Load("CircleCast", typeof(AudioClip)) as AudioClip;
        base.cooldown = cooldown;
        base.manaCost = MANACOST;
        base.castTime = castTime;
        base.spellName = SPELLNAME;
    }

    /// <summary>
    /// Called when a cast is sucesfully finished. Heals the numberTargets lowest raiders.
    /// </summary>
    public override void OnCastSucess()
    {
        List<IRaider> raiderDict = RaiderDB.GetInstance().GetAllRaidersSortedByHealth();

        if (raiderDict.Count() < numberTargets) //if less raiders are alive than we can target, decrease number of targets
        {
            numberTargets = raiderDict.Count();
        }

        for (int i = 0; i < numberTargets; i++) //heal numberTargets raider
        {
            IRaider target = raiderDict.First();
            if (target.IsAlive())
            {
                target.Heal(healAmount);
            }
            raiderDict.Remove(target);
        }
    }
}
