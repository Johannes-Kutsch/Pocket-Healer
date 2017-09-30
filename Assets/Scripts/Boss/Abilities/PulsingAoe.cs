using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

/// <summary>
/// Alterantes between a dmg nuke to multiple targets and an  pulsing aoe.
/// </summary>
public class PulsingAoe : MonoBehaviour {
    private List<IRaider> lastTargetDict = new List<IRaider>();

    public int levelIndex;
    public Image BossModImageRock;
    private Image cooldownOverlayRock;
    public Image BossModImagePulsing;
    private Image cooldownOverlayPulsing;

    private float swingTimerRockCurrent;
    private float swingTimerPulsingCurrent;
    private float rocksBetweenAoeRemaining;
    private int aoeCount = 0;
    private static float DELAYAOE = 0.4f;

    private float swingTimerRock;
    private float swingTimerPulsing;

    public string emoteText1 = "is about to take a swing with her tail.";
    public string emoteText2 = "is about to unleash her fiery breath over your group.";

    private float swingTimer;
    private float dmgRock;
    private int numberTargetsRock;
    private int rocksBetweenAoe;
    private float dmgAoe;
    private int ticksAoe;

    /// <summary>
    /// Called on start.
    /// </summary>
    void Start()
    {
        Settings settings = new Settings(levelIndex);

        swingTimer = settings.pulsingAOESwingTimer;
        dmgRock = settings.pulsingAOEDmgRock;
        numberTargetsRock = settings.pulsingAOENumberTargetsRock;
        rocksBetweenAoe = settings.pulsingAOERocksBetweenAoe;
        dmgAoe = settings.pulsingAOEDmgAoe;
        ticksAoe = settings.pulsingAOETicksAoe;

        swingTimerRock = swingTimer;
        swingTimerPulsing = swingTimer * (rocksBetweenAoe + 1);

        Image[] cooldownOverlays = BossModImageRock.GetComponentsInChildren<Image>();

        foreach (Image image in cooldownOverlays)
        {
            if (image.transform != BossModImageRock.transform)
            {
                cooldownOverlayRock = image;
            }
        }

        BossModImageRock.enabled = true;
        cooldownOverlayRock.enabled = true;

        cooldownOverlays = BossModImagePulsing.GetComponentsInChildren<Image>();

        foreach (Image image in cooldownOverlays)
        {
            if (image.transform != BossModImagePulsing.transform)
            {
                cooldownOverlayPulsing = image;
            }
        }

        BossModImagePulsing.enabled = true;
        cooldownOverlayPulsing.enabled = true;

        if (GameControl.control.difficulty == 0)
        {
            dmgRock *= GameControl.control.easyMultiplyer;
            dmgAoe *= GameControl.control.easyMultiplyer;
        }
        rocksBetweenAoeRemaining = rocksBetweenAoe;
    }

    /// <summary>
    /// Called once every fixed update.
    /// </summary>
    void FixedUpdate()
    {
        swingTimerRockCurrent += 0.02f;
        swingTimerPulsingCurrent += 0.02f;

        if (swingTimerRockCurrent > swingTimerRock - 2.05f && swingTimerRockCurrent < swingTimerRock - 1.95f)
            GetComponent<Boss>().SetEmoteText(" " + emoteText1);
        else if (swingTimerPulsingCurrent > swingTimerPulsing - 2.05f && swingTimerPulsingCurrent < swingTimerPulsing - 1.95f)
            GetComponent<Boss>().SetEmoteText(" " + emoteText2);

        //cooldownoverlay rock
        cooldownOverlayRock.fillAmount = swingTimerRockCurrent / swingTimerRock;

        //cooldownoverlay pusling
        cooldownOverlayPulsing.fillAmount = swingTimerPulsingCurrent / swingTimerPulsing;

        //abilitie logic
        if (swingTimerRockCurrent >= swingTimerRock) //throw a rock
        {
            AttackRock();
            rocksBetweenAoeRemaining--;
            swingTimerRockCurrent = 0f;

            if (rocksBetweenAoeRemaining > 0)
            {
                swingTimerRock = swingTimer;
            } else
            {
                swingTimerRock = swingTimer * 2 + DELAYAOE * (ticksAoe - 1);
            }
        }

        if(swingTimerPulsingCurrent >= swingTimerPulsing) //enough rocks thrown, time for the pulsing aoe
        {
            AttackPulsing();

            aoeCount++;
            swingTimerPulsingCurrent = 0f;

            if (aoeCount >= ticksAoe) //pulsing is over
            {
                aoeCount = 0;
                rocksBetweenAoeRemaining = rocksBetweenAoe;
                swingTimerPulsing = swingTimer * (rocksBetweenAoe + 1);
                swingTimerRockCurrent = swingTimer;
            } else
            {
                swingTimerPulsing = DELAYAOE;
            }
        }
    }

