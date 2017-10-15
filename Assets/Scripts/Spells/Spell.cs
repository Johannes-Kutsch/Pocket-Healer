using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using System;

/// <summary>
/// Abstract class spell which implements the basic functionality of most spells.
/// It triggers the virtual methods OnCastStart() and OnCastSucess(). (When a cast is started and when a cast is sucessfully finished)
/// Overwrite the following variables during awake: 
///     castSound; //The Audioclip used for the cast sound. Can be null if the spell has no casttime
///     impactSound; //The Audioclip used for the impact sound.
///     cooldown; //The cooldown of the spell in seconds.
///     manaCost; //The manacost of the spell.
///     castTime; //The casttime of the spell, 0 when instant.
///     spellName; //The name of the spell.
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
    private IRaider target;

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
    /// Checks if we can cast the spell and starts the cast prozedure.
    /// </summary>
    void OnMouseDown()
    {
        if (!gamestate.GetCastBar().IsCasting() && !gamestate.GetGcdBar().GetIsInGcd() && !onCooldown)
        {
            target = gamestate.GetTarget();
            if (target != null && target.IsAlive())
            {
                if (gamestate.DecreaseMana(manaCost))
                {
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

        //check if the spell has a cooldown
        if (cooldown > 0)
        {
            cooldownTimer = 0f;
            cooldownMax = cooldown;
            onCooldown = true;
            cooldownOverlay.color = new Color32(160, 160, 160, 160);
        }

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

        //check if the spell has a cooldown
        if (cooldown > 0)
        {
            cooldownTimer = 0f;
            cooldownMax = cooldown;
            onCooldown = true;
            cooldownOverlay.color = new Color32(160, 160, 160, 160);
        }

        //start the gcd
        gamestate.GetGcdBar().StartGcd();

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

    /// <summary>
    /// Gets the target .
    /// </summary>
    /// <returns></returns>
    public IRaider GetTarget()
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
