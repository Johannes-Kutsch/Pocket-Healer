using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

/// <summary>
/// Applys a Buff that heals the raider to full if he would die.
/// </summary>
public class GuardianSpirit : MonoBehaviour, ISpell
{
    public Gamestate gamestate;
    public Image cooldownOverlay;
    private IRaider target;
    private List<IRaider> raiderDict;
    private float manaCost = 20f;
    private float castTime = 0f;
    private float cooldown = 15f;
    private float cooldownTimer;
    private float cooldownMax;
    private bool onCooldown = false;
    private string spellName = "Guardian Spirit";

    private AudioSource source;
    private AudioClip impactSound;

    /// <summary>
    /// Called on start. Set some sounds and find the gamestate, the cooldownoverlay and the audiosource.
    /// </summary>
    void Start()
    {
        impactSound = Resources.Load("SchutzgeistCast", typeof(AudioClip)) as AudioClip;
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
    /// Check if we can cast the spell and cast it.
    /// </summary>
    public void Cast()
    {
        target = gamestate.GetTarget();

        if (gamestate.HasTarget() && !gamestate.GetCastBar().IsCasting() && !gamestate.GetGcdBar().GetIsInGcd() && !onCooldown && target.IsAlive())
        {
            if (gamestate.DecreaseMana(manaCost))
            {
                source.PlayOneShot(impactSound, GameControl.control.soundMultiplyer); //play the impact sound

                //apply the buff
                GenerateBuff();

                // start the cooldown
                cooldownTimer = 0f;
                cooldownMax = cooldown;
                onCooldown = true;
                cooldownOverlay.color = new Color32(160, 160, 160, 160);
                gamestate.GetGcdBar().StartGcd();
            }
        }
    }

    /// <summary>
    /// Applys the guardian spirit buff to the current target.
    /// Applys the guardian spirit invis buff to every other raider if the talent is selected.
    /// </summary>
    private void GenerateBuff()
    {
        if (!target.GetGameObject().GetComponent<GuardianSpiritBuff>())
        {
            GuardianSpiritBuff buff = target.GetGameObject().AddComponent<GuardianSpiritBuff>();
            target.GetGameObject().GetComponent<BuffManager>().RegisterBuff(buff);
        }
        else
        {
            target.GetGameObject().GetComponent<GuardianSpiritBuff>().Reset();
        }

        if(GameControl.control.talente[8])
        {
            raiderDict = RaiderDB.GetInstance().GetAllRaidersSortedByHealth();
            raiderDict.Remove(target);
            foreach(IRaider raider in raiderDict)
            {
                GuardianSpiritBuffInvis buff = raider.GetGameObject().AddComponent<GuardianSpiritBuffInvis>();
                raider.GetGameObject().GetComponent<BuffManager>().RegisterBuff(buff);
            }
        }
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
