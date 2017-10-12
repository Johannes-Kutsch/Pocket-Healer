using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine;

/// <summary>
/// Abstract class spell which implements the basic functionality of most buffs.
/// It triggers the virtual methods OnStart() and OnTick() and OnDestroy(). (Called when the buff finished initialisation, with every tick of the buff and when the buff is destroyed)
/// Overwrite the following variables during awake: 
///     image; //The Material used for the buff image.
///     resetable; //True if the buff can be resettet by another application or with the dispell talent.
///     duration; //The duration of the spell, -1 when the buff lasts infinitive.
///     ticks; //The number of ticks, 0 if the spell does not have any ticks.
/// </summary>
public abstract class Buff : MonoBehaviour, IBuff
{
    private BuffManager buffManager;
    private IRaider raider;
    private float runtime = 0f;
    private float currentTicks = 0f;
    private float timeLeft;
    private float tickLength;

    public bool resetable = false;
    public float duration;
    public int ticks = 0;
    public Material image;

    /// <summary>
    /// Called on start.
    /// </summary>
    void Start()
    {
        buffManager = GetComponent<BuffManager>();
        buffManager.RegisterBuff(this);

        raider = GetComponent<IRaider>();

        tickLength = duration / ticks;
        timeLeft = duration;

        OnStart();
    }

    /// <summary>
    /// Called with every update. Advances the calculates if the onTick() or Destroy Method should be called.
    /// </summary>
    void FixedUpdate()
    {
        runtime = runtime + 0.02f;
        timeLeft = duration - runtime;

        if (ticks > 0 && runtime >= tickLength * (currentTicks + 1))
        {
            OnTick();
            currentTicks++;
        }

        if(runtime > duration)
        {
            Destroy();
        }
    }

    /// <summary>
    /// Destroys this buff.
    /// </summary>
    public void Destroy()
    {
        //OnDestroy(); apparently we don't need to call OnDestroy() here because it gets called automatically.

        buffManager.DeregisterBuff(this);
        Destroy(this);
    }

    /// <summary>
    /// Gets the duration.
    /// </summary>
    /// <returns>
    /// the duration, -1 if endless
    /// </returns>
    public virtual float GetDuration()
    {
        return duration;
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
    /// Gets the remaining time.
    /// </summary>
    /// <returns></returns>
    public virtual string GetTimeLeft()
    {
        return timeLeft.ToString("F0");
    }

    /// <summary>
    /// Determines whether this instance is a buff or a debuff.
    /// </summary>
    /// <returns>
    ///   <c>true</c> if this instance is a buff; if this instances is a debuff, <c>false</c>.
    /// </returns>
    public abstract bool IsBuff();

    /// <summary>
    /// Determines whether this instance is dispellable.
    /// </summary>
    /// <returns>
    ///   <c>true</c> if this instance is dispellable; otherwise, <c>false</c>.
    /// </returns>
    public abstract bool IsDispellable();

    /// <summary>
    /// Gets called when the raider the buff is attached to takes damage.
    /// The amount can be modivied here, i.e. if the buff decrases the damage taken by 20% we just return amount * 0.8.
    /// If the damage amount should not be modified we just return the original value.
    /// </summary>
    /// <param name="amount">the amount.</param>
    /// <returns>
    /// the new damage taken amount
    /// </returns>
    public virtual float OnDamageTaken(float amount)
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
    public virtual float OnFatalDamage(float amount)
    {
        return amount;
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
    public virtual float OnGlobalDamageTaken(float amount)
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
    public virtual float OnGlobalHealingTaken(float amount)
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
    public virtual float OnHealingTaken(float amount)
    {
        return amount;
    }

    /// <summary>
    /// Resets this buff as if it was freshly applied. This is used for the reset debuffs on dispell talent and when a buff that should only exists once is reaplied.
    /// </summary>
    public void Reset()
    {
        if(resetable)
        {
            runtime = 0f;
            currentTicks = 0f;
        }

        OnReset();
    }

    /// <summary>
    /// Gets the raider the buff is attached to.
    /// </summary>
    /// <returns></returns>
    public IRaider GetRaider()
    {
        return raider;
    }

    /// <summary>
    /// Called when the buff finished initialisation.
    /// </summary>
    public virtual void OnStart()
    {

    }

    /// <summary>
    /// Called when the buff is destroyed.
    /// </summary>
    public virtual void OnDestroy()
    {

    }

    /// <summary>
    /// Called with every tick of the buff. Only used when ticks is greater than 1.
    /// </summary>
    public virtual void OnTick()
    {

    }

    /// <summary>
    /// Called when the Reset Method is executet, independetly from the resetable variable.
    /// </summary>
    public virtual void OnReset()
    {

    }
}
