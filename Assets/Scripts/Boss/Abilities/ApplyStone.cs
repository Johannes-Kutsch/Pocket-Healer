using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

/// <summary>
/// Script used to apply the two Stone debuffs.
/// Debuff one is a short ticking debuff that expries and deals raidwide damage if the target is healed to full.
/// Debuff two is a ticking debuff that has no duration and expires if the target is healed to full.
/// </summary>
public class ApplyStone : MonoBehaviour {

    private IRaider target = null;
    private List<IRaider> targetDict;
    public string emote = "lässt zwei Spieler zu stein erstarren.";

    public int levelIndex;
    public Image BossModImage;
    private Image cooldownOverlay;
    public Image BossModImage2;
    private Image cooldownOverlay2;

    private float swingTimerCurrent;
    private float swingTimer;
    private float swingTimerStart;
    private float dmg1;
    private float dmg2;

    /// <summary>
    /// Called on start.
    /// </summary>
    void Start()
    {
        Settings settings = new Settings(levelIndex);

        swingTimer = settings.applyStoneDebuffTimer;
        swingTimerCurrent = settings.applyStoneDebuffTimerStart;
        dmg1 = settings.applyStoneDmg1;
        dmg2 = settings.applyStoneDmg2;

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

        cooldownOverlays = BossModImage2.GetComponentsInChildren<Image>();

        foreach (Image image in cooldownOverlays)
        {
            if (image.transform != BossModImage2.transform)
            {
                cooldownOverlay2 = image;
            }
        }

        BossModImage2.enabled = true;
        cooldownOverlay2.enabled = true;
    }

    /// <summary>
    /// Called on every fixed update
    /// </summary>
    void FixedUpdate()
    {
        swingTimerCurrent += 0.02f;
        if (swingTimerCurrent > swingTimer - 2.05f && swingTimerCurrent < swingTimer - 1.95f)
            GetComponent<Boss>().SetEmoteText(" " + emote);

        cooldownOverlay.fillAmount = swingTimerCurrent / swingTimer;
        cooldownOverlay2.fillAmount = swingTimerCurrent / swingTimer;

        if (swingTimerCurrent >= swingTimer)
        {
            targetDict = new List<IRaider>(RaiderDB.GetInstance().GetAllDDsSortedByHealth());
            foreach (IRaider raider in targetDict)
            {
                if (!raider.GetGameObject().GetComponent<StoneOne>() && !raider.GetGameObject().GetComponent<StoneTwo>())
                {
                    target = raider;
                }
            }

            if (target == null)
            {
                targetDict = new List<IRaider>(RaiderDB.GetInstance().GetAllRaidersSortedByHealth());
                foreach (IRaider raider in targetDict)
                {
                    if (!raider.GetGameObject().GetComponent<StoneOne>() && !raider.GetGameObject().GetComponent<StoneTwo>())
                    {
                        target = raider;
                    }
                }
            }

            if (target != null)
            {
                target.Damage(dmg1);
                StoneOne debuff = target.GetGameObject().AddComponent<StoneOne>();
                target.GetGameObject().GetComponent<BuffManager>().RegisterBuff(debuff);
                target = null;
            }

            targetDict = new List<IRaider>(RaiderDB.GetInstance().GetAllDDsSortedByHealth());
            foreach (IRaider raider in targetDict)
            {
                if (!raider.GetGameObject().GetComponent<StoneOne>() && !raider.GetGameObject().GetComponent<StoneTwo>())
                {
                    target = raider;
                }
            }

            if (target == null)
            {
                targetDict = new List<IRaider>(RaiderDB.GetInstance().GetAllRaidersSortedByHealth());
                foreach (IRaider raider in targetDict)
                {
                    if (!raider.GetGameObject().GetComponent<StoneOne>() && !raider.GetGameObject().GetComponent<StoneTwo>())
                    {
                        target = raider;
                    }
                }
            }

            if (target != null)
            {
                target.Damage(dmg2);
                StoneTwo debuff = target.GetGameObject().AddComponent<StoneTwo>();
                target.GetGameObject().GetComponent<BuffManager>().RegisterBuff(debuff);
                target = null;
            }

            swingTimerCurrent = 0f;
        }
    }
}
