using UnityEngine;
using System.Collections.Generic;

public class AutoAttackRange : MonoBehaviour {
    private IRaider target = null;
    private List<IRaider> targetDict = new List<IRaider>();

    public float swingTimerCurrent;
    public float swingTimer;
    public float dmg;
    public float multiplyer;

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
        if (swingTimerCurrent >= swingTimer && !Gamestate.gamestate.paused)
        {
            targetDict = new List<IRaider>(RaiderDB.GetInstance().GetAllDDs());
            if (target != null && targetDict.Count > 1)
            {
                targetDict.Remove(target);
            }
            target = targetDict[Random.Range(0, targetDict.Count)];
            target.ReduceHP(dmg);
            dmg += multiplyer;
            swingTimerCurrent = 0f;
        }
    }
}
