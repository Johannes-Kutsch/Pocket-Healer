using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Pilz : MonoBehaviour, IRaider
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
    private bool canSwing = true;
    private Color32 targetColor = new Color32(102, 255, 255, 255);
    private Color32 keinTargetColor = new Color32(160, 160, 160, 255);
    private Color32 deadColor = new Color32(191, 90, 90, 255);
    private float actualDmg;
    private float currentHealth;
    public float healMultiplyer = 1f;
    public bool alive;
    public bool activated;
    public float maxHealth = 200;
    public float startHealth = 100;
    public float healAmount = 25f;
    public float swingTimer;
    public float startDmg;
    public float multiplyer;

    void Start()
    {
        currentHealth = maxHealth;
        startPos = hpBar.position;
        scaleX = GetComponent<Transform>().localScale.x;
        endPos = new Vector3(hpBar.position.x - hpBar.rect.width * scaleX, hpBar.position.y, hpBar.position.z);
        gamestate = Gamestate.gamestate;
        if (GameControl.control.talente[18])
            healMultiplyer *= 1.05f;
        gameObject.SetActive(false);
    }

    void OnMouseDown()
    {
        if (alive)
        {
            SetTarget();
        }
    }

    void FixedUpdate()
    {
        if (currentHealth <= 0 && alive)
        {
            Die();
        }
        else if (currentHealth >= maxHealth && alive && !activated)
        {
            activated = true;
            canSwing = true;
        }
        else if (canSwing && alive && activated)
        {
            timer = StartCoroutine(Timer(swingTimer));
            foreach (IRaider raider in RaiderDB.GetInstance().GetAllRaider())
            {
                raider.HealSimple(healAmount, true);
            }
            Damage(actualDmg);
            actualDmg += multiplyer;
        } 
    }

    public void Heal(float heilung)
    {
        if (alive)
        {
            if (GameControl.control.talente[20] && currentHealth / maxHealth <= 0.3)
                heilung *= 1.1f;
            heilung *= healMultiplyer;
            foreach (IBuff buff in GetComponent<BuffManager>().GetAllBuffsSortetByDuration())
            {
                heilung = buff.HealingTaken(heilung);
            }
            foreach (IRaider raider in RaiderDB.GetInstance().GetAllRaider())
            {
                foreach (IBuff buff in raider.GetGameObject().GetComponent<BuffManager>().GetAllBuffsSortetByDuration())
                {
                    heilung = buff.GlobalHealingTaken(heilung);
                }
            }
            if (heilung > maxHealth - currentHealth)
            {
                if (GameControl.control.talente[10])
                    Cloudburst.cloudburst.AddOverheal(currentHealth + heilung - maxHealth);
                currentHealth = maxHealth;
            }
            else
            {
                currentHealth += heilung;
            }
            UpdateHpBar();
        }
    }

    public void Damage(float schaden)
    {
        if (alive)
        {
            foreach (IBuff buff in GetComponent<BuffManager>().GetAllBuffsSortetByDuration())
            {
                schaden = buff.DamageTaken(schaden);
                if (currentHealth - schaden <= 0)
                {
                    schaden = buff.FatalDamage(schaden);
                }
            }
            foreach (IRaider raider in RaiderDB.GetInstance().GetAllRaider())
            {
                foreach (IBuff buff in raider.GetGameObject().GetComponent<BuffManager>().GetAllBuffsSortetByDuration())
                {
                    schaden = buff.GlobalDamageTaken(schaden);
                }
            }
            currentHealth = currentHealth - schaden;
            UpdateHpBar();
        }
    }

    public void HealSimple(float heilung, bool combatText)
    {
        if (alive)
        {
            if (GameControl.control.talente[20] && currentHealth / maxHealth <= 0.3)
                heilung *= 1.1f;
            heilung *= healMultiplyer;
            if (heilung > maxHealth - currentHealth)
            {
                currentHealth = maxHealth;
            }
            else
            {
                currentHealth += heilung;
            }
        }
    }

    public void DamageSimple(float schaden, bool combatText)
    {
        if (alive)
        {
            currentHealth = currentHealth - schaden;
            UpdateHpBar();
        }
    }

    public void UpdateHpBar()
    {
        hpBar.position = Vector3.Lerp(endPos, startPos, currentHealth / maxHealth);
    }

    public void ChangeBackgroundColor(Color32 backgroundColor)
    {
        background.color = backgroundColor;
    }

    public void SetTarget()
    {
        if (gamestate.HasTarget() && gamestate.GetTarget().IsAlive())
        {
            gamestate.GetTarget().ChangeBackgroundColor(keinTargetColor);
        }
        else if (gamestate.HasTarget() && !gamestate.GetTarget().IsAlive())
        {
            gamestate.GetTarget().ChangeBackgroundColor(deadColor);
        }
        gamestate.SetTarget(this);
        ChangeBackgroundColor(targetColor);
    }

    public float GetHealth()
    {
        return currentHealth / maxHealth;
    }

    public GameObject GetGameObject()
    {
        return this.gameObject;
    }

    IEnumerator Timer(float timeToWait)
    {
        canSwing = false;
        yield return new WaitForSeconds(timeToWait);
        canSwing = true;
    }

    public bool IsAlive()
    {
        return alive;
    }

    public void Die()
    {

        actualDmg = startDmg;
        activated = false;
        currentHealth = 100;
        UpdateHpBar();
        alive = false;
        GetComponent<BuffManager>().ClearBuffs();
        gameObject.SetActive(false);
    }

    public void Summon()
    {
        if(alive == false && activated == false)
        {
            currentHealth = startHealth;
            alive = true;
            actualDmg = startDmg;
            background.color = keinTargetColor;
            UpdateHpBar();
            gameObject.SetActive(true);
        }
    }

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

    public Color32 GetTargetColor()
    {
        return targetColor;
    }

    public Color32 GetNotTargetColor()
    {
        return keinTargetColor;
    }

    public void ChangeHealmultiplyer(float multiplyer)
    {
        healMultiplyer *= multiplyer;
    }

    public void GetMaxHealth(float health)
    {
        maxHealth += health;
    }
}

