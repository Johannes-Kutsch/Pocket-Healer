using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

/// <summary>
/// A short ticking Debuff that deals raidwide damage if the target is healed to full or dies.
/// </summary>
public class StoneOne : MonoBehaviour, IBuff
{
    private List<IRaider> targetDict;
    public float exploDmg = 90f;
    public Material image;
    public float duration = 10f;
    public float runtime;
    public float damagePerTick = 30f;
    public float tickLength = 2f;
    private int ticksSinceLastEffect = 0;
    private IRaider raider;
    private Color32 debuffColor = new Color32(255, 50, 50, 255);

    /// <summary>
    /// Called on start.
    /// </summary>
    void Start()
    {
        image = Resources.Load("Stein_1", typeof(Material)) as Material;
        if (GameControl.control.difficulty == 0)
        {
            damagePerTick *= GameControl.control.easyMultiplyer;
            exploDmg *= GameControl.control.easyMultiplyer;
        }
        raider = GetComponent<IRaider>();
        StartCoroutine(ApplyDamage());

        //raider.ChangeBackgroundColor(debuffColor);
    }

    /*   void Update()
       {
           if (!(raider == FindObjectOfType<Gamestate>().GetTarget()))
           {
               raider.ChangeBackgroundColor(debuffColor);
           }
       }*/

    /// <summary>
    /// Called with every fixed update
    /// </summary>
    void FixedUpdate()
    {
        runtime = runtime + 0.02f;
        if (raider.GetHealth() >= 1f)
        {
            targetDict = RaiderDB.GetInstance().GetAllRaiders();
            foreach (IRaider target in targetDict)
                target.Damage(exploDmg);

            Debug.Log("StoneOne Exploded with at 100% Health.");
            Destroy();
        }
    }

    /// <summary>
    /// Corroutine to apply the singeltarget damage.
    /// </summary>
    /// <returns></returns>
    IEnumerator ApplyDamage()
    {
        float ticks = (duration / tickLength);
        for (ticksSinceLastEffect = 0; ticksSinceLastEffect < ticks; ticksSinceLastEffect++)
        {
            yield return new WaitForSeconds(tickLength);
            raider.Damage(damagePerTick);
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
      /*  if (raider == FindObjectOfType<Gamestate>().GetTarget())
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
        bool hasGuardianSpirit = false;
        foreach (IBuff buff in GetComponent<BuffManager>().GetAllBuffsSortetByDuration())
        {
            if (buff.GetType() == typeof(GuardianSpiritBuff) || buff.GetType() == typeof(GuardianSpiritBuffInvis))
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

            Debug.Log("StoneOne Exploded with Fatal Damage.");

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
