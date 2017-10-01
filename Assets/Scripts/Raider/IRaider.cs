using UnityEngine;

/// <summary>
/// Interface for a raider, i.e. a partymember or encounterobject that can be healed and damaged
/// </summary>
public interface IRaider {
    //ToDo combine Damage and DamageSimple / Heal and HealSimple and controll cloudburst/events with variables

    /// <summary>
    /// Damages the raider by an amount, i.e. decreases the currentHealth.
    /// Triggers DamageTaken events in all buffs.
    /// </summary>
    /// <param name="amount">The amount.</param>
    void Damage(float amount);

    /// <summary>
    /// Heals the raider by an amount, i.e. increases the currentHealth.
    /// Triggers HealingTaken events in all buffs.
    /// The currentHealth can not be bigger than maxHealth.
    /// </summary>
    /// <param name="amount">The amount.</param>
    void Heal(float amount);

    /// <summary>
    /// Damages the raider by an amount in a simple way (i.e. without triggering the DamageTaken events in buffs).
    /// </summary>
    /// <param name="amount">The amount.</param>
    /// <param name="combatText">if set to <c>true</c> a combat text will be displayed.</param>
    void DamageSimple(float amount, bool combatText);

    /// <summary>
    /// Heals the raider by an amount in a simple way (i.e. without triggering the cloudburst talent).
    /// </summary>
    /// <param name="amount">The amount.</param>
    /// <param name="combatText">if set to <c>true</c> a combat text will be displayed.</param>
    void HealSimple(float amount, bool combatText);

    /// <summary>
    /// Updates the hp bar.
    /// </summary>
    void UpdateHpBar();

    /// <summary>
    /// Changes the color of the background.
    /// </summary>
    /// <param name="color">The color.</param>
    void ChangeBackgroundColor(Color32 color);

    /// <summary>
    /// Sets this as the current target.
    /// </summary>
    void SetTarget();

    /// <summary>
    /// Gets the current health.
    /// </summary>
    /// <returns>the current health</returns>
    float GetHealth();

    /// <summary>
    /// Gets the game object.
    /// </summary>
    /// <returns></returns>
    GameObject GetGameObject();

    /// <summary>
    /// Determines whether this instance is alive or not.
    /// </summary>
    /// <returns>
    ///   <c>true</c> if this instance is alive; otherwise, <c>false</c>.
    /// </returns>
    bool IsAlive();

    /// <summary>
    /// Dies this instance.
    /// </summary>
    void Die();

    /// <summary>
    /// Sets this as the current boss target (i.e. the target that is hit by auto attacks).
    /// </summary>
    /// <param name="isTarget">if set to <c>true</c> this is the current boss target.</param>
    void SetBossTarget(bool isTarget);

    /// <summary>
    /// Multiplies the Healmultiplier with the a value.
    /// </summary>
    /// <param name="multiplier">The value.</param>
    void ChangeHealmultiplier(float value);

    /// <summary>
    /// Increases the maximum health.
    /// </summary>
    /// <param name="health">The amount the health is increased by.</param>
    void IncreaseMaxHealth(float health);

    /// <summary>
    /// Gets the color that is used if the instance is not the target.
    /// </summary>
    /// <returns></returns>
    Color32 GetNotTargetColor();

    /// <summary>
    /// Sets the color that is used if the instance is not the target.
    /// </summary>
    /// <returns></returns>
    void SetNotTargetColor(Color32 color);

    /// <summary>
    /// Gets the color that is used if the instance is the target.
    /// </summary>
    /// <returns></returns>
    Color32 GetTargetColor();

    /// <summary>
    /// Sets the color that is used if the instance is the target.
    /// </summary>
    /// <returns></returns>
    void SetTargetColor(Color32 color);
}
