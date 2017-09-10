using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PermanentAoe : MonoBehaviour {
    //private IRaider target = null;
    private List<IRaider> targetDict = new List<IRaider>();
    private Coroutine timer;
    //private bool canAttack = false;
    private float swingTimerCurrent;
    public float swingTimer;
    public float dmgAoe;
    public float multiplyer;

    void Start()
    {
        if (GameControl.control.difficulty == 0)
        {
            dmgAoe *= GameControl.control.easyMultiplyer;
        }
    }

    void FixedUpdate()
    {
        swingTimerCurrent += 0.02f;
        if (swingTimerCurrent >= swingTimer)
        {
            targetDict = new List<IRaider>(RaiderDB.GetInstance().GetAllRaider());
            foreach (IRaider raider in targetDict)
            {
                raider.ReduceHP(dmgAoe);
            }
            swingTimerCurrent = 0;
            dmgAoe += multiplyer;
        }
    }
}
