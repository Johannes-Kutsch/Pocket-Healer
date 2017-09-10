using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;

public class CircleOfHealing : MonoBehaviour, ISpell {
    public Gamestate gamestate;
    private IRaider target;
    public Image cooldownOverlay;
    private Coroutine timer;
    private List<IRaider> raiderDict = new List<IRaider>();
    public float cooldown = 8f;
    public float cooldownTimer;
    public float cooldownMax;
    public bool onCooldown = false;
    private float healAmount = 50f;
    private float manaKosten = 80f;
    private float numberTargets = 5f;
    private float castTime = 0f;
    private string spellName = "Kreis der Heilung";

    private AudioSource source;
    private AudioClip castSound;
    private AudioClip impactSound;

    void Start()
    {
        castSound = Resources.Load("GreaterHealCast", typeof(AudioClip)) as AudioClip;
        impactSound = Resources.Load("CircleCast", typeof(AudioClip)) as AudioClip;
        if (GameControl.control.talente[2])
        {
            numberTargets += 2;
        }
        if (GameControl.control.talente[7])
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
        if (!GameControl.control.talente[7])
            CastInstant();
        else
            Cast();
    }

    public void CastInstant()
    {
        if (!gamestate.GetCastBar().IsCasting() && !gamestate.GetGcdBar().GetIsInGcd() && !onCooldown)
            if (gamestate.DecreaseMana(manaKosten))
            {
                source.PlayOneShot(impactSound, GameControl.control.soundMultiplyer);
                raiderDict = RaiderDB.GetInstance().GetAllRaiderSortetByHealth();
                if(raiderDict.Count() < numberTargets)
                {
                    numberTargets = raiderDict.Count();
                }
                for (int i = 0; i < numberTargets; i++)
                {
                    target = raiderDict.First();
                    if (target.IsAlive())
                    {
                        target.Heal(healAmount);
                    }
                    raiderDict.Remove(target);
                }
                cooldownTimer = 0f;
                cooldownMax = cooldown;
                onCooldown = true;
                cooldownOverlay.color = new Color32(160, 160, 160, 160);
                gamestate.GetGcdBar().StartGcd();
            }
    }

    public void Cast()
    {
        if (!gamestate.GetCastBar().IsCasting() && !gamestate.GetGcdBar().GetIsInGcd() && !onCooldown)
        {
            if (gamestate.DecreaseMana(manaKosten))
            {
                timer = StartCoroutine(Timer());
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
        raiderDict = RaiderDB.GetInstance().GetAllRaiderSortetByHealth();
        if (raiderDict.Count() < numberTargets)
        {
            numberTargets = raiderDict.Count();
        }
        for (int i = 0; i < numberTargets; i++)
        {
            target = raiderDict.First();
            if (target.IsAlive())
            {
                target.Heal(healAmount);
            }
            raiderDict.Remove(target);
        }
        cooldownTimer = 0f;
        cooldownMax = cooldown;
        onCooldown = true;
        cooldownOverlay.color = new Color32(160, 160, 160, 160);
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
