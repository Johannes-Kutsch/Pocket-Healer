using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;

public class Hymne : MonoBehaviour, ISpell {
    public Gamestate gamestate;
    public Image cooldownOverlay;
    private float cooldown = 25f;
    private float cooldownTimer;
    private float cooldownMax;
    private Coroutine timer;
    private float healAmount = 20f;
    private float manaKosten = 50f;
    private float castTime = 3f;
    private float ticks = 5f;
    private bool onCooldown = false;
    private string spellName = "Hymne der Hoffnung";

    private AudioSource source;
    private AudioClip castSound;

    void Start()
    {
        castSound = Resources.Load("HymneCast", typeof(AudioClip)) as AudioClip;
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
        if (!gamestate.GetCastBar().IsCasting() && !gamestate.GetGcdBar().GetGcd() && !onCooldown)
        {
            if (gamestate.DecreaseMana(manaKosten))
            {
                timer = StartCoroutine(Timer());
            }
        }
    }

    IEnumerator Timer()
    {
        gamestate.GetCastBar().SetzeCasting(true);
        gamestate.GetCastBar().Caste(castTime, spellName);
        gamestate.GetGcdBar().StartGcd();
        cooldownTimer = 0f;
        cooldownMax = cooldown;
        onCooldown = true;
        source.PlayOneShot(castSound, GameControl.control.soundMultiplyer);
        for (float i = 0; i < castTime; )
        {
            
            foreach (IRaider raider in RaiderDB.GetInstance().GetAllRaider())
            {
                raider.IncreaseHP(healAmount);
                
            }
            yield return new WaitForSeconds(castTime / (ticks -1));
            i += (castTime / (ticks - 1));
        }
        foreach (IRaider raider in RaiderDB.GetInstance().GetAllRaider())
        {
            raider.IncreaseHP(healAmount);
            if (GameControl.control.talente[5])
            {
                GenerateHot(raider);
            }
        }
        gamestate.GetCastBar().SetzeCasting(false);
        source.Stop();
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

    private void GenerateHot(IRaider target)
    {
        if (!target.GetGameObject().GetComponent<HymneBuff>())
        {
            HymneBuff hot = target.GetGameObject().AddComponent<HymneBuff>();
            target.GetGameObject().GetComponent<BuffManager>().RegisterBuff(hot);
        }
        else
        {
            target.GetGameObject().GetComponent<HymneBuff>().Reset();
        }
    }

    public void RemoveSpellFromButton()
    {
        GetComponent<MeshRenderer>().material = null;
        Destroy(this);
    }
}
