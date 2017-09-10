using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class Shield : MonoBehaviour, ISpell
{
    public Gamestate gamestate;
    public Image cooldownOverlay;
    private IRaider target;
    private float manaKosten = 25f;
    private float castTime = 0f;
    private float cooldown = 3.5f;
    private float cooldownTimer;
    private float cooldownMax;
    private bool onCooldown = false;
    private string spellName = "Machtwort: Schild";

    private AudioSource source;
    private AudioClip impactSound;

    void Start()
    {
        impactSound = Resources.Load("ShieldImpact", typeof(AudioClip)) as AudioClip;
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
                cooldownTimer = 0f;
                cooldownMax = cooldown;
                onCooldown = true;
                cooldownOverlay.color = new Color32(160, 160, 160, 160);
                source.PlayOneShot(impactSound, GameControl.control.soundMultiplyer);
                GenerateBuff();
                gamestate.GetGcdBar().StartGcd();
            }
    }


    private void GenerateBuff()
    {
        if (!target.GetGameObject().GetComponent<ShieldBuff>())
        {
            ShieldBuff buff = target.GetGameObject().AddComponent<ShieldBuff>();
            target.GetGameObject().GetComponent<BuffManager>().RegisterBuff(buff);
        }
        else
        {
            target.GetGameObject().GetComponent<ShieldBuff>().Reset();
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
