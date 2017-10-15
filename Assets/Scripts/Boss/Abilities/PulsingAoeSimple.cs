using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Script that initaites a pulsing aoe (multiple eventy that damage each raider) in a fixed intervall.
/// </summary>
public class PulsingAoeSimple : MonoBehaviour {
    public int levelIndex;
    public Image BossModImage;
    private Image cooldownOverlay;

    private float swingTimer;
    private float swingTimerPulsing;
    private float swingTimerCurrent = 0f;
    private int aoeCount = 0;
    private float dmgAoe;
    private int ticksAoe;
    private static float DELAYAOE = 0.4f;

    public string emoteText = "is about to unleash his putrid breath over your group.";

    /// <summary>
    /// Called on start.
    /// </summary>
    void Start () {
        Settings settings = new Settings(levelIndex);

        swingTimer = settings.pulsingAOESwingTimer;
        dmgAoe = settings.pulsingAOEDmgAoe;
        ticksAoe = settings.pulsingAOETicksAoe;

        swingTimerPulsing = swingTimer;

        Image[] cooldownOverlays = BossModImage.GetComponentsInChildren<Image>();

        foreach (Image image in cooldownOverlays)
        {
            if (image.transform != BossModImage.transform)
            {
                cooldownOverlay = image;
            }
        }

        BossModImage.enabled = true;
        cooldownOverlay.enabled = true;

        if (GameControl.control.difficulty == 0)
        {
            dmgAoe *= GameControl.control.easyMultiplier;
        }

        
    }

    /// <summary>
    /// Called once every fixed update.
    /// </summary>
    void FixedUpdate() {
        swingTimerCurrent += 0.02f;

        if (swingTimerCurrent > swingTimerPulsing - 2.05f && swingTimerCurrent < swingTimerPulsing - 1.95f)
            GetComponent<Boss>().SetEmoteText(" " + emoteText);

        cooldownOverlay.fillAmount = swingTimerCurrent / swingTimerPulsing;

        if (swingTimerCurrent >= swingTimerPulsing)
        {
            List<Raider> targetDict = new List<Raider>(RaiderDB.GetInstance().GetAllRaiders());

            foreach (Raider raider in targetDict)
            {
                raider.Damage(dmgAoe);
            }

            aoeCount++;
            swingTimerCurrent = 0f;

            if (aoeCount >= ticksAoe) //pulsing is over
            {
                aoeCount = 0;
                swingTimerPulsing = swingTimer;
            }
            else
            {
                swingTimerPulsing = DELAYAOE;
            }
        }
    }
}
