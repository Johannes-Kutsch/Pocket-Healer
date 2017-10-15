using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Linq;

/// <summary>
/// Script used for the multi phase fight in Scene 10.
/// There are 3 Phases which constantly rotate.
/// P1: Fire Phase - applys simple nondipeallable dots
/// P2: Wind Phase - Applys a debuff that reduces healing taking. The debuff gets stronger over time.
/// P3: Water Phase - Removes all debuffs and damages every raider in a fixed intervall.
/// </summary>
public class Alakir : MonoBehaviour {
    private IRaider rangeTarget = null;

    public int levelIndex;
    public Image BossModImagePhase;
    private Image cooldownOverlayPhase;
    public Image BossModImageDebuff;
    private Image cooldownOverlayDebuff;
    public Text BossModTextDebuff;

    private int phaseID = 1;
    private float timeInPhase;
    private float fireDotTimerCurrent;
    private float frostTimerCurrent;
    private float rangeAttackTimerCurrent;

    public string emotePhaseOne = "transforms into his air form. The healing you do decreases slowly.";
    public string emotePhaseTwo = "transforms into his water form. A blizzard appears.";
    public string emotePhaseThree = "transforms into his fire form.";

    private float phaseduration;
    private float rangeAttackTimer;
    private float rangeAttackDamage;
    private float frostTimer;
    private float frostDamage;
    private float fireDotTimer;
    private float fireDotTimerStart;

    /// <summary>
    /// Called on start.
    /// </summary>
    void Start()
    {
        Settings settings = new Settings(levelIndex);

        rangeAttackTimer = settings.rangeAutoAttackSwingTimer;
        rangeAttackDamage = settings.rangeAutoAttackDmg;
        frostTimer = settings.alakirFrostTimer;
        frostDamage = settings.alakirFrostDmg;
        fireDotTimer = settings.alakirFireDotTimer;
        fireDotTimerStart = settings.alakirFirDotTimerStart;
        phaseduration = settings.alakirPhaseduration;

        fireDotTimerCurrent = fireDotTimerStart;

        Image[] cooldownOverlays = BossModImagePhase.GetComponentsInChildren<Image>();

        foreach (Image image in cooldownOverlays)
        {
            if (image.transform != BossModImagePhase.transform)
            {
                cooldownOverlayPhase = image;
            }
        }

        BossModImagePhase.enabled = true;
        cooldownOverlayPhase.enabled = true;

        cooldownOverlays = BossModImageDebuff.GetComponentsInChildren<Image>();

        foreach (Image image in cooldownOverlays)
        {
            if (image.transform != BossModImageDebuff.transform)
            {
                cooldownOverlayDebuff = image;
            }
        }

        BossModImageDebuff.enabled = true;
        cooldownOverlayDebuff.enabled = true;

        if (GameControl.control.difficulty == 0)
        {
            rangeAttackDamage *= GameControl.control.easyMultiplier;
            frostDamage *= GameControl.control.easyMultiplier;
        }
    }

    /// <summary>
    /// Called with every fixed update.
    /// </summary>
    void FixedUpdate()
    {
        timeInPhase += 0.02f;

        cooldownOverlayPhase.fillAmount = timeInPhase / phaseduration;

        switch (phaseID)
        {
            case 1:
                FirePhase();
                break;

            case 2:
                AirPhase();
                break;

            case 3:
                WaterPhase();
                break;
        }
    }

