using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TimedAoe : MonoBehaviour {
    private List<IRaider> targetDict = new List<IRaider>();

    //private bool canAttack = false;
    public float swingTimerCurrent;
    public float swingTimer;
    public float dmg;
    public string emoteText = "wird wütend und macht sich bereit deine Gruppe zu zertrampeln.";

    void Start()
    {
        if (GameControl.control.difficulty == 0)
        {
            dmg *= GameControl.control.easyMultiplyer;
        }
        swingTimerCurrent = -1f;
    }

    void FixedUpdate()
    {
        swingTimerCurrent += 0.02f;
        if (swingTimerCurrent > swingTimer - 2.05f && swingTimerCurrent < swingTimer - 1.95f)
            GetComponent<Boss>().SetEmoteText(" " + emoteText);
        if (swingTimerCurrent >= swingTimer)
        {
            targetDict = new List<IRaider>(RaiderDB.GetInstance().GetAllRaider());
            foreach(IRaider raider in targetDict)
            {
                raider.ReduceHP(dmg);
            }
            swingTimerCurrent = 0f;
        }
    }
}
