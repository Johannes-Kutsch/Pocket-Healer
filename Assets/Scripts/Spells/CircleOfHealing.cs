﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;

/// <summary>
/// A Heal that heals the 5 lowest raiders.
/// </summary>
public class CircleOfHealing : MonoBehaviour, ISpell {
    public Gamestate gamestate;
    private IRaider target;
    public Image cooldownOverlay;
    private Coroutine timer;
    private List<IRaider> raiderDict = new List<IRaider>();
    public float cooldownTimer;
    public float cooldownMax;
    public bool onCooldown = false;
    private string spellName = "Circle of Healing";

    public float cooldown = 8f;
    private float healAmount = 50f;
    private float manaCost = 80f;
    private float numberTargets = 5f;
    private float castTime = 0f;

    private AudioSource source;
    private AudioClip castSound;
    private AudioClip impactSound;

    /// <summary>
    /// Called on start. Set some sounds and find the gamestate, the cooldownoverlay and the audiosource.
    /// Checks if talents are picked that modify the skill.
    /// </summary>
    void Start()
    {
        castSound = Resources.Load("GreaterHealCast", typeof(AudioClip)) as AudioClip;
        impactSound = Resources.Load("CircleCast", typeof(AudioClip)) as AudioClip;

        if (GameControl.control.talente[2]) //7 targets talent
        {
            numberTargets += 2;
        }

        if (GameControl.control.talente[7]) //no cooldown but casttime talent
        {
            cooldown = 0f;
            castTime = 2f;
        }

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
        if (!GameControl.control.talente[7])
            CastInstant();
        else
            Cast();
    }

    /// <summary>
    /// Check if we can cast the spell and cast it. This is used when the spell has no casttime.
    /// </summary>
    public void CastInstant()
    {
        if (!gamestate.GetCastBar().IsCasting() && !gamestate.GetGcdBar().GetIsInGcd() && !onCooldown)
            if (gamestate.DecreaseMana(manaCost))
            {
                //play the impactSound
                source.PlayOneShot(impactSound, GameControl.control.soundMultiplyer);

                raiderDict = RaiderDB.GetInstance().GetAllRaidersSortedByHealth();

                if(raiderDict.Count() < numberTargets) //if less raiders are alive than we can target, decrease number of targets
                {
                    numberTargets = raiderDict.Count();
                }

                for (int i = 0; i < numberTargets; i++) //heal numberTargets raider
                {
                    target = raiderDict.First();
                    if (target.IsAlive())
                    {
                        target.Heal(healAmount);
                    }
                    raiderDict.Remove(target);
                }

                // start the cooldown
                cooldownTimer = 0f;
                cooldownMax = cooldown;
                onCooldown = true;
                cooldownOverlay.color = new Color32(160, 160, 160, 160);
                gamestate.GetGcdBar().StartGcd();
            }
    }

    /// <summary>
    /// Check if we can cast the spell and start the cast procedure. This is used when the spell has a casttime.
    /// </summary>
    public void Cast()
    {
        if (!gamestate.GetCastBar().IsCasting() && !gamestate.GetGcdBar().GetIsInGcd() && !onCooldown)
        {
            if (gamestate.DecreaseMana(manaCost))
            {
                timer = StartCoroutine(Timer());
            }
        }
    }

    /// <summary>
    /// The cast procedure.
    /// </summary>
    /// <returns></returns>
    IEnumerator Timer()
    {
        //start the cast
        gamestate.GetCastBar().Cast(castTime, spellName);
        gamestate.GetGcdBar().StartGcd();
        source.PlayOneShot(castSound, GameControl.control.soundMultiplyer);

        //wait till the cast is finished
        yield return new WaitForSeconds(castTime);

        //stop the cast sound and play the impactSound
        source.Stop();
        source.PlayOneShot(impactSound, GameControl.control.soundMultiplyer);

        raiderDict = RaiderDB.GetInstance().GetAllRaidersSortedByHealth();

        if (raiderDict.Count() < numberTargets) //if less raiders are alive than we can target, decrease number of targets
        {
            numberTargets = raiderDict.Count();
        }

        for (int i = 0; i < numberTargets; i++) //heal numberTargets raider
        {
            target = raiderDict.First();
            if (target.IsAlive())
            {
                target.Heal(healAmount);
            }
            raiderDict.Remove(target);
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
