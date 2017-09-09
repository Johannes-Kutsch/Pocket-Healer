using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DragonSummon : MonoBehaviour
{
    public Red red;
    public Blue blue;
    public Green green;
    public float swingTimerCurrent;
    public float swingTimerRed;
    public float swingTimerBlue;
    public float swingTimerGreen;
    private bool redSummond = false;
    private bool blueSummond = false;
    private bool greenSummond = false;

    void Start()
    {
    }

    void FixedUpdate()
    {
        swingTimerCurrent += 0.02f;
        if (swingTimerCurrent > swingTimerRed - 2.05f && swingTimerCurrent < swingTimerRed - 1.95f)
            GetComponent<Boss>().SetEmoteText(" ruft einen roten Drachen herbei. Heile ihn damit er deine Gruppe unterstützt.");
        else if (swingTimerCurrent > swingTimerBlue - 2.05f && swingTimerCurrent < swingTimerBlue - 1.95f)
            GetComponent<Boss>().SetEmoteText(" ruft einen bluen Drachen herbei. Heile ihn damit er deine Gruppe unterstützt.");
        else if (swingTimerCurrent > swingTimerGreen - 2.05f && swingTimerCurrent < swingTimerGreen - 1.95f)
            GetComponent<Boss>().SetEmoteText(" ruft einen grünen Drachen herbei. Heile ihn damit er deine Gruppe unterstützt.");
        if (swingTimerCurrent >= swingTimerRed && !redSummond)
        {
            red.Summon();
            redSummond = true;
        } else if (swingTimerCurrent >= swingTimerBlue && !blueSummond)
        {
            blue.Summon();
            blueSummond = true;
        } else if (swingTimerCurrent >= swingTimerGreen && !greenSummond)
        {
            greenSummond = true;
            green.Summon();
        }
    }
}
