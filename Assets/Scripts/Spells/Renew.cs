using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class Renew : MonoBehaviour, ISpell {
    public Gamestate gamestate;
    public Image cooldownOverlay;
    private IRaider target;
    private float manaKosten = 10f;
    private float castTime = 0f;
    private float cooldown = 3.5f;
    private float cooldownTimer;
    private float cooldownMax;
    private bool onCooldown = false;
    private string spellName = "Renew";

    private AudioSource source;
    private AudioClip castSound = Resources.Load("RenewCast", typeof(AudioClip)) as AudioClip;

    void Start()
    {
        if (GameControl.control.talente[0])
            cooldown = 0f;
        gamestate = Gamestate.gamestate;
        cooldownOverlay = GetComponentInChildren<Image>();
        gamestate.AddSpell(this);
        cooldownTimer = cooldown;
        source = GetComponent<AudioSource>();
    }

    void FixedUpdate()
    {
        if(cooldownTimer >= cooldownMax)
        {
            onCooldown = false;
            cooldownOverlay.color = new Color32(160, 160, 160, 0);
        } else
        {
            cooldownTimer += 0.02f;
            cooldownOverlay.fillAmount = cooldownTimer/cooldownMax;
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
                gamestate.GetGcdBar().StartGcd();
                cooldownOverlay.color = new Color32(160, 160, 160, 160);
                source.PlayOneShot(castSound, GameControl.control.soundMultiplyer);
                GenerateHot();
            }
    }


    private void GenerateHot()
    {
        if (!target.GetGameObject().GetComponent<RenewHot>())
        {
            RenewHot hot = target.GetGameObject().AddComponent<RenewHot>();
            target.GetGameObject().GetComponent<BuffManager>().RegisterBuff(hot);
        } else
        {
            target.GetGameObject().GetComponent<RenewHot>().Reset();
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
