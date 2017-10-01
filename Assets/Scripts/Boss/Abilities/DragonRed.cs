using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

/// <summary>
/// The Blue Dragon used during scene 11. He will double your healing and increases the maxhealth of every raider by 50 when he is healed to full.
/// </summary>
public class DragonRed : MonoBehaviour, IRaider
{
    private Gamestate gamestate;
    private Boss currentBoss;
    private Coroutine timer;
    private Vector3 startPos;
    private Vector3 endPos;
    public Image background;
    public RectTransform hpBar;
    public CanvasGroup hpGroup;
    private float scaleX;

    private Color32 targetColor = new Color32(102, 255, 255, 255);
    private Color32 notTargetColor = new Color32(160, 160, 160, 255);
    private Color32 deadColor = new Color32(191, 90, 90, 255);
    private float currentHealth;
    public float healMultiplier = 1f;
    public bool alive;
    public bool activated;
    public float maxHealth = 500;
    public float startHealth = 1;
    public float swingTimer;

    /// <summary>
    /// Called on start.
    /// </summary>
    void Start()
    {
        startPos = hpBar.position;
        scaleX = GetComponent<Transform>().localScale.x;
        endPos = new Vector3(hpBar.position.x - hpBar.rect.width * scaleX, hpBar.position.y, hpBar.position.z);
        currentHealth = startHealth;
        UpdateHpBar();
        gamestate = Gamestate.gamestate;
        background.color = notTargetColor;
        if (GameControl.control.talente[18])
            healMultiplier *= 1.05f;

        gameObject.SetActive(false);
    }

    /// <summary>
    /// Called when [mouse down].
    /// </summary>
    void OnMouseDown()
    {
        if (alive)
        {
            SetTarget();
        }
    }

    /// <summary>
    /// Called with every fixed update.
    /// </summary>
    void FixedUpdate()
    {
        if (currentHealth <= 0 && alive)
        {
            Die();
        }
        else if (currentHealth >= maxHealth && alive && !activated)
        {
            activated = true;

            List<IRaider> raiderDict = RaiderDB.GetInstance().GetAllRaiders();

            foreach (IRaider raider in raiderDict)
            {
                raider.ChangeHealmultiplier(2);
                raider.IncreaseMaxHealth(50f);
            }
        }
    }

    /// <summary>
    /// Heals the raider by an amount, i.e. increases the currentHealth.
    /// Triggers HealingTaken events in all buffs.
    /// The currentHealth can not be bigger than maxHealth.
    /// </summary>
    /// <param name="amount">The amount.</param>
    public void Heal(float amount)
    {
        if (alive)
        {
            if (GameControl.control.talente[20] && currentHealth / maxHealth <= 0.3)
                amount *= 1.1f;
            amount *= healMultiplier;
            foreach (IBuff buff in GetComponent<BuffManager>().GetAllBuffsSortetByDuration())
            {
                amount = buff.HealingTaken(amount);
            }
            foreach (IRaider raider in RaiderDB.GetInstance().GetAllRaiders())
            {
                foreach (IBuff buff in raider.GetGameObject().GetComponent<BuffManager>().GetAllBuffsSortetByDuration())
                {
                    amount = buff.GlobalHealingTaken(amount);
                }
            }
            if (amount > maxHealth - currentHealth)
            {
                if (GameControl.control.talente[10])
                    Cloudburst.cloudburst.AddOverheal(currentHealth + amount - maxHealth);
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
    /// Triggers DamageTaken events in all buffs.
    /// </summary>
    /// <param name="amount">The amount.</param>
    public void Damage(float amount)
    {
        if (alive)
        {
            foreach (IBuff buff in GetComponent<BuffManager>().GetAllBuffsSortetByDuration())
            {
                amount = buff.DamageTaken(amount);
                if (currentHealth - amount <= 0)
                {
                    amount = buff.FatalDamage(amount);
                }
            }
            foreach (IRaider raider in RaiderDB.GetInstance().GetAllRaiders())
            {
                foreach (IBuff buff in raider.GetGameObject().GetComponent<BuffManager>().GetAllBuffsSortetByDuration())
                {
                    amount = buff.GlobalDamageTaken(amount);
                }
            }
            currentHealth = currentHealth - amount;
            UpdateHpBar();
        }
    }

    /// <summary>
    /// Heals the raider by an amount in a simple way (i.e. without triggering the cloudburst talent).
    /// </summary>
    /// <param name="amount">The amount.</param>
    /// <param name="combatText">if set to <c>true</c> a combat text will be displayed.</param>
    public void HealSimple(float amount, bool combatText)
    {
        if (alive)
        {
            if (GameControl.control.talente[20] && currentHealth / maxHealth <= 0.3)
                amount *= 1.1f;
            amount *= healMultiplier;
            if (amount > maxHealth - currentHealth)
            {
                currentHealth = maxHealth;
            }
            else
            {
                currentHealth += amount;
            }
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
        }
    }

    /// <summary>
    /// Updates the hp bar.
    /// </summary>
    public void UpdateHpBar()
    {
        hpBar.position = Vector3.Lerp(endPos, startPos, currentHealth / maxHealth);
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
    /// Dies this instance.
    /// </summary>
    public void Die()
    {
        activated = false;
        currentHealth = 100;
        UpdateHpBar();
        alive = false;
        GetComponent<BuffManager>().ClearBuffs();
        gameObject.SetActive(false);
    }

    /// <summary>
    /// Summons this instance.
    /// </summary>
    public void Summon()
    {
        if (alive == false && activated == false)
        {
            currentHealth = startHealth;
            alive = true;
            background.color = notTargetColor;
            UpdateHpBar();
            gameObject.SetActive(true);
        }
    }

    /// <summary>
    /// Sets this as the current boss target (i.e. the target that is hit by auto attacks).
    /// </summary>
    /// <param name="isTarget">if set to <c>true</c> this is the current boss target.</param>
    public void SetBossTarget(bool isTarget)
    {
        if (isTarget == true)
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
}

