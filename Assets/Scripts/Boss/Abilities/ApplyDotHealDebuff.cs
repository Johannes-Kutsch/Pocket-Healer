using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

/// <summary>
/// This script alternates between apllying a heal imunity debuff and a dispellable damage dealing debuff.
/// </summary>
public class ApplyDotHealDebuff : MonoBehaviour
{
    public int levelIndex;
    public Image BossModImage;
    private Image cooldownOverlay;

    private Raider target = null;
    private bool healDebuff;
    private bool canAttack = false;
    private float swingTimerCurrent;
    private float swingTimer;

    public string emoteText = "applys poison to her weapon.";
    public string emoteText2 = "shakes the soul of a group member and prevents it from being healed.";

    /// <summary>
    /// Called on Start.
    /// </summary>
    void Start()
    {
        Settings settings = new Settings(levelIndex);

        swingTimer = settings.applyDotHealDebuffSwingTimer;
        swingTimerCurrent = settings.applyDotHealDebuffSwingTimerStart;

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
    /// Called with every fixed update.
    /// </summary>
    void FixedUpdate()
    {
        swingTimerCurrent += 0.02f;

        if (swingTimerCurrent > swingTimer - 2.05f && swingTimerCurrent < swingTimer - 1.95f && !healDebuff)
            GetComponent<Boss>().SetEmoteText(" " + emoteText);
        else if (swingTimerCurrent > swingTimer - 2.05f && swingTimerCurrent < swingTimer - 1.95f && healDebuff)
            GetComponent<Boss>().SetEmoteText(" " + emoteText2);

        cooldownOverlay.fillAmount = swingTimerCurrent / swingTimer;

        if (swingTimerCurrent >= swingTimer)
        {
            List<Raider> targetDict = new List<Raider>(RaiderDB.GetInstance().GetAllDDs());
            if(targetDict.Count <= 0)
            {
                targetDict = new List<Raider>(RaiderDB.GetInstance().GetAllRaiders());
            }
            if (target != null && targetDict.Count > 1)
            {
                targetDict.Remove(target);
            }
            if (healDebuff)
            {
                target = targetDict[Random.Range(0, targetDict.Count)];
                target.GetGameObject().AddComponent<HealDebuff>();
                healDebuff = false;
                BossModImage.sprite = Resources.Load("Dot_Debuff", typeof(Sprite)) as Sprite;
            }
            else if(!healDebuff)
            {
                target = targetDict[Random.Range(0, targetDict.Count)];
                target.GetGameObject().AddComponent<DotDebuff>();
                healDebuff = true;
                BossModImage.sprite = Resources.Load("Heal_Debuff", typeof(Sprite)) as Sprite;
            }
            swingTimerCurrent = 0f;
        }
    }
}