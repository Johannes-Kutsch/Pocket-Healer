using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ApplyHealDebuff : MonoBehaviour
{
    private IRaider target = null;
    private List<IRaider> targetDict = new List<IRaider>();
    public string emoteText = "trägt Gifte auf ihre Waffe auf.";

    public float swingTimerCurrent;
    public float swingTimer;


    void FixedUpdate()
    {
        swingTimerCurrent += 0.02f;
        if (swingTimerCurrent > swingTimer - 2.05f && swingTimerCurrent < swingTimer - 1.95f)
            GetComponent<Boss>().SetEmoteText(" " + emoteText);
        if (swingTimerCurrent >= swingTimer)
        {
            targetDict = new List<IRaider>(RaiderDB.GetInstance().GetAllDDs());
            if (target != null && targetDict.Count > 1)
            {
                targetDict.Remove(target);
            }
            if (targetDict.Count == 0)
            {
                targetDict = new List<IRaider>(RaiderDB.GetInstance().GetAllRaider());
            }
            target = targetDict[Random.Range(0, targetDict.Count)];
            HealDebuff debuff = target.GetGameObject().AddComponent<HealDebuff>();
            target.GetGameObject().GetComponent<BuffManager>().RegisterBuff(debuff);
            swingTimerCurrent = 0f;
        }
    }
}