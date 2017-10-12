using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// An undispellable debuff without duration that reduces the Healing taken. The debuff gets stronger over time.
/// </summary>
public class AlakirWasserDebuff : MonoBehaviour, IBuff
{
    public Material image;
    private float tickLength = 1f;
    private float healMultiplier = 0.95f;
    private float healMultiplierIncrease = 0.05f;

    /// <summary>
    /// Called on start.
    /// </summary>
    void Start()
    {
        image = Resources.Load("Luft", typeof(Material)) as Material;

        if (GameControl.control.difficulty == 0)
        {
            healMultiplierIncrease *= GameControl.control.easyMultiplyer;
        }

        StartCoroutine(ApplyDamage());
    }

    /// <summary>
    /// Applies the damage.
    /// </summary>
    /// <returns></returns>
    IEnumerator ApplyDamage()
    {
        while (true)
        {
            yield return new WaitForSeconds(tickLength);
            if (healMultiplier - healMultiplierIncrease >= 0)
                healMultiplier -= healMultiplierIncrease;
            else
                healMultiplier = 0;
        }
    }

    /// <summary>
    /// Gets the duration.
    /// </summary>
    /// <returns>
    /// the duration, -1 if endless
    /// </returns>
    public float GetDuration()
    {
        return -2;
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
    public string GetTimeLeft()
    {
        return (" ");
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
        return amount * healMultiplier;
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
    }

    /// <summary>
    /// Gets the healmultiplier.
    /// </summary>
    /// <returns>The healmultiplier</returns>
    public float getHealMultiplier()
    {
        return healMultiplier;
    }
}
