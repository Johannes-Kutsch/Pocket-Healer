using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

/// <summary>
/// The Blue Dragon used during scene 11. He will double your healing and increases the maxhealth of every raider by 50 when he is healed to full.
/// </summary>
public class DragonRed : Raider
{

    private readonly float MAXHEALTH = 150f;
    private float startHealth = 1f;

    private bool activated = false;

    /// <summary>
    /// Called on start.
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

            foreach (IRaider raider in RaiderDB.GetInstance().GetAllRaiders()) //increase raider max hp by 50 and increase healmultiplier
            {
                raider.ChangeHealmultiplier(2);
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
        throw new System.Exception("DragonRed does not use a swingtimer.");
    }

}

