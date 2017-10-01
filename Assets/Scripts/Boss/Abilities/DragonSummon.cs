using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

/// <summary>
/// Script used during Scene 11 to summon the dragons.
/// Red Dragon: will double your healing and increases the maxhealth of every raider by 50.
/// Blue Dragon: will increase mana regeneration.
/// Green Dragon: will split half of your healing evenly among every raider.
/// </summary>
public class DragonSummon : MonoBehaviour
{
    public DragonRed red;
    public DragonBlue blue;
    public DragonGreen green;
    public int levelIndex;
    public Image BossModImage;
    private Image cooldownOverlay;

    private float swingTimerCurrent;
    private float swingTimer;
    private float swingTimerRed;
    private float swingTimerBlue;
    private float swingTimerGreen;
    private bool redSummond = false;
    private bool blueSummond = false;
    private bool greenSummond = false;

    /// <summary>
    /// Called on Start.
    /// </summary>
    void Start()
    {
        Settings settings = new Settings(levelIndex);
        swingTimerRed = settings.dragonSummonSwingTimerRed;
        swingTimerBlue = settings.dragonSummonSwingTimerBlue;
        swingTimerGreen = settings.dragonSummonSwingTimerGreen;
        swingTimer = swingTimerRed;

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
    /// Called with ever fixed update.
    /// </summary>
    void FixedUpdate()
    {
        swingTimerCurrent += 0.02f;

        cooldownOverlay.fillAmount = swingTimerCurrent / swingTimer;

        if (swingTimerCurrent > swingTimer - 2.05f && swingTimerCurrent < swingTimer - 1.95f && !redSummond)
            GetComponent<Boss>().SetEmoteText(" summons a red dragon. Heal him and he will increase your healing.");
        else if (swingTimerCurrent > swingTimer - 2.05f && swingTimerCurrent < swingTimer - 1.95f && !blueSummond)
            GetComponent<Boss>().SetEmoteText(" summons a blue dragon. Heal him and he will increase your manaregeneration.");
        else if (swingTimerCurrent > swingTimer - 2.05f && swingTimerCurrent < swingTimer - 1.95f && !greenSummond)
            GetComponent<Boss>().SetEmoteText(" summons a green dragon. Heal him and he will increase your healing.");

        if (swingTimerCurrent >= swingTimer && !redSummond) //summon red
        {
            red.Summon();
            redSummond = true;

            swingTimerCurrent = 0f;
            swingTimer = swingTimerBlue;

            BossModImage.sprite = Resources.Load("blue", typeof(Sprite)) as Sprite;

        } else if (swingTimerCurrent >= swingTimer && !blueSummond) //summon blue
        {
            blue.Summon();
            blueSummond = true;

            swingTimerCurrent = 0f;
            swingTimer = swingTimerGreen;

            BossModImage.sprite = Resources.Load("green", typeof(Sprite)) as Sprite;

        } else if (swingTimerCurrent >= swingTimer && !greenSummond) //summon green
        {
            green.Summon();
            greenSummond = true;

            swingTimerCurrent = 0f;

            BossModImage.enabled = false;
            cooldownOverlay.enabled = false;
        }
    }
}
