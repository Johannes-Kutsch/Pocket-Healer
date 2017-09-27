using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// A Debuff that deals damage to a single raider. The damage is doubled after each tick, up to a maximum. Every time the Debuff ticks it also damages the Boss.
/// </summary>
public class MarkDebuff : MonoBehaviour, IBuff
{
    public Material image;
    public float duration = 18f;
    public float runtime;
    public float damagePerTick = 2f;
    public float maxDamagePerTick = 32f;
    public float multiplier = 2f;
    public float tickLength = 1f;
    public float bossDamage = 150f;
    public int ticksSinceLastEffect = 0;
    private IRaider raider;
    private Boss boss;

    /// <summary>
    /// Called on start.
    /// </summary>
    void Start()
    {
        image = Resources.Load("Marked_Debuff", typeof(Material)) as Material;
        if (GameControl.control.difficulty == 0)
        {
            damagePerTick *= GameControl.control.easyMultiplyer;
            maxDamagePerTick *= GameControl.control.easyMultiplyer;
        }
        raider = GetComponent<IRaider>();
        boss = FindObjectOfType<Boss>();
        StartCoroutine(ApplyDamage());
    }

    /// <summary>
    /// Called on every fixed update.
    /// </summary>
    void FixedUpdate()
    {
        runtime = runtime + 0.02f;
    }

    /// <summary>
    /// Coroutine used to damage the raider & boss.
    /// </summary>
    /// <returns></returns>
    IEnumerator ApplyDamage()
    {
        float ticks = (duration / tickLength);
        for (ticksSinceLastEffect = 0; ticksSinceLastEffect < ticks; ticksSinceLastEffect++)
        {
            
            yield return new WaitForSeconds(tickLength);
            
            if (raider != null)
            {
                raider.Damage(damagePerTick);
                boss.TakeDamage(bossDamage);
            }
            if (damagePerTick*multiplier > maxDamagePerTick)
            {
                damagePerTick = maxDamagePerTick;
            } else
            {
                bossDamage *= multiplier;
                damagePerTick *= multiplier;
            }
        }
        Destroy();
    }

    /// <summary>
    /// Gets the duration.
    /// </summary>
    /// <returns>
    /// the duration, -1 if endless
    /// </returns>
    public float GetDuration()
    {
        return -1;
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
        return (duration - runtime).ToString("F0");
    }

    /// <summary>
    /// Determines whether this instance is a buff or a debuff.
    /// </summary>
    /// <returns>
    ///   <c>true</c> if this instance is a buff; if this instances is a debuff, <c>false</c>.
    /// </returns>
    public bool IsBuff()
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

    /// <summary>
    /// Gets called when any raider takes damage.
    /// The amount can be modivied here, i.e. if the buff decrases the damage taken by 20% we just return amount * 0.8.
    /// If the damage amount should not be modified we just return the original value.
    /// </summary>
    /// <param name="amount">The amount.</param>
    /// <returns>
    /// the new healamount
    /// </returns>
    public float GlobalDamageTaken(float amount)
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
    public float GlobalHealingTaken(float amount)
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
    public float HealingTaken(float amount)
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
    public float DamageTaken(float amount)
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
    public float FatalDamage(float amount)
    {
        return amount;
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
    /// Resets this buff as if it was freshly applied. This is used for the reset debuffs on dispell talent.
    /// </summary>
    public void Reset()
    {
        runtime = 0;
    }
}