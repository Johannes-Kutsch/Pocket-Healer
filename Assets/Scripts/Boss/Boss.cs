using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

/// <summary>
/// Script that controlls the boss, including the hp bar, name, emotes and health.
/// </summary>
public class Boss : MonoBehaviour{
    private Gamestate gamestate;
    private Vector3 startPos;
    private Vector3 endPos;
    private float scaleX;

    public Text nameText;
    public Text emoteText;
    public RectTransform hpTransform;
    public Canvas UI;
    public string name;
    public float maxHealth;
    public float currentHealth;

    private float emoteMaxTime = 2f;
    private float emoteCurrentTime;

    /// <summary>
    /// Called on start.
    /// </summary>
    void Start()
    {
        gamestate = Gamestate.gamestate;
        currentHealth = maxHealth;
        emoteCurrentTime = emoteMaxTime;
        gamestate.SetBoss(this);
        scaleX = UI.GetComponent<RectTransform>().localScale.x;
        startPos = hpTransform.position;
        endPos = new Vector3(hpTransform.position.x - hpTransform.rect.width * scaleX, hpTransform.position.y, hpTransform.position.z);
        nameText.text = name;
    }

    /// <summary>
    /// Called with every fixed update.
    /// </summary>
    void FixedUpdate()
    {
        if (emoteCurrentTime < emoteMaxTime)
        {
            emoteCurrentTime += 0.02f;
        }
        else //hide the emote text.
        {
            emoteText.color = new Color32(0, 0, 0, 0);
        }
    }

    /// <summary>
    /// Takes damage.
    /// </summary>
    /// <param name="amount">The amount.</param>
    public void TakeDamage(float amount)
    {
        currentHealth = currentHealth - amount;
        SetHpBar();
    }

    /// <summary>
    /// Takes healing.
    /// </summary>
    /// <param name="amount">The amount.</param>
    public void TakeHealing(float amount)
    {
        if (amount > maxHealth - currentHealth)
        {
            currentHealth = maxHealth;
        }
        else
        {
            currentHealth += amount;
        }

        SetHpBar();
    }

    /// <summary>
    /// Sets the hp bar.
    /// </summary>
    public void SetHpBar()
    {
        hpTransform.position = Vector3.Lerp(endPos, startPos, currentHealth / maxHealth);
    }

    /// <summary>
    /// Gets the current hp.
    /// </summary>
    /// <returns></returns>
    public float GetCurrentHp()
    {
        return currentHealth;
    }

    /// <summary>
    /// Sets the emote text.
    /// </summary>
    /// <param name="emote">The emote.</param>
    public void SetEmoteText(string emote)
    {
        emoteText.text = name + emote;
        emoteCurrentTime = 0;
        emoteText.color = new Color32(0, 0, 0, 255);
    }
}