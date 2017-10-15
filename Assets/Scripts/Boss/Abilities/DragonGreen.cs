using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

/// <summary>
/// The Green Dragon used during scene 11. He will will split half of your healing evenly among every raider when healed to full.
/// </summary>
public class DragonGreen : Raider
{
    private readonly float MAXHEALTH = 150f;
    private float startHealth = 1f;

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

            foreach (Raider raider in RaiderDB.GetInstance().GetAllRaiders()) //increase raider max hp by 50 and apply invis buff
            {
                raider.GetGameObject().AddComponent<GreenBuffInvis>();
                raider.IncreaseMaxHealth(50f);
                raider.Heal(50);
            }
        }
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
        throw new System.Exception("DragonGreen does not use a swingtimer.");
    }
}

