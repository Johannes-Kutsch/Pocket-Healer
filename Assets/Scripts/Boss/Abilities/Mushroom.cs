using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// The Mushroom used during Scene 8. Once healed to full he periodically takes damage and heals your party until he dies.
/// </summary>
public class Mushroom : Raider
{
    private bool canSwing = true;
    private float currentDmg;
    private float currentHealth;
    public float healMultiplier = 1f;
    public bool activated;
    public float maxHealth = 200;
    public float startHealth = 100;
    public float healAmount = 25f;
    public float swingTimer;
    public float startDmg;
    public float multiplier;


    /// <summary>
    /// Called on Start.
    /// </summary>
    public override void OnStart()
    {
        gameObject.SetActive(false);
        base.swingTimer = swingTimer;
    }

    /// <summary>
    /// Called with every fixed update
    /// </summary>
    void FixedUpdate()
    {
        if (currentHealth <= 0 && IsAlive())
        {
            Die();
        }
        else if (currentHealth >= maxHealth && IsAlive() && !activated) //activate
        {
            activated = true;
            canSwing = true;
        }
        else if (canSwing && IsAlive() && activated) //heal party and damage self.
        {

            foreach (IRaider raider in RaiderDB.GetInstance().GetAllRaiders())
            {
                raider.HealSimple(healAmount, true);
            }

            Damage(currentDmg);
            currentDmg += multiplier;
        } 
    }

    /// <summary>
    /// Called when the Die() method in the base class is called.
    /// </summary>
    public override void OnDie()
    {
        activated = false;
        gameObject.SetActive(false);
    }

    /// <summary>
    /// Summons this instance.
    /// </summary>
    public void Summon()
    {
        if(!IsAlive() && !activated)
        {
            currentHealth = startHealth;
            SetAlive(true);
            currentDmg = startDmg;
            background.color = notTargetColor;
            UpdateHpBar();
            gameObject.SetActive(true);
        }
    }
}

