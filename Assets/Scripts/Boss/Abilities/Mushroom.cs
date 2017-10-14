using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// The Mushroom used during Scene 8. Once healed to full he periodically takes damage and heals your party until he dies.
/// </summary>
public class Mushroom : Raider
{
    public readonly float MAXHEALTH = 200;
    public readonly float swingTimer;

    private bool canSwing = true;
    private float currentDmg;
    private float currentHealth;
    public float healMultiplier = 1f;
    public bool activated;
    
    public float startHealth = 100;
    public float healAmount = 25f;
    
    public float startDmg;
    public float multiplier;


    /// <summary>
    /// Called on Start.
    /// </summary>
    public override void OnStart()
    {
        base.maxHealth = MAXHEALTH;
        gameObject.SetActive(false);
        SetAlive(false);
    }

    /// <summary>
    /// Called with every fixed update
    /// </summary>
    public override void OnFixedUpdate()
    {
        if (currentHealth >= MAXHEALTH && IsAlive() && !activated) //activate
        {
            activated = true;
            canSwing = true;
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
        if(!IsAlive())
        {
            currentHealth = startHealth;
            SetAlive(true);
            currentDmg = startDmg;
            background.color = notTargetColor;
            UpdateHpBar();
            gameObject.SetActive(true);
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

