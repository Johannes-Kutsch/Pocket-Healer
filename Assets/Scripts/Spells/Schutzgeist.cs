using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class Schutzgeist : MonoBehaviour, ISpell
{
    public Gamestate gamestate;
    public Image cooldownOverlay;
    private IRaider target;
    private List<IRaider> raiderDict;
    private float manaKosten = 20f;
    private float castTime = 0f;
    private float cooldown = 15f;
    private float cooldownTimer;
    private float cooldownMax;
    private bool onCooldown = false;
    private string spellName = "Schutzgeist";

    private AudioSource source;
    private AudioClip castSound = Resources.Load("SchutzgeistCast", typeof(AudioClip)) as AudioClip;

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
                cooldownTimer = 0f;
                cooldownMax = cooldown;
                onCooldown = true;
                cooldownOverlay.color = new Color32(160, 160, 160, 160);
                source.PlayOneShot(castSound, GameControl.control.soundMultiplyer);
                gamestate.GetGcdBar().StartGcd();
                GenerateBuff();
            }
    }


    private void GenerateBuff()
    {
        if (!target.GetGameObject().GetComponent<SchutzgeistBuff>())
        {
            SchutzgeistBuff buff = target.GetGameObject().AddComponent<SchutzgeistBuff>();
            target.GetGameObject().GetComponent<BuffManager>().RegisterBuff(buff);
        }
        else
        {
            target.GetGameObject().GetComponent<SchutzgeistBuff>().Reset();
        }
        if(GameControl.control.talente[8])
        {
            raiderDict = RaiderDB.GetInstance().GetAllRaiderSortetByHealth();
            raiderDict.Remove(target);
            foreach(IRaider raider in raiderDict)
            {
                SchutzgeistBuffInvis buff = raider.GetGameObject().AddComponent<SchutzgeistBuffInvis>();
                raider.GetGameObject().GetComponent<BuffManager>().RegisterBuff(buff);
            }
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
