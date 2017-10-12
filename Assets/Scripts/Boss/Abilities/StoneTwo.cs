﻿using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

/// <summary>
/// A ticking debuff without duration that expires if the target is healed to full and explodes if the target dies.
/// </summary>
public class StoneTwo : MonoBehaviour, IBuff
{
    public Material image;
    public float damagePerTick = 30f;
    public float tickLength = 3f;
    private IRaider raider;
    private Color32 debuffColor = new Color32(100, 255, 100, 255);
    public float exploDmg = 90f;

    /// <summary>
    /// Called on start.
    /// </summary>
    void Start()
    {
        image = Resources.Load("Stein_2", typeof(Material)) as Material;
        if (GameControl.control.difficulty == 0)
        {
            damagePerTick *= GameControl.control.easyMultiplyer;
        }

        raider = GetComponent<IRaider>();
        StartCoroutine(ApplyDamage());

        //raider.ChangeBackgroundColor(debuffColor);
    }

    /*void Update()
    {
        if (!(raider == FindObjectOfType<Gamestate>().GetTarget()))
        {
            raider.ChangeBackgroundColor(debuffColor);
        }
    }*/

    /// <summary>
    /// Called on every fixed update.
    /// </summary>
    void FixedUpdate()
    {
        if (raider.GetHealth() >= 1f)
        {
            GetComponent<BuffManager>().DeregisterBuff(this);
            Destroy(this);
        }
    }

    /// <summary>
    /// Corroutine used to apply the ticking damage.
    /// </summary>
    /// <returns></returns>
    IEnumerator ApplyDamage()
    {
        while (1 == 1)
        {
            yield return new WaitForSeconds(tickLength);
            raider.Damage(damagePerTick);
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
    public string GetRemainingDuration()
    {
        return " ";
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
       /* if (raider == FindObjectOfType<Gamestate>().GetTarget())
        {
            raider.ChangeBackgroundColor(raider.GetTargetColor());
        }
        else
        {
            raider.ChangeBackgroundColor(raider.GetNotTargetColor());
        }*/

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
        bool hasGuardianSpirit = false;
        foreach (IBuff buff in GetComponent<BuffManager>().GetAllBuffsSortetByDuration())
        {
            if(buff.GetType() == typeof(GuardianSpiritBuff) || buff.GetType() == typeof(GuardianSpiritBuffInvis))
            {
                hasGuardianSpirit = true;
            }
        }

        if (!hasGuardianSpirit)
        {
            List<IRaider> targetDict = RaiderDB.GetInstance().GetAllRaiders();
            targetDict.Remove(raider);
            foreach (IRaider target in targetDict)
            {
                target.Damage(exploDmg);
            }
            Debug.Log("StoneTwo Exploded with Fatal Damage.");
            Destroy();
        }
        
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
}