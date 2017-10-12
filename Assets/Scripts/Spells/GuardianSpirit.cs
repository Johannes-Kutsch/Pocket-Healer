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
    

    /// <summary>
    /// Called on awake.
    /// Checks if talents are picked that modify the skill.
    /// Set variables in base class.
    /// </summary>
    void Awake()
    {
        base.impactSound = Resources.Load("SchutzgeistCast", typeof(AudioClip)) as AudioClip;
        base.cooldown = COOLDOWN;
        base.manaCost = MANACOST;
        base.castTime = CASTTIME;
        base.spellName = SPELLNAME;
    }

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
}
