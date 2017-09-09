using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NukeAoe : MonoBehaviour {
    private List<IRaider> targetDict = new List<IRaider>();
    public string emoteText;
    private bool canAttack = false;
    public float swingTimerCurrent;
    public float swingTimer;
    public float dmg;
    void Start()
    {
        if (GameControl.control.difficulty == 0)
        {
            dmg *= GameControl.control.easyMultiplyer;
        }
    }

    void FixedUpdate()
    {
        swingTimerCurrent += 0.02f;
        if (swingTimerCurrent > swingTimer - 2.05f && swingTimerCurrent < swingTimer - 1.95f)
            GetComponent<Boss>().SetEmoteText(" " + emoteText);
        if (swingTimerCurrent >= swingTimer)
        {
            targetDict = new List<IRaider>(RaiderDB.GetInstance().GetAllRaider());
            foreach (IRaider target in targetDict)
            {
                target.ReduceHP(dmg);
            }
            swingTimerCurrent = 0f;
        }
    }
}
