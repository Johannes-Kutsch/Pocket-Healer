using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

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

    void Start()
    {
        Settings settings = new Settings(levelIndex);

        swingTimer = settings.applyStoneDebuffTimer;
        swingTimerCurrent = settings.applyStoneDebuffTimerStart;

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

    void FixedUpdate()
    {
        swingTimerCurrent += 0.02f;
        if (swingTimerCurrent > swingTimer - 2.05f && swingTimerCurrent < swingTimer - 1.95f)
            GetComponent<Boss>().SetEmoteText(" " + emote);

        cooldownOverlay.fillAmount = swingTimerCurrent / swingTimer;
        cooldownOverlay2.fillAmount = swingTimerCurrent / swingTimer;

        if (swingTimerCurrent >= swingTimer)
        {
            targetDict = new List<IRaider>(RaiderDB.GetInstance().GetAllDDs());
            foreach (IRaider raider in targetDict)
            {
                if (!raider.GetGameObject().GetComponent<SteinOne>() && !raider.GetGameObject().GetComponent<SteinTwo>())
                {
                    target = raider;
                }
            }
            if (target == null)
            {
                targetDict = new List<IRaider>(RaiderDB.GetInstance().GetAllRaiders());
                foreach (IRaider raider in targetDict)
                {
                    if (!raider.GetGameObject().GetComponent<SteinOne>() && !raider.GetGameObject().GetComponent<SteinTwo>())
                    {
                        target = raider;
                    }
                }
            }
            if (target != null)
            {
                target.Damage(20f);
                SteinOne debuff = target.GetGameObject().AddComponent<SteinOne>();
                target.GetGameObject().GetComponent<BuffManager>().RegisterBuff(debuff);
                target = null;
            }
            targetDict = new List<IRaider>(RaiderDB.GetInstance().GetAllDDs());
            foreach (IRaider raider in targetDict)
            {
                if (!raider.GetGameObject().GetComponent<SteinOne>() && !raider.GetGameObject().GetComponent<SteinTwo>())
                {
                    target = raider;
                }
            }
            if (target == null)
            {
                targetDict = new List<IRaider>(RaiderDB.GetInstance().GetAllRaiders());
                foreach (IRaider raider in targetDict)
                {
                    if (!raider.GetGameObject().GetComponent<SteinOne>() && !raider.GetGameObject().GetComponent<SteinTwo>())
                    {
                        target = raider;
                    }
                }
            }
            if (target != null)
            {
                target.Damage(30f);
                SteinTwo debuff = target.GetGameObject().AddComponent<SteinTwo>();
                target.GetGameObject().GetComponent<BuffManager>().RegisterBuff(debuff);
                target = null;
            }
            swingTimerCurrent = 0f;
        }
    }
}