    /// <summary>
    /// Fire Phase logic.
    /// </summary>
    private void FirePhase()
    {
        fireDotTimerCurrent += 0.02f;
        rangeAttackTimerCurrent += 0.02f;

        cooldownOverlayDebuff.fillAmount = fireDotTimerCurrent / fireDotTimer;

        if (fireDotTimerCurrent >= fireDotTimer) //apply a new Fire Dot
        {
            fireDotTimerCurrent = 0f;

            List<IRaider> targetDict = new List<IRaider>();

            foreach (IRaider raider in new List<IRaider>(RaiderDB.GetInstance().GetAllDDs()))
            {
                if (!raider.GetGameObject().GetComponent<AlakirFeuerDebuff>())
                {
                    targetDict.Add(raider);
                }
            }

            if (targetDict.Count == 0)
            {
                foreach (IRaider raider in new List<IRaider>(RaiderDB.GetInstance().GetAllRaiders()))
                {
                    if (!raider.GetGameObject().GetComponent<AlakirFeuerDebuff>())
                    {
                        targetDict.Add(raider);
                    }
                }
            }

            if (targetDict.Count == 0)
            {
                targetDict = RaiderDB.GetInstance().GetAllRaiders();
            }

            IRaider fireTarget = targetDict[Random.Range(0, targetDict.Count)];
            fireTarget.GetGameObject().AddComponent<AlakirFeuerDebuff>();
            fireTarget = null;

        }

        else if (rangeAttackTimerCurrent >= rangeAttackTimer) //Range AutoAttack
        {
            rangeAttackTimerCurrent = 0f;

            List<IRaider> targetDict = new List<IRaider>(RaiderDB.GetInstance().GetAllDDs());

            if (rangeTarget != null && targetDict.Count > 1 && targetDict.Contains(rangeTarget))
            {
                targetDict.Remove(rangeTarget);
            }

            rangeTarget = targetDict[Random.Range(0, targetDict.Count)];
            rangeTarget.Damage(rangeAttackDamage);
        }

        else if (timeInPhase > phaseduration - 2.05f && timeInPhase < phaseduration - 1.95f) //emote for air phase
            GetComponent<Boss>().SetEmoteText(" " + emotePhaseOne);

        else if (timeInPhase > phaseduration) //switch to air phase
        {
            List<IRaider> targetDict = new List<IRaider>(RaiderDB.GetInstance().GetAllRaiders());

            foreach (IRaider target in targetDict)
            {
                target.GetGameObject().AddComponent<AlakirWasserDebuff>();
            }

            BossModImagePhase.sprite = Resources.Load("Blizzard", typeof(Sprite)) as Sprite;
            BossModImageDebuff.sprite = Resources.Load("Luft", typeof(Sprite)) as Sprite;
            cooldownOverlayDebuff.enabled = false;

            fireDotTimerCurrent = 0f;
            phaseID = 2;
            timeInPhase = 0;
        }
    }

    /// <summary>
    /// Air phase logic.
    /// </summary>
    private void AirPhase()
    {
        if (timeInPhase > phaseduration - 2.05f && timeInPhase < phaseduration - 1.95f) //emote for water phase
            GetComponent<Boss>().SetEmoteText(" " + emotePhaseTwo);

        List<IRaider> targetDict = new List<IRaider>(RaiderDB.GetInstance().GetAllRaiders());

        foreach (IRaider target in targetDict) //set bossmod text for air debuff strength
        {
            AlakirWasserDebuff wasserBuff = target.GetGameObject().GetComponent<AlakirWasserDebuff>();
            if (wasserBuff != null)
            {
                string text = "" + (100 - (wasserBuff.getHealMultiplier() * 100));
                BossModTextDebuff.text = text.Split('.')[0];
                break;
            }
        }

        if (timeInPhase > phaseduration) //switch to water phase
        {
            targetDict = new List<IRaider>(RaiderDB.GetInstance().GetAllRaiders());

            foreach (IRaider target in targetDict)
            {
                AlakirFeuerDebuff feuerBuff = target.GetGameObject().GetComponent<AlakirFeuerDebuff>();
                if (feuerBuff != null)
                {
                    feuerBuff.Destroy();
                }

                AlakirWasserDebuff wasserBuff = target.GetGameObject().GetComponent<AlakirWasserDebuff>();
                if (wasserBuff != null)
                {
                    wasserBuff.Destroy();
                }
            }

            BossModImagePhase.sprite = Resources.Load("Feuer_Debuff", typeof(Sprite)) as Sprite;
            BossModImageDebuff.sprite = Resources.Load("Blizzard", typeof(Sprite)) as Sprite;
            BossModTextDebuff.text = "";
            cooldownOverlayDebuff.enabled = true;

            phaseID = 3;
            timeInPhase = 0;
        }
    }

    /// <summary>
    /// Water Phase logic
    /// </summary>
    private void WaterPhase()
    {
        frostTimerCurrent += 0.02f;

        cooldownOverlayDebuff.fillAmount = frostTimerCurrent / frostTimer;

        if (frostTimerCurrent >= frostTimer) //deal raidwide damage
        {
            frostTimerCurrent = 0f;

            List<IRaider> targetDict = new List<IRaider>(RaiderDB.GetInstance().GetAllRaiders());

            foreach (IRaider raider in targetDict)
            {
                raider.Damage(frostDamage);
            }
        }

        if (timeInPhase > phaseduration - 2.05f && timeInPhase < phaseduration - 1.95f) //emote for fire phase
            GetComponent<Boss>().SetEmoteText(" " + emotePhaseThree);

        else if (timeInPhase > phaseduration) //siwtch to fire phase
        {
            phaseID = 1;
            timeInPhase = 0;
            fireDotTimerCurrent = fireDotTimerStart;

            BossModImagePhase.sprite = Resources.Load("Luft", typeof(Sprite)) as Sprite;
            BossModImageDebuff.sprite = Resources.Load("Feuer_Debuff", typeof(Sprite)) as Sprite;
        }
    }
}

