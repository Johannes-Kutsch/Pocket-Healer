using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

/// <summary>
/// Applys a Buff that heals the raider to full if he would die.
/// </summary>
public class GuardianSpirit : Spell
{
    private readonly string SPELLNAME = "Guardian Spirit";
    private readonly float MANACOST = 20f;
    private readonly float CASTTIME = 0f;
    private readonly float COOLDOWN = 15f;
    private readonly string CASTSOUNDNAME = null;
    private readonly string IMPACTSOUNDNAME = "SchutzgeistCast";

    /// <summary>
    /// Called when a cast is sucesfully finished.
    /// Applys the guardian spirit buff to the current target.
    /// Applys the guardian spirit invis buff to every other raider if the talent is selected.
    /// </summary>
    public override void OnCastSucess()
    {
        IRaider target = GetTarget();
        
        if (!target.GetGameObject().GetComponent<GuardianSpiritBuff>()) //check if target allready has the buff
        {
            GuardianSpiritBuff buff = target.GetGameObject().AddComponent<GuardianSpiritBuff>(); //apply new buff
        }
        else
        {
            target.GetGameObject().GetComponent<GuardianSpiritBuff>().Reset(); //refresh old buff
        }

        if (GameControl.control.talente[8]) //apply invis buff to every other raider if talent is picked
        {
            List<IRaider> raiderDict = RaiderDB.GetInstance().GetAllRaidersSortedByHealth();

            raiderDict.Remove(target);

            foreach (IRaider raider in raiderDict)
            {
                GuardianSpiritBuffInvis buff = raider.GetGameObject().AddComponent<GuardianSpiritBuffInvis>();
            }
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
        return COOLDOWN;
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
        return CASTTIME;
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
