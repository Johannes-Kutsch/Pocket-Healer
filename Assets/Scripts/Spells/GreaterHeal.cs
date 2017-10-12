using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

/// <summary>
/// Heals the current target.
/// </summary>
public class GreaterHeal : MonoBehaviour, ISpell {
    public Gamestate gamestate;
    private IRaider target;
    public Image cooldownOverlay;
    private float cooldown = 0f;
    private float cooldownTimer;
    private float cooldownMax;
    private Coroutine timer;
    private float healAmount = 50f;
    private float manaCost = 10f;
    private float castTime = 2f;
    private bool onCooldown = false;
    private string spellName = "Greater Heal";

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
        impactSound = Resources.Load("GreaterHealImpact", typeof(AudioClip)) as AudioClip;

        if (GameControl.control.talente[1]) //shorter cast time talent
        {
            castTime *= 0.75f;
        }

        if (GameControl.control.talente[4]) //lower heal/refund mana talent
        {
            healAmount *= 0.75f;
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
        Cast();
    }

    /// <summary>
    /// Check if we can cast the spell and start the cast procedure.
    /// </summary>
    public void Cast()
    {
        if (!gamestate.GetCastBar().IsCasting() && !gamestate.GetGcdBar().GetIsInGcd() && !onCooldown && gamestate.HasTarget())
        {
            target = gamestate.GetTarget();
            if (target.IsAlive())
            {
                if (GameControl.control.talente[4])
                {
                    gamestate.IncreaseMana(manaCost/2); //lower heal/refund mana talent
                    timer = StartCoroutine(Timer());
                }
                else 
                {
                    if (gamestate.DecreaseMana(manaCost))
                    {
                        timer = StartCoroutine(Timer());
                    }
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
