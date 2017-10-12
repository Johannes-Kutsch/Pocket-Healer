using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// A Debuff that kills the raider after a fixed duration if not dispelled
/// </summary>
public class KillDebuff : MonoBehaviour, IBuff {
    public Material image;
    public float duration = 10f;
    public float runtime;
    private IRaider raider;
    private Color32 debuffColor = new Color32(170, 0, 255, 255);

    /// <summary>
    /// Called on start.
    /// </summary>
    void Start()
    {
        image = Resources.Load("Kill_Debuff", typeof(Material)) as Material;
        raider = GetComponent<IRaider>();
        raider.ChangeBackgroundColor(debuffColor);
    }

    /// <summary>
    /// Called with every update.
    /// </summary>
    void Update()
    {
        if (!(raider == FindObjectOfType<Gamestate>().GetTarget()))
        {
            raider.ChangeBackgroundColor(debuffColor);
        }
    }

    /// <summary>
    /// Called with every fixed update, i.e. 50 times a second.
    /// </summary>
    void FixedUpdate()
    {
        runtime = runtime + 0.02f;
        if(duration-runtime <= 0)
        {
            raider.Die();
            GetComponent<BuffManager>().DeregisterBuff(this);
            Destroy(this);
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
    public string GetTimeLeft()
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
        if (raider == FindObjectOfType<Gamestate>().GetTarget())
        {
            raider.ChangeBackgroundColor(raider.GetTargetColor());
        } else
        {
            raider.ChangeBackgroundColor(raider.GetNotTargetColor());
        } 
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
        return true;
    }

    /// <summary>
    /// Resets this buff to as if it was freshly applied. This is used
    /// </summary>
    public void Reset()
    {
        runtime = 0;
    }
}
