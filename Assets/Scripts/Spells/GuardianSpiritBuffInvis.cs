using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Invisible buff used for the guardian spirit talent. When the target receivs fatal damage he is healed to full instead. The original guardian spirit and all other guardian spirit invis buffs are removed afterwards.
/// </summary>
public class GuardianSpiritBuffInvis : Buff, IGuardianSpirit
{
    private readonly float DURATION = 14f;
    private readonly string MATERIALNAME = null;

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

        List<IRaider> raiderDict = RaiderDB.GetInstance().GetAllRaidersSortedByHealth();
        raiderDict.Remove(GetRaider());

        foreach (IRaider target in raiderDict)
        {
            IGuardianSpirit buff = target.GetGameObject().GetComponent<IGuardianSpirit>();

            if(buff is GuardianSpiritBuff)
            {
                target.HealSimple(300, false); //Heal original target to full
            }

            buff.Destroy();
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