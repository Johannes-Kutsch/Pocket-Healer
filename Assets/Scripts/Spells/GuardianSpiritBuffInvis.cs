using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Invisible buff used for the guardian spirit talent. When the target receivs fatal damage he is healed to full instead. The original guardian spirit and all other guardian spirit invis buffs are removed afterwards.
/// </summary>
public class GuardianSpiritBuffInvis : Buff, IGuardianSpirit
{
    private readonly float Duration = 14f;

    /// <summary>
    /// Called on awake.
    /// Set variables in base class.
    /// </summary>
    void Awake()
    {
        base.image = null;
        base.resetable = true;

        base.duration = Duration;
    }

    /// <summary>
    /// Gets the duration.
    /// </summary>
    /// <returns>
    /// the duration, -1 if endless
    /// </returns>
    public override float GetDuration()
    {
        return 99999999999;
    }

    /// <summary>
    /// Gets the remaining duration.
    /// </summary>
    /// <returns></returns>
    public override string GetTimeLeft()
    {
        return " ";
    }

    /// <summary>
    /// Gets called when the raider the buff is attached to receives fatal damage but bevore the damage is applied.
    /// This is i.e. used to proc guardian spirit.
    /// </summary>
    /// <param name="amount">The amount.</param>
    /// <returns>
    /// the new damage taken amount
    /// </returns>
    public float OnFatalDamage(float amount)
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
}