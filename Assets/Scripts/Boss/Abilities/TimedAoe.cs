using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

/// <summary>
/// Damages all raider in a fixed intervall.
/// </summary>
public class TimedAoe : MonoBehaviour {
    private List<IRaider> targetDict = new List<IRaider>();

    public int levelIndex;
    public Image BossModImage;
    private Image cooldownOverlay;

    private float swingTimerCurrent;
    private float swingTimer;
    private float dmg;
    public string emoteText = "gets angry and is about to trample on your group.";

    /// <summary>
    /// Called on start.
    /// </summary>
    void Start()
    {
        Settings settings = new Settings(levelIndex);

        swingTimer = settings.timedAOESwingTimer;
        dmg = settings.timedAOEAttackDmg;

        if (GameControl.control.difficulty == 0)
        {
            dmg *= GameControl.control.easyMultiplyer;
        }

        swingTimerCurrent = -1f;

        BossModImage.sprite = Resources.Load("ThrowRock", typeof(Sprite)) as Sprite;
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
    }

    /// <summary>
    /// Called on every fixed update, i.e. 50 times a second.
    /// Increases the swingtimer, advances the bossmod graphic and cheks if we need to display the emote or can initiate the attack routine.
    /// </summary>
    void FixedUpdate()
    {
        swingTimerCurrent += 0.02f;

        cooldownOverlay.fillAmount = swingTimerCurrent / swingTimer;

        if (swingTimerCurrent > swingTimer - 2.05f && swingTimerCurrent < swingTimer - 1.95f)
            GetComponent<Boss>().SetEmoteText(" " + emoteText);

        if (swingTimerCurrent >= swingTimer)
        {
            Attack();
        }
    }

    /// <summary>
    /// The attack routine.
    /// Select every raider and damage him.
    /// </summary>
    private void Attack()
    {
        targetDict = new List<IRaider>(RaiderDB.GetInstance().GetAllRaiders());
        foreach (IRaider raider in targetDict)
        {
            raider.Damage(dmg);
        }
        swingTimerCurrent = 0f;
    }
}
