using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class FlashHeal : MonoBehaviour, ISpell
{
    public Gamestate gamestate;
    private IRaider target;
    public Image cooldownOverlay;
    private float cooldown = 0f;
    private float cooldownTimer;
    private float cooldownMax;
    private Coroutine timer;
    private float healAmount = 50f;
    private float manaKosten = 20f;
    private float castTime = 1f;
    private bool onCooldown = false;
    private string spellName = "Blitzheilung";

    private AudioSource source;
    private AudioClip castSound;
    private AudioClip impactSound;

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
        if (!gamestate.GetCastBar().IsCasting() && !gamestate.GetGcdBar().GetIsInGcd() && !onCooldown && gamestate.HasTarget())
        {
            target = gamestate.GetTarget();
            if (target.IsAlive())
            {
                if (gamestate.DecreaseMana(manaKosten))
                {
                    timer = StartCoroutine(Timer());
                }
            }
        }
    }

    IEnumerator Timer()
    {
        gamestate.GetCastBar().Cast(castTime, spellName);
        gamestate.GetGcdBar().StartGcd();
        source.PlayOneShot(castSound, GameControl.control.soundMultiplyer);
        yield return new WaitForSeconds(castTime);
        source.Stop();
        source.PlayOneShot(impactSound, GameControl.control.soundMultiplyer);
        target.Heal(healAmount);
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
