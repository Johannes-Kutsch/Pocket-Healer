﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Raider : MonoBehaviour
{
    public CanvasGroup hpGroup; //The hp bar background and the mask
    public RectTransform hpBar;
    public Image background; //The unit frame background

    public Gamestate gamestate; //A reference to the gamestate
    private Boss currentBoss; //A reference to the current boss

    private Vector3 hPBarStartPos;
    private Vector3 hpBarEndPos;
    private float scaleX;

    private Color32 targetColor = new Color32(102, 255, 255, 255);
    public Color32 notTargetColor = new Color32(160, 160, 160, 255);
    private Color32 deadColor = new Color32(191, 90, 90, 255);

    public float maxHealth;
    public float currentHealth;
    private bool alive = true;
    private float healMultiplier = 1f; //A global heal multiplyer i.e. used by talents that increase flat healing

    public bool canSwing = true;
    private Coroutine timer;

    /// <summary>
    /// Called on Start.
    /// Sets some variables, calculates the hpBarEndPos and registers the tank in the database.
    /// </summary>
    void Start () {
        gamestate = Gamestate.gamestate;
        currentHealth = maxHealth;
        hPBarStartPos = hpBar.position;
        scaleX = GetComponent<Transform>().localScale.x;
        hpBarEndPos = new Vector3(hpBar.position.x - hpBar.rect.width * scaleX, hpBar.position.y, hpBar.position.z);
        background.color = notTargetColor;
        
        if (GameControl.control.talente[18])
            healMultiplier *= 1.05f; //+5% falt Healing Talent

        OnStart();
    }

    /// <summary>
    /// Called when [mouse down].
    /// Sets the dd as current target.
    /// </summary>
    void OnMouseDown()
    {
        if (alive)
        {
            SetTarget();
        }
    }

    /// <summary>
    /// Called in each simulation tick i.e. 50 times a second.
    /// Checks if the dd is dead and if the dd can attack the boss.
    /// </summary>
    void FixedUpdate()
    {
        if (currentHealth <= 0 && alive)
        {
            Die();
        }

        if (canSwing && alive && !Gamestate.gamestate.GetPaused())
        {
            OnSwing();
            StartCoroutine(Wait(GetSwingTimer()));
        }

        OnFixedUpdate();
    }

    /// <summary>
    /// Heals the raider by an amount, i.e. increases the currentHealth.
    /// The currentHealth can not be bigger than maxHealth.
    /// Updates the Hp Bar.
    /// </summary>
    /// <param name="amount">The amount.</param>
    public void Heal(float amount)
    {
        if (alive)
        {
            if (GameControl.control.talente[20] && currentHealth / maxHealth <= 0.3) //increase healing by 25% if health is <= 30% and talent is choosen
                amount *= 1.25f;

            amount *= healMultiplier; //increase Healing by a heal multiplyer (i.e. flat 5% Heal talent)

            foreach (IBuff buff in GetComponent<BuffManager>().GetAllBuffsSortetByDuration())
            {
                amount = buff.OnHealingTaken(amount); //call the HealingTaken method in every buff
            }

            foreach (Raider raider in RaiderDB.GetInstance().GetAllRaiders())
            {
                foreach (IBuff buff in raider.GetGameObject().GetComponent<BuffManager>().GetAllBuffsSortetByDuration())
                {
                    amount = buff.OnGlobalHealingTaken(amount); //call the GlobalHealingTaken method in every buff for every raider
                }
            }

            CombatTextManager.manager.CreateHealText(transform.position, this, amount);

            if (amount > maxHealth - currentHealth)
            {
                if (GameControl.control.talente[10])
                    Cloudburst.cloudburst.AddOverheal(currentHealth + amount - maxHealth); //add overheal to cloudburst if the talent is choosen
                currentHealth = maxHealth;
            }

            else
            {
                currentHealth += amount;
            }

            UpdateHpBar();
        }
    }

    /// <summary>
    /// Damages the raider by an amount, i.e. decreases the currentHealth.
    /// </summary>
    /// <param name="amount">The amount.</param>
    public void Damage(float amount)
    {
        if (alive)
        {
            foreach (IBuff buff in GetComponent<BuffManager>().GetAllBuffsSortetByDuration())
            {
                amount = buff.OnDamageTaken(amount); //call the DamageTaken method in every buff
            }

            foreach (IBuff buff in GetComponent<BuffManager>().GetAllBuffsSortetByDuration())
            {
                if (currentHealth - amount <= 0)
                {
                    amount = buff.OnFatalDamage(amount); //if the damage would kill the raider call the FatalDamage method in every buff
                }
            }

            foreach (Raider raider in RaiderDB.GetInstance().GetAllRaiders())
            {
                foreach (IBuff buff in raider.GetGameObject().GetComponent<BuffManager>().GetAllBuffsSortetByDuration())
                {
                    amount = buff.OnGlobalDamageTaken(amount);  //call the GlobalDamageTaken method in every buff for every raider
                }
            }

            CombatTextManager.manager.CreateDmgText(transform.position, this, amount);
            currentHealth = currentHealth - amount;
            UpdateHpBar();
        }
    }

    /// <summary>
    /// Increases the hp in a simple way (i.e. without triggering the cloudburst talent and without calling healing taken methods in buffs).
    /// </summary>
    /// <param name="amount">The amount.</param>
    /// <param name="combatText">if set to <c>true</c> a combat text will be displayed.</param>
    public void HealSimple(float amount, bool combatText)
    {
        if (alive)
        {
            if (GameControl.control.talente[20] && currentHealth / maxHealth <= 0.3)  //increase healing by 25% if health is <= 30% and talent is choosen
                amount *= 1.1f;

            amount *= healMultiplier; //increase Healing by a heal multiplyer (i.e. flat 5% Heal talent)

            if (amount > maxHealth - currentHealth)
            {
                currentHealth = maxHealth;
            }
            else
            {
                currentHealth += amount;
            }

            if (combatText)
                CombatTextManager.manager.CreateHealText(transform.position, this, amount);

            UpdateHpBar();
        }
    }

    /// <summary>
    /// Damages the raider by an amount in a simple way (i.e. without triggering the DamageTaken events in buffs).
    /// </summary>
    /// <param name="amount">The amount.</param>
    /// <param name="combatText">if set to <c>true</c> a combat text will be displayed.</param>
    public void DamageSimple(float amount, bool combatText)
    {
        if (alive)
        {
            currentHealth = currentHealth - amount;
            UpdateHpBar();
            if (combatText)
                CombatTextManager.manager.CreateDmgText(transform.position, this, amount);
        }
    }

    /// <summary>
    /// Updates the hp bar.
    /// </summary>
    public void UpdateHpBar()
    {
        hpBar.position = Vector3.Lerp(hpBarEndPos, hPBarStartPos, currentHealth / maxHealth);
    }

    /// <summary>
    /// Changes the color of the background.
    /// </summary>
    /// <param name="backgroundColor">Color of the background.</param>
    public void ChangeBackgroundColor(Color32 backgroundColor)
    {
        background.color = backgroundColor;
    }

    /// <summary>
    /// Sets this as the current target.
    /// </summary>
    public void SetTarget()
    {
        if (gamestate.HasTarget() && gamestate.GetTarget().IsAlive())
        {
            gamestate.GetTarget().ChangeBackgroundColor(notTargetColor);
        }
        else if (gamestate.HasTarget() && !gamestate.GetTarget().IsAlive())
        {
            gamestate.GetTarget().ChangeBackgroundColor(deadColor);
        }

        gamestate.SetTarget(this);
        ChangeBackgroundColor(targetColor);
    }

    /// <summary>
    /// Gets the current health.
    /// </summary>
    /// <returns>
    /// the current health
    /// </returns>
    public float GetHealth()
    {
        return currentHealth / maxHealth;
    }

    /// <summary>
    /// Gets the game object.
    /// </summary>
    /// <returns></returns>
    public GameObject GetGameObject()
    {
        return this.gameObject;
    }

    /// <summary>
    /// Waits the specified time to wait.
    /// </summary>
    /// <param name="timeToWait">The time to wait.</param>
    /// <returns></returns>
    IEnumerator Wait(float timeToWait)
    {
        canSwing = false;
        yield return new WaitForSeconds(timeToWait);
        canSwing = true;
    }

    /// <summary>
    /// Determines whether this instance is alive or not.
    /// </summary>
    /// <returns>
    ///   <c>true</c> if this instance is alive; otherwise, <c>false</c>.
    /// </returns>
    public bool IsAlive()
    {
        return alive;
    }

    /// <summary>
    /// Sets alive.
    /// </summary>
    /// <param name="alive">if set to <c>true</c> [alive].</param>
    public void SetAlive(bool alive)
    {
        this.alive = alive;
    }

    /// <summary>
    /// Dies this instance.
    /// </summary>
    public void Die()
    {
        alive = false;
        GetComponent<BuffManager>().ClearBuffs();
        hpGroup.alpha = 0;
        background.color = deadColor;
        OnDie();
    }

    /// <summary>
    /// Sets this as the current boss target (i.e. the target that is hit by auto attacks).
    /// </summary>
    /// <param name="isTarget">if set to <c>true</c> this is the current boss target.</param>
    public void SetBossTarget(bool isTarget)
    {
        if (isTarget)
        {
            background.rectTransform.sizeDelta = new Vector2(0.955f, 0.925f);
        }
        else
        {
            background.rectTransform.sizeDelta = new Vector2(1, 1);
        }
    }

    /// <summary>
    /// Gets the color that is used if the instance is the target.
    /// </summary>
    /// <returns></returns>
    public Color32 GetTargetColor()
    {
        return targetColor;
    }

    /// <summary>
    /// Sets the color that is used if the instance is the target.
    /// </summary>
    /// <param name="color"></param>
    public void SetTargetColor(Color32 color)
    {
        targetColor = color;
    }

    /// <summary>
    /// Gets the color that is used if the instance is not the target.
    /// </summary>
    /// <returns></returns>
    public Color32 GetNotTargetColor()
    {
        return notTargetColor;
    }

    /// <summary>
    /// Sets the color that is used if the instance is not the target.
    /// </summary>
    /// <param name="color"></param>
    public void SetNotTargetColor(Color32 color)
    {
        notTargetColor = color;
    }

    /// <summary>
    /// Multiplies the Healmultiplier with the a value.
    /// </summary>
    /// <param name="multiplier">The value.</param>
    public void ChangeHealmultiplier(float value)
    {
        healMultiplier *= value;
    }

    /// <summary>
    /// Increases the maximum health.
    /// </summary>
    /// <param name="health">The amount the health is increased by.</param>
    public void IncreaseMaxHealth(float health)
    {
        maxHealth += health;
    }

    /// <summary>
    /// Gets the swing timer.
    /// </summary>
    /// <returns></returns>
    public abstract float GetSwingTimer();

    /// <summary>
    /// Called when the Start() method of the base class finished initialisation.
    /// </summary>
    public virtual void OnStart()
    {

    }

    /// <summary>
    /// Called when the Die() method in the base class is called.
    /// </summary>
    public virtual void OnDie()
    {

    }

    /// <summary>
    /// Called when the object is removed.
    /// </summary>
    public virtual void OnDestroy()
    {

    }

    /// <summary>
    /// Called when the raider triggers a swing.
    /// </summary>
    public virtual void OnSwing()
    {

    }

    /// <summary>
    /// Called with every fixed update.
    /// </summary>
    public virtual void OnFixedUpdate()
    {

    }
}
