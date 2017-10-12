using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

/// <summary>
/// Removes a dispellable debuff from a friendly target.
/// </summary>
public class Dispell : MonoBehaviour, ISpell {
    public Gamestate gamestate;
    public Image cooldownOverlay;
    private IRaider target;
    private float cooldownTimer;
    private float cooldownMax;
    private bool onCooldown = false;
    private string spellName = "Dispell";

    private float manaCost = 10f;
    private float castTime = 0f;
    private float cooldown = 0f;

    private AudioSource source;
    private AudioClip castSound;

    /// <summary>
    /// Called on start. Set some sounds and find the gamestate, the cooldownoverlay and the audiosource.
    /// </summary>
    void Start()
    {
        castSound = Resources.Load("DispellCast", typeof(AudioClip)) as AudioClip;
        gamestate = Gamestate.gamestate;
        cooldownOverlay = GetComponentInChildren<Image>();
        gamestate.AddSpell(this);
        cooldownTimer = cooldown;
        source = GetComponent<AudioSource>();
    }

    /// <summary>
    /// Called with every fixed update. Advance the cooldown and draw the cooldownoverlay.
    /// </summary>
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

    /// <summary>
    /// Called when [mouse down].
    /// </summary>
    void OnMouseDown()
    {
        Cast();
    }

    /// <summary>
    /// Check if we can cast the spell and cast it.
    /// </summary>
    public void Cast()
    {
        target = gamestate.GetTarget();
        if (gamestate.HasTarget() && !gamestate.GetCastBar().IsCasting() && !gamestate.GetGcdBar().GetIsInGcd() && !onCooldown && target.IsAlive())
            if (gamestate.DecreaseMana(manaCost))
            {
                //play the impactSound
                source.PlayOneShot(castSound, GameControl.control.soundMultiplyer);

                //the cast procedure
                RemoveDebuff();

                // start the cooldown
                cooldownTimer = 0f;
                cooldownMax = cooldown;
                onCooldown = true;
                cooldownOverlay.color = new Color32(160, 160, 160, 160);
                gamestate.GetGcdBar().StartGcd();
            }
    }


    /// <summary>
    /// Removes all dispellable buffs.
    /// </summary>
    private void RemoveDebuff()
    {
        BuffManager manager = target.GetGameObject().GetComponent<BuffManager>();
        List <IBuff> buffDict = manager.GetAllBuffsSortetByDuration(); //get all buffs

        foreach(IBuff buff in buffDict)
        {
            if(buff.IsDispellable())
            {
                manager.DeregisterBuff(buff); //remove buff if it is dispellable
                buff.Destroy();
            }
            else if(buff.IsBuff() && GameControl.control.talente[6])
            {
                buff.Reset(); //reset buff if the "dispell reset" talent is picked
            }
        }
    }

    /// <summary>
    /// Starts a GCD.
    /// </summary>
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

    /// <summary>
    /// Removes the spell from the button.
    /// </summary>
    public void RemoveSpellFromButton()
    {
        GetComponent<MeshRenderer>().material = null;
        Destroy(this);
    }
}
