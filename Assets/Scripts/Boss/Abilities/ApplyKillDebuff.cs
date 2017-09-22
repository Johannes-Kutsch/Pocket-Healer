using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ApplyKillDebuff : MonoBehaviour {
    private IRaider target = null;
    private List<IRaider> targetDict = new List<IRaider>();
    public string emoteText = "bereitet sich darauf vor einen mächtigen Fluch auszusprechen.";

    private bool canAttack = false;
    public float swingTimerCurrent;
    public float swingTimer;

    void Start()
    {
        swingTimerCurrent = -2f;
    }

    void FixedUpdate()
    {
        swingTimerCurrent += 0.02f;
        if (swingTimerCurrent > swingTimer - 2.05f && swingTimerCurrent < swingTimer - 1.95f)
            GetComponent<Boss>().SetEmoteText(" " + emoteText);
        if (swingTimerCurrent >= swingTimer)
        {
            targetDict = new List<IRaider>(RaiderDB.GetInstance().GetAllRaiders());
            if (target != null && targetDict.Count > 1)
            {
                targetDict.Remove(target);
            }
            if (target != null && targetDict.Count > 1 && target.GetGameObject().GetComponent<HealDebuff>())
            {
                targetDict.Remove(target);
            }
            target = targetDict[Random.Range(0, targetDict.Count)];
            KillDebuff debuff = target.GetGameObject().AddComponent<KillDebuff>();
            target.GetGameObject().GetComponent<BuffManager>().RegisterBuff(debuff);
            swingTimerCurrent = 0f;
        }
    }
}