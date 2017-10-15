using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Script used in Scene 12 (currently not finished)
/// </summary>
public class ApplyDiaDebuff : MonoBehaviour
{
    private Raider target = null;
    private List<Raider> targetDict = new List<Raider>();
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
            targetDict = new List<Raider>(RaiderDB.GetInstance().GetAllRaiders());
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
            targetDict = new List<Raider>(RaiderDB.GetInstance().GetAllRaiders());
            foreach (Raider target in targetDict)
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