    /// <summary>
    /// The rock attack routine.
    /// first select random dd's not hit last round
    /// if not enought targets are hit select random dd's not hit last round
    /// if still not enought targets are hit select random tanks
    /// if still not enought targets are hit, go ham and select random raiders until enought targets are hit
    /// </summary>
    private void AttackRock()
    {

        List<IRaider> targetDict = null;
        int targetsLeft = numberTargetsRock;

        if (targetsLeft > 0) //only hit dds not hit in the last round
        {
            targetDict = new List<IRaider>(RaiderDB.GetInstance().GetAllDDs());
            foreach (IRaider raider in lastTargetDict)
            {
                if (targetDict.Contains(raider))
                {
                    targetDict.Remove(raider);
                }
            }

            lastTargetDict = new List<IRaider>();

            while (targetsLeft > 0 && targetDict.Count > 0)
            {
                IRaider target = targetDict[Random.Range(0, targetDict.Count)];
                targetDict.Remove(target);
                lastTargetDict.Add(target);
                target.Damage(dmgRock);
                targetsLeft--;
            }
        }

        if (targetsLeft > 0) //only hit dds not jet hit in this round
        {
            targetDict = new List<IRaider>(RaiderDB.GetInstance().GetAllDDs());
            foreach (IRaider raider in lastTargetDict)
            {
                if (targetDict.Contains(raider))
                {
                    targetDict.Remove(raider);
                }
            }

            while (targetsLeft > 0 && targetDict.Count > 0)
            {
                IRaider target = targetDict[Random.Range(0, targetDict.Count)];
                targetDict.Remove(target);
                lastTargetDict.Add(target);
                target.Damage(dmgRock);
                targetsLeft--;
            }
        }

        if (targetsLeft > 0) //hit tanks
        {
            targetDict = new List<IRaider>(RaiderDB.GetInstance().GetAllTanks());
            while (targetsLeft > 0 && targetDict.Count > 0)
            {
                IRaider target = targetDict[Random.Range(0, targetDict.Count)];
                targetDict.Remove(target);
                lastTargetDict.Add(target);
                target.Damage(dmgRock);
                targetsLeft--;
            }
        }

        while (targetsLeft > 0) //go wild, hit random raiders until enough are hit
        {
            targetDict = new List<IRaider>(RaiderDB.GetInstance().GetAllRaiders());

            while (targetsLeft > 0 && targetDict.Count > 0)
            {
                IRaider target = targetDict[Random.Range(0, targetDict.Count)];
                targetDict.Remove(target);
                target.Damage(dmgRock);
                targetsLeft--;
            }
        }
    }

    /// <summary>
    /// Damages every raider for the pulsing damage.
    /// </summary>
    private void AttackPulsing()
    {
        List<IRaider> targetDict = new List<IRaider>(RaiderDB.GetInstance().GetAllRaiders());

        foreach (IRaider raider in targetDict)
        {
            raider.Damage(dmgAoe);
        }
    }
}