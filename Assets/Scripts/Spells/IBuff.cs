using System.Collections;
using UnityEngine;

/// <summary>
/// Interface for Buffs/Debuffs.
/// </summary>
public interface IBuff {

    /// <summary>
    /// Gets the duration.
    /// </summary>
    /// <returns>the duration, -1 if endless</returns>
    float GetDuration();

    /// <summary>
    /// Gets the material used to display the buff.
    /// </summary>
    /// <returns>the material</returns>
    Material GetMaterial();


    /// <summary>
    /// Gets the remaining duration.
    /// </summary>
    /// <returns></returns>
    string GetRemainingDuration();


    /// <summary>
    /// Gets called when the raider the buff is attached to takes damage.
    /// The amount can be modivied here, i.e. if the buff decrases the damage taken by 20% we just return amount * 0.8.
    /// If the damage amount should not be modified we just return the original value.
    /// </summary>
    /// <param name="amount">the amount.</param>
    /// <returns>the new damage taken amount</returns>
    float DamageTaken(float amount);


    /// <summary>
    /// Gets called when the raider the buff is attached to receives fatal damage but bevore the damage is applied.
    /// This is i.e. used to proc guardian spirit.
    /// </summary>
    /// <param name="amount">The amount.</param>
    /// <returns>the new damage taken amount</returns>
    float FatalDamage(float amount);


    /// <summary>
    /// Gets called when the raider the buff is attached to takes healing.
    /// The amount can be modivied here, i.e. if the buff increses the healing taken by 20% we just return amount * 1.2.
    /// If the heal amount should not be modified we just return the original value.
    /// </summary>
    /// <param name="amount">The amount.</param>
    /// <returns>the new healamount</returns>
    float HealingTaken(float amount);


    /// <summary>
    /// Gets called when any raider takes healing.
    /// The amount can be modivied here, i.e. if the buff increses the healing taken by 20% we just return amount * 1.2.
    /// If the heal amount should not be modified we just return the original value.
    /// This is i.e. used for the flame talent.
    /// </summary>
    /// <param name="amount">The amount.</param>
    /// <returns>the new healamount</returns>
    float GlobalHealingTaken(float amount);

    /// <summary>
    /// Gets called when any raider takes damage.
    /// The amount can be modivied here, i.e. if the buff decrases the damage taken by 20% we just return amount * 0.8.
    /// If the damage amount should not be modified we just return the original value.
    /// </summary>
    /// <param name="amount">The amount.</param>
    /// <returns>the new healamount</returns>
    float GlobalDamageTaken(float amount);


    /// <summary>
    /// Determines whether this instance is a buff or a debuff.
    /// </summary>
    /// <returns>
    ///   <c>true</c> if this instance is a buff; if this instances is a debuff, <c>false</c>.
    /// </returns>
    bool IsBuff();


    /// <summary>
    /// Determines whether this instance is dispellable.
    /// </summary>
    /// <returns>
    ///   <c>true</c> if this instance is dispellable; otherwise, <c>false</c>.
    /// </returns>
    bool IsDispellable();


    /// <summary>
    /// Destroys this buff.
    /// </summary>
    void Destroy();


    /// <summary>
    /// Resets this buff to as if it was freshly applied. This is used 
    /// </summary>
    void Reset();
}
