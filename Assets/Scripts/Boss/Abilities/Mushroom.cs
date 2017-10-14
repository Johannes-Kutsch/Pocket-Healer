using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// The Mushroom used during Scene 8. Once healed to full he periodically takes damage and heals your party until he dies.
/// </summary>
public class Mushroom : Raider
{
    private readonly float MAXHEALTH = 200f;
    private readonly float swingTimer = 1.5f;

    private float startHealth = 100f;
    private float healAmount = 25f;

    private float currentDmg = 20f;
    private float startDmg = 20f;
    private float dmgMultiplier = 15f;


    /// <summary>
    /// Called on Start.
    /// </summary>
    public override void OnStart()
    {
        base.canSwing = false;
        base.maxHealth = MAXHEALTH;
        base.currentHealth = startHealth;
        SetAlive(false);

        gameObject.SetActive(false);
    }

    /// <summary>
    /// Called with every fixed update
    /// </summary>
    public override void OnFixedUpdate()
    {
        if (currentHealth >= MAXHEALTH && IsAlive() && !base.canSwing) //activate
        {
            base.canSwing = true;
        }
    }

    /// <summary>
    /// Called when the raider triggers a swing.
    /// </summary>
    public override void OnSwing()
    {
        foreach (IRaider raider in RaiderDB.GetInstance().GetAllRaiders())
        {
            raider.HealSimple(healAmount, true);
        }

        Damage(currentDmg);
        currentDmg += dmgMultiplier;
    }

    /// <summary>
    /// Called when the Die() method in the base class is called.
    /// </summary>
    public override void OnDie()
    {
        base.canSwing = false;
        gameObject.SetActive(false);

        CombatText[] combatTexts = GetComponentsInChildren<CombatText>();
        foreach (CombatText combatText in combatTexts)
        {
            Destroy(combatText.gameObject);
        }
    }

    /// <summary>
    /// Summons this instance.
    /// </summary>
    public void Summon()
    {
        if(!IsAlive())
        {
            base.currentHealth = startHealth;
            SetAlive(true);
            currentDmg = startDmg;
            base.background.color = notTargetColor;
            base.hpGroup.alpha = 1;
            gameObject.SetActive(true);
            UpdateHpBar();
        }
    }

    /// <summary>
    /// Gets the swing timer.
    /// </summary>
    /// <returns></returns>
    public override float GetSwingTimer()
    {
        return swingTimer;
    }
}

