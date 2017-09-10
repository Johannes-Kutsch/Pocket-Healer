using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

public class Dispell : MonoBehaviour, ISpell {
    public Gamestate gamestate;
    public Image cooldownOverlay;
    private IRaider target;
    private float manaKosten = 10f;
    private float castTime = 0f;
    private float cooldown = 0f;
    private float cooldownTimer;
    private float cooldownMax;
    private bool onCooldown = false;
    private string spellName = "Dispell";

    private AudioSource source;
    private AudioClip castSound;

    void Start()
    {
        castSound = Resources.Load("DispellCast", typeof(AudioClip)) as AudioClip;
        gamestate = Gamestate.gamestate;
        cooldownOverlay = GetComponentInChildren<Image>();
        gamestate.AddSpell(this);
        cooldownTimer = cooldown;
        source = GetComponent<AudioSource>();
    }

    void FixedUpdate()
    {
        if (cooldownTimer >= cooldownMax)
        {
            onCooldown = false;
            cooldownOverlay.color = new Color32(160, 160, 160, 0);
        }
        else
        {
            cooldownTimer += 0.02f;
            cooldownOverlay.fillAmount = cooldownTimer / cooldownMax;
        }
    }

    void OnMouseDown()
    {
        Cast();
    }

    public void Cast()
    {
        target = gamestate.GetTarget();
        if (gamestate.HasTarget() && !gamestate.GetCastBar().IsCasting() && !gamestate.GetGcdBar().GetGcd() && !onCooldown && target.IsAlive())
            if (gamestate.DecreaseMana(manaKosten))
            {
                cooldownTimer = 0f;
                cooldownMax = cooldown;
                onCooldown = true;
                cooldownOverlay.color = new Color32(160, 160, 160, 160);
                source.PlayOneShot(castSound, GameControl.control.soundMultiplyer);
                gamestate.GetGcdBar().StartGcd();
                RemoveDebuff();
            }
    }


    private void RemoveDebuff()
    {
        BuffManager manager = target.GetGameObject().GetComponent<BuffManager>();
        List <IBuff> buffDict = manager.GetAllBuffsSortetByDuration();
        foreach(IBuff buff in buffDict)
        {
            if(buff.IsDispellable())
            {
                manager.DeregisterBuff(buff);
                buff.Destroy();
            }
            else if(buff.IsBuff() && GameControl.control.talente[6])
            {
                buff.Reset();
            }
        }
    }

    public void StartGcd()
    {
        if (!onCooldown || cooldownMax - cooldownTimer < gamestate.GetGcdBar().GetGcdTime())
        {
            cooldownMax = gamestate.GetGcdBar().GetGcdTime();
            onCooldown = true;
            cooldownTimer = 0;
            cooldownOverlay.color = new Color32(160, 160, 160, 160);
        }
    }

    public void RemoveSpellFromButton()
    {
        GetComponent<MeshRenderer>().material = null;
        Destroy(this);
    }
}
