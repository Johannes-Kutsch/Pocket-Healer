using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Invisible buff used for the guardian spirit talent. When the target receivs fatal damage he is healed to full instead. The original guardian spirit and all other guardian spirit invis buffs are removed afterwards.
/// </summary>
public class GuardianSpiritBuffInvis : MonoBehaviour, IBuff, IGuardianSpirit
{
    private List<IRaider> raiderDict;
    public Material image = null;
    public float duration = 14f;
    public float runtime;
    public float timeLeft;

    private IRaider raider;

    /// <summary>
    /// Called on start.
    /// </summary>
    void Start()
    {
        raider = GetComponent<IRaider>();
    }

    /// <summary>
    /// Called with every fixed update.
    /// </summary>
    void FixedUpdate()
    {
        runtime = runtime + (float)0.02;
        timeLeft = duration - runtime;
        if (timeLeft <= 0)
        {
            Destroy();
        }
    }

    /// <summary>
    /// Resets this buff as if it was freshly applied. This is used for the reset debuffs on dispell talent.
    /// </summary>
    public void Reset()
    {

    }

    /// <summary>
    /// Gets the duration.
    /// </summary>
    /// <returns>
    /// the duration, -1 if endless
    /// </returns>
    public float GetDuration()
    {
        return 99999999999;
    }

    /// <summary>
    /// Gets the material used to display the buff.
    /// </summary>
    /// <returns>
    /// the material
    /// </returns>
    public Material GetMaterial()
    {
        return image;
    }

    /// <summary>
    /// Gets the remaining duration.
    /// </summary>
    /// <returns></returns>
    public string GetRemainingDuration()
    {
        return " ";
    }

    /// <summary>
    /// Gets called when any raider takes damage.
    /// The amount can be modivied here, i.e. if the buff decrases the damage taken by 20% we just return amount * 0.8.
    /// If the damage amount should not be modified we just return the original value.
    /// </summary>
    /// <param name="amount">The amount.</param>
    /// <returns>
    /// the new healamount
    /// </returns>
    public float OnGlobalDamageTaken(float amount)
    {
        return amount;
    }

    /// <summary>
    /// Gets called when any raider takes healing.
    /// The amount can be modivied here, i.e. if the buff increses the healing taken by 20% we just return amount * 1.2.
    /// If the heal amount should not be modified we just return the original value.
    /// This is i.e. used for the flame talent.
    /// </summary>
    /// <param name="amount">The amount.</param>
    /// <returns>
    /// the new healamount
    /// </returns>
    public float OnGlobalHealingTaken(float amount)
    {
        return amount;
    }

    /// <summary>
    /// Gets called when the raider the buff is attached to takes healing.
    /// The amount can be modivied here, i.e. if the buff increses the healing taken by 20% we just return amount * 1.2.
    /// If the heal amount should not be modified we just return the original value.
    /// </summary>
    /// <param name="amount">The amount.</param>
    /// <returns>
    /// the new healamount
    /// </returns>
    public float OnHealingTaken(float amount)
    {
        return amount;
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
    public float OnDamageTaken(float amount)
    {
        return amount;
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
        raider.HealSimple(300, false);
        Destroy();
        raiderDict = RaiderDB.GetInstance().GetAllRaidersSortedByHealth();
        raiderDict.Remove(raider);
        foreach (IRaider target in raiderDict)
        {
            IGuardianSpirit buff = target.GetGameObject().GetComponent<IGuardianSpirit>();
            if(buff is GuardianSpiritBuff)
            {
                target.HealSimple(300, false); //Heal original target to full
            }
            if (buff != null)
            {
                target.GetGameObject().GetComponent<BuffManager>().DeregisterBuff(buff);
                buff.Destroy();
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
    public bool IsBuff()
    {
        return true;
    }

    /// <summary>
    /// Determines whether this instance is dispellable.
    /// </summary>
    /// <returns>
    ///   <c>true</c> if this instance is dispellable; otherwise, <c>false</c>.
    /// </returns>
    public bool IsDispellable()
    {
        return false;
    }

    /// <summary>
    /// Destroys this buff.
    /// </summary>
    public void Destroy()
    {
        GetComponent<BuffManager>().DeregisterBuff(this);
        Destroy(this);
    }

}