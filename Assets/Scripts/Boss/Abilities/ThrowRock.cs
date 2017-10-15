using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

/// <summary>
/// Damages one or multiple raiders, prefers dds
/// </summary>
public class ThrowRock : MonoBehaviour
{
    private List<Raider> lastTargetDict = new List<Raider>();

    public int levelIndex;
    public Image BossModImage;
    private Image cooldownOverlay;

    private float swingTimerCurrent = 4f;
    private float swingTimer;
    private float dmg;
    private int numberTargets;

    /// <summary>
    /// Called on start.
    /// </summary>
    void Start()
    {
        Settings settings = new Settings(levelIndex);

        swingTimer = settings.throwRockSwingTimer;
        dmg = settings.throwRockAttackDmg;
        numberTargets = settings.throwRockNumberTargets;

        if (GameControl.control.difficulty == 0)
        {
            dmg *= GameControl.control.easyMultiplier;
        }

        
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

        if (swingTimerCurrent > swingTimer - 2.15f && swingTimerCurrent < swingTimer - 1.55f)
            GetComponent<Boss>().SetEmoteText(" picks up a huge Stone.");

        if (swingTimerCurrent >= swingTimer)
        {
            Attack();
        }
    }

    /// <summary>
    /// The attack routine.
    /// first select random dd's not hit last round
    /// if not enought targets are hit select random dd's not hit last round
    /// if still not enought targets are hit select random tanks
    /// if still not enought targets are hit, go ham and select random raiders until enought targets are hit
    /// </summary>
    private void Attack()
    {

        List<Raider> targetDict = null;
        int targetsLeft = numberTargets;

        if (targetsLeft > 0) //only hit dds not hit in the last round
        {
            targetDict = new List<Raider>(RaiderDB.GetInstance().GetAllDDs());
            foreach (Raider raider in lastTargetDict)
            {
                if (targetDict.Contains(raider))
                {
                    targetDict.Remove(raider);
                }
            }

            lastTargetDict = new List<Raider>();

            while (targetsLeft > 0 && targetDict.Count > 0)
            {
                Raider target = targetDict[Random.Range(0, targetDict.Count)];
                targetDict.Remove(target);
                lastTargetDict.Add(target);
                target.Damage(dmg);
                targetsLeft--;
            }
        }

        if (targetsLeft > 0) //only hit dds not jet hit in this round
        {
            targetDict = new List<Raider>(RaiderDB.GetInstance().GetAllDDs());
            foreach (Raider raider in lastTargetDict)
            {
                if (targetDict.Contains(raider))
                {
                    targetDict.Remove(raider);
                }
            }

            while (targetsLeft > 0 && targetDict.Count > 0)
            {
                Raider target = targetDict[Random.Range(0, targetDict.Count)];
                targetDict.Remove(target);
                lastTargetDict.Add(target);
                target.Damage(dmg);
                targetsLeft--;
            }
        }

        if (targetsLeft > 0) //hit tanks
        {
            targetDict = new List<Raider>(RaiderDB.GetInstance().GetAllTanks());
            while (targetsLeft > 0 && targetDict.Count > 0)
            {
                Raider target = targetDict[Random.Range(0, targetDict.Count)];
                targetDict.Remove(target);
                lastTargetDict.Add(target);
                target.Damage(dmg);
                targetsLeft--;
            }
        }

        while (targetsLeft > 0) //go wild, hit random raiders until enough are hit
        {
            targetDict = new List<Raider>(RaiderDB.GetInstance().GetAllRaiders());

            while (targetsLeft > 0 && targetDict.Count > 0)
            {
                Raider target = targetDict[Random.Range(0, targetDict.Count)];
                targetDict.Remove(target);
                target.Damage(dmg);
                targetsLeft--;
            }
        }

        swingTimerCurrent = 0f;
    }
}
