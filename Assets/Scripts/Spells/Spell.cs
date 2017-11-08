using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using System;

/// <summary>
/// Abstract class spell which implements the basic functionality of most spells.
/// It triggers the virtual methods OnCastStart() and OnCastSucess(). (When a cast is started and when a cast is sucessfully finished)
/// </summary>
public abstract class Spell : MonoBehaviour
{
    private Gamestate gamestate;
    private Image cooldownOverlay;
    private Coroutine timer;
    private float cooldownTimer;
    private float cooldownMax;
    private bool onCooldown = false;
    private AudioSource source;
    private Raider target;

    private AudioClip castSound;
    private AudioClip impactSound;
    private float cooldown;
    private float manaCost;
    private float castTime;
    private string spellName;

    /// <summary>
    /// Called on start. Assign some variables and find the gamestate, the cooldownoverlay and the audiosource.
    /// </summary>
    public void Start()
    {
        spellName = GetSpellname();
        cooldown = GetCooldown();
        manaCost = GetManacost();
        castTime = GetCastTime();
        castSound = Resources.Load(GetCastSoundName(), typeof(AudioClip)) as AudioClip;
        impactSound = Resources.Load(GetImpactSoundName(), typeof(AudioClip)) as AudioClip;

        gamestate = Gamestate.gamestate;
        cooldownOverlay = GetComponentInChildren<Image>();
        cooldownOverlay.color = new Color32(160, 160, 160, 160);
        cooldownOverlay.enabled = false;
        gamestate.AddSpell(this);
        cooldownTimer = cooldown;
        source = GetComponent<AudioSource>();
    }

    /// <summary>
    /// Called with every fixed update. Advances the cooldown and draws the cooldownoverlay at the new Position.
    /// </summary>
    void FixedUpdate()
    {
        if (onCooldown && cooldownTimer >= cooldownMax)
        {
            onCooldown = false;
            cooldownOverlay.enabled = false;
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
        StartCast();
    }

    /// <summary>
    /// Checks if we can cast the spell and starts the cast procedure.
    /// </summary>
    public void StartCast()
    {
        if (!gamestate.GetCastBar().IsCasting() && !gamestate.GetGcdBar().GetIsInGcd() && !onCooldown)
        { //we are not casting, are not in an gcd (Global Cooldown) and the spell is not on cooldown
            target = gamestate.GetTarget();
            if (target != null && target.IsAlive())
            {
                if (manaCost >= 0) //spell costs Mana
                {
                    if (gamestate.DecreaseMana(manaCost))
                    {                                       //we have enough Mana to cast the spell
                        if (castTime == 0)
                            CastInstant(); //spell with no casttime
                        else
                            StartCoroutine(Cast()); //spell with casttime
                    }
                }
                else //spel regenerates Mana
                {
                    gamestate.IncreaseMana(manaCost * -1);

                    if (castTime == 0)
                        CastInstant(); //spell with no casttime
                    else
                        StartCoroutine(Cast()); //spell with casttime
                }
            }
        }
    }

    /// <summary>
    /// The cast procedure for a spell without cast time.
    /// </summary>
    public void CastInstant()
    {
        //play the impactSound
        if (impactSound != null)
        {
            source.PlayOneShot(impactSound, GameControl.control.soundMultiplier);
        }

        //call OnCastStart() and OnCastSucess()
        OnCastStart();
        OnCastSucess();

        StartCooldown(); //stat the cooldown

        //start the gcd
        gamestate.GetGcdBar().StartGcd();

    }

    /// <summary>
    /// The cast procedure for a spell with cast time.
    /// </summary>
    /// <returns></returns>
    IEnumerator Cast()
    {
        //start the cast
        gamestate.GetCastBar().Cast(castTime, spellName);

        //start the gcd
        gamestate.GetGcdBar().StartGcd();

        //start the cooldown
        StartCooldown(); 

        //play the cast sound
        if (castSound != null)
        {
            source.PlayOneShot(castSound, GameControl.control.soundMultiplier);
        }

        //call OnCastStart()
        OnCastStart();

        //wait till the cast is finished
        yield return new WaitForSeconds(castTime);

        //stop the cast sound and play the impactSound
        if (source.isPlaying)
        {
            source.Stop();
        }

        if (impactSound != null)
        {
            source.PlayOneShot(impactSound, GameControl.control.soundMultiplier);
        }

        //call OnCastSucess()
        OnCastSucess();
    }

    /// <summary>
    /// Checks if the spell is not onCooldown or if the cooldown is lower than the GCD and starts a GCD if it is.
    /// </summary>
    public void StartGcd()
    {
        if (!onCooldown || cooldownMax - cooldownTimer < gamestate.GetGcdBar().GetGcdTime())
        {
            cooldownMax = gamestate.GetGcdBar().GetGcdTime();
            onCooldown = true;
            cooldownTimer = 0f;
            cooldownOverlay.enabled = true;
        }
    }

    /// <summary>
    /// Checks if the spell has a cooldown and starts the cooldown if the spell has one.
    /// </summary>
    private void StartCooldown()
    {
        if (cooldown > 0) //check if the spell has a cooldown
        { //start the cooldown
            cooldownTimer = 0f;
            cooldownMax = cooldown;
            onCooldown = true;
            cooldownOverlay.enabled = true;
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

    /// <summary>
    /// Gets the target .
    /// </summary>
    /// <returns></returns>
    public Raider GetTarget()
    {
        return target;
    }

    /// <summary>
    /// Called when a cast is started.
    /// </summary>
    public virtual void OnCastStart()
    {

    }

    /// <summary>
    /// Called when a cast is sucesfully finished.
    /// </summary>
    public virtual void OnCastSucess()
    {

    }

    /// <summary>
    /// Gets the spellname.
    /// </summary>
    /// <returns>the spellname</returns>
    public abstract string GetSpellname();

    /// <summary>
    /// Gets the cooldown.
    /// </summary>
    /// <returns>the cooldown</returns>
    public abstract float GetCooldown();

    /// <summary>
    /// Gets the manacost.
    /// </summary>
    /// <returns>the manacost</returns>
    public abstract float GetManacost();

    /// <summary>
    /// Gets the cast time.
    /// </summary>
    /// <returns>the cast time</returns>
    public abstract float GetCastTime();

    /// <summary>
    /// Gets the name of the cast sound.
    /// </summary>
    /// <returns>the name of the cast sound</returns>
    public abstract string GetCastSoundName();

    /// <summary>
    /// Gets the name of the impact sound.
    /// </summary>
    /// <returns>the name of the impact sound</returns>
    public abstract string GetImpactSoundName();    
}
