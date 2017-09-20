using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ApplyStone : MonoBehaviour {
    private float swingTimerCurrent;
    public float swingTimer;
    public float swingTimerStart;
    private IRaider target = null;
    private List<IRaider> targetDict;
    public string emote = "lässt zwei Spieler zu stein erstarren.";

    void Start()
    {
        swingTimerCurrent = swingTimerStart;
    }

    void FixedUpdate()
    {
        swingTimerCurrent += 0.02f;
        if(swingTimerCurrent >= swingTimer)
        {
            targetDict = new List<IRaider>(RaiderDB.GetInstance().GetAllDDs());
            foreach (IRaider raider in targetDict)
            {
                if (!raider.GetGameObject().GetComponent<SteinOne>() && !raider.GetGameObject().GetComponent<SteinTwo>())
                {
                    target = raider;
                }
            }
            if (target == null)
            {
                targetDict = new List<IRaider>(RaiderDB.GetInstance().GetAllRaider());
                foreach (IRaider raider in targetDict)
                {
                    if (!raider.GetGameObject().GetComponent<SteinOne>() && !raider.GetGameObject().GetComponent<SteinTwo>())
                    {
                        target = raider;
                    }
                }
            }
            if (target != null)
            {
                target.Damage(20f);
                SteinOne debuff = target.GetGameObject().AddComponent<SteinOne>();
                target.GetGameObject().GetComponent<BuffManager>().RegisterBuff(debuff);
                target = null;
            }
            targetDict = new List<IRaider>(RaiderDB.GetInstance().GetAllDDs());
            foreach (IRaider raider in targetDict)
            {
                if (!raider.GetGameObject().GetComponent<SteinOne>() && !raider.GetGameObject().GetComponent<SteinTwo>())
                {
                    target = raider;
                }
            }
            if (target == null)
            {
                targetDict = new List<IRaider>(RaiderDB.GetInstance().GetAllRaider());
                foreach (IRaider raider in targetDict)
                {
                    if (!raider.GetGameObject().GetComponent<SteinOne>() && !raider.GetGameObject().GetComponent<SteinTwo>())
                    {
                        target = raider;
                    }
                }
            }
            if (target != null)
            {
                target.Damage(30f);
                SteinTwo debuff = target.GetGameObject().AddComponent<SteinTwo>();
                target.GetGameObject().GetComponent<BuffManager>().RegisterBuff(debuff);
                target = null;
            }
            swingTimerCurrent = 0f;
        }
        else if (swingTimerCurrent > swingTimer - 2.05f && swingTimerCurrent < swingTimer - 1.95f)
            GetComponent<Boss>().SetEmoteText(" " + emote);
    }

}
