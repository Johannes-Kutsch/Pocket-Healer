using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Abstract class BuffTicking which implements the basic functionality of most buffs.
/// It triggers the virtual methods OnStart(), OnTick() and OnDestroy(). (Called when the buff finished initialisation, with every tick of the buff and when the buff is destroyed)
/// </summary>
public abstract class BuffTicking : Buff
{
    private float currentTicks = 1f;
    private float tickLength;

    /// <summary>
    /// Called on start.
    /// </summary>
    void Start()
    {
        duration = GetRealDuration();
        image = Resources.Load(GetMaterialName(), typeof(Material)) as Material;

        buffManager = GetComponent<BuffManager>();
        buffManager.RegisterBuff(this);

        raider = GetComponent<Raider>();

        timeLeft = duration;

        tickLength = GetIntervallTicks();

        OnStart();
    }

    /// <summary>
    /// Called with every update. Advances the runtime and checks if the buff should be destroyed. Calculates if the onTick() Method should be called.
    /// </summary>
    void FixedUpdate()
    {
        runtime = runtime + 0.02f;
        timeLeft = duration - runtime;

        if (runtime >= tickLength * (currentTicks))
        {
            OnTick();
            currentTicks++;
        }

        OnFixedUpdate();

        if (runtime > duration)
        {
            Destroy();
        }
    }

    /// <summary>
    /// Resets this buff as if it was freshly applied. This is used for the reset debuffs on dispell talent and when a buff that should only exists once is reaplied.
    /// </summary>
    public override void Reset()
    {
        if (resetable)
        {
            runtime = 0f;
            currentTicks = 0f;
        }

        OnReset();
    }

    /// <summary>
    /// Called with every tick of the buff.
    /// </summary>
    public virtual void OnTick()
    {

    }

    /// <summary>
    /// Gets the intervall for ticks.
    /// </summary>
    /// <returns></returns>
    public abstract float GetIntervallTicks();
}
