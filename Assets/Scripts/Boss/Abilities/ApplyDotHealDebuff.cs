using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ApplyDotHealDebuff : MonoBehaviour
{
    private IRaider target = null;
    private List<IRaider> targetDict = new List<IRaider>();
    public string emoteText = "trägt Gifte auf ihre Waffe auf.";
    public string emoteText2 = "erschüttert die Seele eines Gruppenmitgliedes und verhindert das er geheilt werden kann.";
    private bool healDebuff;
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
        if (swingTimerCurrent > swingTimer - 2.05f && swingTimerCurrent < swingTimer - 1.95f && !healDebuff)
            GetComponent<Boss>().SetEmoteText(" " + emoteText);
        else if (swingTimerCurrent > swingTimer - 2.05f && swingTimerCurrent < swingTimer - 1.95f && healDebuff)
            GetComponent<Boss>().SetEmoteText(" " + emoteText2);
        if (swingTimerCurrent >= swingTimer)
        {
            targetDict = new List<IRaider>(RaiderDB.GetInstance().GetAllDDs());
            if(targetDict.Count <= 0)
            {
                targetDict = new List<IRaider>(RaiderDB.GetInstance().GetAllRaiders());
            }
            if (target != null && targetDict.Count > 1)
            {
                targetDict.Remove(target);
            }
            if (healDebuff)
            {
                target = targetDict[Random.Range(0, targetDict.Count)];
                HealDebuff debuff = target.GetGameObject().AddComponent<HealDebuff>();
                target.GetGameObject().GetComponent<BuffManager>().RegisterBuff(debuff);
                healDebuff = false;
            }
            else if(!healDebuff)
            {
                target = targetDict[Random.Range(0, targetDict.Count)];
                DotDebuff debuff = target.GetGameObject().AddComponent<DotDebuff>();
                target.GetGameObject().GetComponent<BuffManager>().RegisterBuff(debuff);
                healDebuff = true;
            }
            swingTimerCurrent = 0f;
        }
    }
}