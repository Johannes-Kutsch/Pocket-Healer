using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PilzSummon : MonoBehaviour
{
    public Pilz pilz;
    public float swingTimerCurrent;
    public float swingTimer;

    void Start()
    {
    }

    void FixedUpdate()
    {
        swingTimerCurrent += 0.02f;
        if (swingTimerCurrent > swingTimer - 2.05f && swingTimerCurrent < swingTimer - 1.95f)
            GetComponent<Boss>().SetEmoteText(" ist dabei einen Pilz zu beschwören. Heile ihn damit er deine Gruppe unterstützt.");
        if (swingTimerCurrent >= swingTimer)
        {
            pilz.Summon();
            swingTimerCurrent = 0f;
        }
    }
}
