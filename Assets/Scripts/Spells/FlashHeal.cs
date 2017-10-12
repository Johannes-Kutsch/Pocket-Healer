using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

/// <summary>
/// A Heal that heals the current target.
/// </summary>
public class FlashHeal : MonoBehaviour, ISpell
{
    public Gamestate gamestate;
    private IRaider target;
    public Image cooldownOverlay;
    private float cooldownTimer;
    private float cooldownMax;
    private Coroutine timer;
    private bool onCooldown = false;
    private string spellName = "Flash Heal";

    private float cooldown = 0f;
    private float healAmount = 50f;
    private float manaCost = 20f;
    private float castTime = 1f;

    private AudioSource source;
    private AudioClip castSound;
    private AudioClip impactSound;

    /// <summary>
    /// Called on start. Set some sounds and find the gamestate, the cooldownoverlay and the audiosource.
    /// </summary>
    void Start()
    {
        castSound = Resources.Load("GreaterHealCast", typeof(AudioClip)) as AudioClip;
        impactSound = Resources.Load("FlashHealImpact", typeof(AudioClip)) as AudioClip;
        gamestate = FindObjectOfType<Gamestate>();
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
    /// Check if we can cast the spell and start the cast procedure.
    /// </summary>
    public void Cast()
    {
        if (!gamestate.GetCastBar().IsCasting() && !gamestate.GetGcdBar().GetIsInGcd() && !onCooldown && gamestate.HasTarget())
        {
            target = gamestate.GetTarget(); //save the current target
            if (target.IsAlive())
            {
                if (gamestate.DecreaseMana(manaCost))
                {
                    timer = StartCoroutine(Timer());
                }
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

        //heal the target
        target.Heal(healAmount);
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
