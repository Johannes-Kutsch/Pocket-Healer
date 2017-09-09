using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class Prayer : MonoBehaviour, ISpell {
    public Gamestate gamestate;
    public Image cooldownOverlay;
    private IRaider target;
    private float manaKosten = 25f;
    private float castTime = 0f;
    private float cooldown = 10f;
    private float cooldownTimer;
    private float cooldownMax;
    private bool onCooldown = false;
    private string spellName = "Prayer of Mending";

    private AudioSource source;
    private AudioClip castSound = Resources.Load("PrayerCast", typeof(AudioClip)) as AudioClip;

    void Start()
    {
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
                gamestate.GetGcdBar().StartGcd();
                cooldownTimer = 0f;
                cooldownMax = cooldown;
                onCooldown = true;
                cooldownOverlay.color = new Color32(160, 160, 160, 160);
                source.PlayOneShot(castSound, GameControl.control.soundMultiplyer);
                GenerateBuff();
            }
    }


    private void GenerateBuff()
    {
        if (!target.GetGameObject().GetComponent<PrayerBuff>())
        {
            PrayerBuff buff = target.GetGameObject().AddComponent<PrayerBuff>();
            target.GetGameObject().GetComponent<BuffManager>().RegisterBuff(buff);
        }
        else
        {
            target.GetGameObject().GetComponent<PrayerBuff>().Reset();
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
