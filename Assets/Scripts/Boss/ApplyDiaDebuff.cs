using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ApplyDiaDebuff : MonoBehaviour
{
    private IRaider target = null;
    private List<IRaider> targetDict = new List<IRaider>();
    private bool dotsApplyed = false;

    public string emoteText = "markiert drei Spieler deiner Gruppe";
    public float swingTimerCurrent;
    public float swingTimer;
    public float dotDuration;
    public int numberTargets = 3;


    void FixedUpdate()
    {
        swingTimerCurrent += 0.02f;
        if (swingTimerCurrent > swingTimer - 2.05f && swingTimerCurrent < swingTimer - 1.95f)
            GetComponent<Boss>().SetEmoteText(" " + emoteText);
        if (swingTimerCurrent >= swingTimer && !dotsApplyed)
        {
            targetDict = new List<IRaider>(RaiderDB.GetInstance().GetAllRaider());
            for (int i = 0; i < numberTargets; i++)
            {
                if (target != null && targetDict.Count > 1)
                {
                    targetDict.Remove(target);
                }
                target = targetDict[Random.Range(0, targetDict.Count)];
                DiaDebuff debuff = target.GetGameObject().AddComponent<DiaDebuff>();
                target.GetGameObject().GetComponent<BuffManager>().RegisterBuff(debuff);
                dotsApplyed = true;
            }
        }
        if (swingTimerCurrent >= dotDuration)
        {
            targetDict = new List<IRaider>(RaiderDB.GetInstance().GetAllRaider());
            foreach (IRaider target in targetDict)
            {
                DiaDebuff debuff = target.GetGameObject().GetComponent<DiaDebuff>();
                if (debuff != null)
                {
                    target.GetGameObject().GetComponent<BuffManager>().DeregisterBuff(debuff);
                    Destroy(debuff);
                }
            }
            dotsApplyed = false;
            swingTimerCurrent = 0f;
        }
    }
}
