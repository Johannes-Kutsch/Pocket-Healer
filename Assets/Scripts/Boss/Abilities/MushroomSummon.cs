using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

/// <summary>
/// Script used to enable the mushroom.
/// </summary>
public class MushroomSummon : MonoBehaviour
{
    public int levelIndex;
    public Image BossModImage;
    private Image cooldownOverlay;

    public Mushroom mushroom;
    private float swingTimerCurrent;
    private float swingTimer;

    /// <summary>
    /// Called on start.
    /// </summary>
    private void Start()
    {
        Settings settings = new Settings(levelIndex);

        swingTimer = settings.mushroomSwingTimer;
        swingTimerCurrent = settings.mushroomSwingTimerStart;

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
        if (swingTimerCurrent > swingTimer - 2.05f && swingTimerCurrent < swingTimer - 1.95f)
            GetComponent<Boss>().SetEmoteText(" is about to summon a mushroom. Heal it to support your party.");

        cooldownOverlay.fillAmount = swingTimerCurrent / swingTimer;

        if (swingTimerCurrent >= swingTimer)
        {
            mushroom.Summon();
            swingTimerCurrent = 0f;
        }
    }
}
