using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

/// <summary>
/// Applies a debuff that kills the party member after 10 seconds if not dispelled.
/// </summary>
public class ApplyKillDebuff : MonoBehaviour {
    private IRaider target = null;
    private List<IRaider> targetDict = new List<IRaider>();
    public string emoteText = "prepares to curse one of your partymembers.";

    public int levelIndex;
    public Image BossModImage;
    private Image cooldownOverlay;

    private float swingTimerCurrent;
    private float swingTimer;

    /// <summary>
    /// Called on start.
    /// </summary>
    void Start()
    {
        Settings settings = new Settings(levelIndex);

        swingTimer = settings.applyKillDebuffTimer;
        swingTimerCurrent = settings.applyKillDebuffTimerStart;

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
    /// Called on every fixed update, e.i. 50 times a second
    /// </summary>
    void FixedUpdate()
    {
        swingTimerCurrent += 0.02f;
        if (swingTimerCurrent > swingTimer - 2.05f && swingTimerCurrent < swingTimer - 1.95f)
            GetComponent<Boss>().SetEmoteText(" " + emoteText);

        cooldownOverlay.fillAmount = swingTimerCurrent / swingTimer;

        if (swingTimerCurrent >= swingTimer)
        {
            targetDict = new List<IRaider>(RaiderDB.GetInstance().GetAllRaiders());
            if (target != null && targetDict.Count > 1) //remove last target if more than 1 potential target
            {
                targetDict.Remove(target);
            }

            target = targetDict[Random.Range(0, targetDict.Count)];
            KillDebuff debuff = target.GetGameObject().AddComponent<KillDebuff>();
            target.GetGameObject().GetComponent<BuffManager>().RegisterBuff(debuff);
            swingTimerCurrent = 0f;
        }
    }
}