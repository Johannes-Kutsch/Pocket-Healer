using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

public class Boss : MonoBehaviour{
    private Gamestate gamestate;
    public string name;
    public Text nameText;
    public Text emoteText;
    private Vector3 startPos;
    private Vector3 endPos;
    private float emoteMaxTime = 2f;
    private float emoteCurrentTime;
    public Canvas UI;
    private float scaleX;
    public RectTransform hpTransform;


    public float maxHealth;
    public float currentHealth;

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

    void FixedUpdate()
    {
        if (emoteCurrentTime < emoteMaxTime)
        {
            emoteCurrentTime += 0.02f;
        }
        else
        {
            emoteText.color = new Color32(0, 0, 0, 0);
        }
    }

    public void TakeDamage(float schaden)
    {
        currentHealth = currentHealth - schaden;
        SetzeHpBar();
    }

    public void ErhalteHeilung(float heilung)
    {
        if (heilung > maxHealth - currentHealth)
        {
            currentHealth = maxHealth;
        }
        else
        {
            currentHealth += heilung;
        }
        SetzeHpBar();
    }

    public void SetzeHpBar()
    {
        hpTransform.position = Vector3.Lerp(endPos, startPos, currentHealth / maxHealth);
    }

    public float GetCurrentHp()
    {
        return currentHealth;
    }

    public void SetEmoteText(string emote)
    {
        emoteText.text = name + emote;
        emoteCurrentTime = 0;
        emoteText.color = new Color32(0, 0, 0, 255);
    }
}