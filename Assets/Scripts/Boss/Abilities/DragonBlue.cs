using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

/// <summary>
/// The Blue Dragon used during scene 11. He will regenerate mana when healed to full.
/// </summary>
public class DragonBlue : Raider
{
    private readonly float MAXHEALTH = 150f;
    private float swingTimer = 10f;

    private float startHealth = 1f;
    private float manaIncrease = 120f;

    private bool activated = false;

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
        if (currentHealth >= MAXHEALTH && IsAlive() && !activated) //activate
        {
            activated = true;
            base.canSwing = true;

            foreach (IRaider raider in RaiderDB.GetInstance().GetAllRaiders()) //increase raider max hp by 50
            {
                raider.IncreaseMaxHealth(50f);
                raider.Heal(50);
            }
        }
    }

    /// <summary>
    /// Called when the raider triggers a swing.
    /// </summary>
    public override void OnSwing()
    {
        Gamestate.gamestate.IncreaseMana(manaIncrease);
    }

    /// <summary>
    /// Summons this instance.
    /// </summary>
    public void Summon()
    {
        if (!IsAlive())
        {
            base.currentHealth = startHealth;
            SetAlive(true);
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

