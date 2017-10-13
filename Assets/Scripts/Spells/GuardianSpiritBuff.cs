using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Buff used by the fuardian spirit spell. Heals the target to full if he takes fatal damage.
/// </summary>
public class GuardianSpiritBuff : Buff, IGuardianSpirit
{
    private readonly float DURATION = 14f;
    private readonly string MATERIALNAME = "Schutzgeist_Buff";

    /// <summary>
    /// Called on awake.
    /// Set variables in base class.
    /// </summary>
    void Awake()
    {
        base.resetable = true;
    }

    /// <summary>
    /// Gets called when the raider the buff is attached to receives fatal damage but bevore the damage is applied.
    /// This is i.e. used to proc guardian spirit.
    /// </summary>
    /// <param name="amount">The amount.</param>
    /// <returns>
    /// the new damage taken amount
    /// </returns>
    public override float OnFatalDamage(float amount)
    {
        GetRaider().HealSimple(300, false);
        Destroy();

        if (GameControl.control.talente[8])
        {
            List<IRaider> raiderDict = RaiderDB.GetInstance().GetAllRaidersSortedByHealth();
            raiderDict.Remove(GetRaider());

            foreach (IRaider raider in raiderDict)
            {
                raider.GetGameObject().GetComponent<IGuardianSpirit>().Destroy();
            }
        }

        return 0;
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
        return MATERIALNAME;
    }
}
