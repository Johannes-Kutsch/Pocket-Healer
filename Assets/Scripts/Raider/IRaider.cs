using UnityEngine;

/// <summary>
/// Interface for a raider, i.e. a partymember or encounterobject that can be healed and damaged
/// </summary>
public interface IRaider {

    /// <summary>
    /// Reduces the hp.
    /// </summary>
    /// <param name="amount">The amount.</param>
    void ReduceHP(float amount);

    /// <summary>
    /// Increases the hp.
    /// </summary>
    /// <param name="amount">The amount.</param>
    void IncreaseHP(float amount);

    /// <summary>
    /// Reduces the hp in a simple way (i.e. without triggering the DamageTaken events of debuffs).
    /// </summary>
    /// <param name="amount">The amount.</param>
    /// <param name="combatText">if set to <c>true</c> a combat text will be displayed.</param>
    void ReduceHPSimple(float amount, bool combatText);

    /// <summary>
    /// Increases the hp in a simple way (i.e. without triggering the cloudburst talent).
    /// </summary>
    /// <param name="amount">The amount.</param>
    /// <param name="combatText">if set to <c>true</c> a combat text will be displayed.</param>
    void IncreaseHPSimple(float amount, bool combatText);

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
    /// Changes the healmultiplyer.
    /// </summary>
    /// <param name="multiplyer">The multiplyer.</param>
    void ChangeHealmultiplyer(float multiplyer);

    /// <summary>
    /// Gets the maximum health.
    /// </summary>
    /// <param name="health">The health.</param>
    void GetMaxHealth(float health);

    /// <summary>
    /// Gets the color that is used if the instance is not the target.
    /// </summary>
    /// <returns></returns>
    Color32 GetNotTargetColor();

    /// <summary>
    /// Gets the color that is used if the instance is the target.
    /// </summary>
    /// <returns></returns>
    Color32 GetTargetColor();
}
