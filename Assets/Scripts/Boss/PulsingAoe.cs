using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PulsingAoe : MonoBehaviour {
    private IRaider target = null;
    private List<IRaider> targetDict = new List<IRaider>();
    private Coroutine timer;
    //private bool canAttack = false;
    private float swingTimerCurrent;
    private float rocksBetweenAoeCurrent;
    private int aoeCount = 0;
    public float swingTimer;
    public float dmgRock;
    public int numberTargetsRock;
    public string emoteText1 = "holt mit seinem Schwanz aus.";
    public string emoteText2 = "ist kurz davor seinen feurigen Atem über deiner Gruppe zu entladen.";

    public float rocksBetweenAoe;
    public float dmgAoe;
    public float ticksAoe;
    

    void Start()
    {
        if (GameControl.control.difficulty == 0)
        {
            dmgRock *= GameControl.control.easyMultiplyer;
            dmgAoe *= GameControl.control.easyMultiplyer;
        }
        rocksBetweenAoeCurrent = rocksBetweenAoe;
    }

    void FixedUpdate()
    {
        swingTimerCurrent += 0.02f;
        if (swingTimerCurrent > swingTimer - 2.05f && swingTimerCurrent < swingTimer - 1.95f && rocksBetweenAoeCurrent > 0)
            GetComponent<Boss>().SetEmoteText(" " + emoteText1);
        else if (swingTimerCurrent > swingTimer - 2.05f && swingTimerCurrent < swingTimer - 1.95f && rocksBetweenAoeCurrent <= 0)
            GetComponent<Boss>().SetEmoteText(" " + emoteText2);
        if (swingTimerCurrent >= swingTimer && rocksBetweenAoeCurrent > 0)
        {
            targetDict = new List<IRaider>(RaiderDB.GetInstance().GetAllDDs());
            for (int i = 0; i < numberTargetsRock; i++)
            {
                if (target != null && targetDict.Count > 1)
                {
                    targetDict.Remove(target);
                }
                target = targetDict[Random.Range(0, targetDict.Count)];
                target.Damage(dmgRock);
            }
            rocksBetweenAoeCurrent--;
            swingTimerCurrent = 0f;
        } else if(swingTimerCurrent >= swingTimer && rocksBetweenAoeCurrent <= 0)
        {
            targetDict = new List<IRaider>(RaiderDB.GetInstance().GetAllRaider());
            foreach (IRaider raider in targetDict)
            {
                raider.Damage(dmgAoe);
            }
            aoeCount++;
            swingTimerCurrent = swingTimer - 0.4f;
            if (aoeCount >= ticksAoe)
            {
                aoeCount = 0;
                rocksBetweenAoeCurrent = rocksBetweenAoe;
                swingTimerCurrent = 0f;
            }
            
        }
    }
}