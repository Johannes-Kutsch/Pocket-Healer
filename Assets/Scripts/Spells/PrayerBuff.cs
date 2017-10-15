using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class PrayerBuff : Buff {
    private readonly float DURATION = 10f;
    private readonly string MATERIALNAME = "PrayerOfMending_Buff";

    private float healAmount = 50f;
    private float jumpsLeft = 6;

    /// <summary>
    /// Called on awake.
    /// Set variables in base class.
    /// </summary>
    void Awake()
    {
        base.resetable = true;
    }

    /// <summary>
    /// Called when the Reset Method is executet, independetly from the resetable variable.
    /// </summary>
    public override void OnReset()
    {
        jumpsLeft = 6;
    }

    /// <summary>
    /// Gets called when the raider the buff is attached to takes damage.
    /// The amount can be modivied here, i.e. if the buff decrases the damage taken by 20% we just return amount * 0.8.
    /// If the damage amount should not be modified we just return the original value.
    /// </summary>
    /// <param name="amount">the amount.</param>
    /// <returns>
    /// the new damage taken amount
    /// </returns>
    public override float OnDamageTaken(float amount)
    {
        Destroy();
        Jump();
        return amount;
    }

    /// <summary>
    /// Called when the buff is destroyed.
    /// Decreases jumps by 1 and applies the buff to another raider.
    /// </summary>
    public void Jump()
    {
        GetRaider().Heal(healAmount);
        if (jumpsLeft > 0)
        {
            List<Raider> raiderDict = RaiderDB.GetInstance().GetAllRaidersSortedByHealth();
            raiderDict.Remove(GetRaider());

            int countRaider = raiderDict.Count;
            for (int i = 0; i < countRaider; i++)
            {
                Raider target = raiderDict.First();

                if (!target.GetGameObject().GetComponent<PrayerBuff>())
                {
                    target = raiderDict.First();
                    PrayerBuff buff = target.GetGameObject().AddComponent<PrayerBuff>();
                    buff.jumpsLeft = jumpsLeft - 1;
                    break;
                }

                raiderDict.Remove(target);
            }
        }
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
