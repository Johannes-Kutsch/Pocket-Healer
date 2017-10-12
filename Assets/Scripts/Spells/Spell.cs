using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using System;

public abstract class Spell : MonoBehaviour, ISpell
{
    private Gamestate gamestate;
    private Image cooldownOverlay;
    private Coroutine timer;
    private float cooldownTimer;
    private float cooldownMax;
    private bool onCooldown = false;
    private AudioSource source;

    private AudioClip castSound;
    private AudioClip impactSound;
    private float cooldown;
    private float manaCost;
    private float castTime;
    private string spellName;

    /// <summary>
    /// Called on start. Check if some variables are assigned and find the gamestate, the cooldownoverlay and the audiosource.
    /// </summary>
    public void Start()
    {
        if(castSound == null) //check if all variables are assigned
        {
            throw new Exception("castSound is null, please assign a castSound in the Awake() method");
        }
        if (impactSound == null)
        {
            throw new Exception("impactSound is null, please assign a impactSound in the Awake() method");
        }
        if (spellName == null)
        {
            throw new Exception("spellName is null, please assign a spellName in the Awake() method");
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
        if (castTime == 0)
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
        source.PlayOneShot(castSound, GameControl.control.soundMultiplyer);

        //call OnCastStart()
        OnCastStart();

        //wait till the cast is finished
        yield return new WaitForSeconds(castTime);

        //stop the cast sound and play the impactSound
        source.Stop();
        source.PlayOneShot(impactSound, GameControl.control.soundMultiplyer);

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

    public abstract void OnCastStart();

    public abstract void OnCastSucess();
}
