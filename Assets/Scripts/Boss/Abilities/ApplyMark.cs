using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

/// <summary>
/// Routine used to apply the Mark debuff.
/// </summary>
public class ApplyMark : MonoBehaviour
{
    private Raider target = null;
    private List<Raider> targetDict = new List<Raider>();
    public string emoteText = "markiert einen Spieler deiner Gruppe";

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

        swingTimer = settings.applyMarkDebuffTimer;
        swingTimerCurrent = settings.applyMarkDebuffTimerStart;

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
    /// Called on every fixed update.
    /// </summary>
    void FixedUpdate()
    {
        swingTimerCurrent += 0.02f;
        if (swingTimerCurrent > swingTimer - 2.05f && swingTimerCurrent < swingTimer - 1.95f)
            GetComponent<Boss>().SetEmoteText(" " + emoteText);

        cooldownOverlay.fillAmount = swingTimerCurrent / swingTimer;

        if (swingTimerCurrent >= swingTimer)
        {
            targetDict = new List<Raider>(RaiderDB.GetInstance().GetAllDDs());
            if (target != null && targetDict.Count > 1)
            {
                targetDict.Remove(target);
            }
            if(targetDict.Count == 0)
            {
                targetDict = new List<Raider>(RaiderDB.GetInstance().GetAllRaiders());
            }
            target = targetDict[Random.Range(0, targetDict.Count)];
            target.GetGameObject().AddComponent<MarkDebuff>();
            swingTimerCurrent = 0f;
        }
    }
}
