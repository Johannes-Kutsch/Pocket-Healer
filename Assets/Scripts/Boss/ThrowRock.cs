using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ThrowRock : MonoBehaviour {
    private IRaider target = null;
    private List<IRaider> targetDict = new List<IRaider>();

    private bool canAttack = false;
    public float swingTimerCurrent;
    public float swingTimer;
    public float dmg;
    public int numberTargets;

       void Start()
       {
        if (GameControl.control.difficulty == 0)
        {
            dmg *= GameControl.control.easyMultiplyer;
        }
        swingTimerCurrent = -2f;
       }

       void FixedUpdate()
       {
           swingTimerCurrent += 0.02f;
           if(swingTimerCurrent > swingTimer - 2.05f && swingTimerCurrent < swingTimer - 1.95f)
               GetComponent<Boss>().SetEmoteText(" hebt einen Stein auf.");
           if (swingTimerCurrent >= swingTimer)
           {
               targetDict = new List<IRaider>(RaiderDB.GetInstance().GetAllDDs());
               for (int i = 0; i < numberTargets; i++) {
                   if (target != null && targetDict.Count > 1)
                   {
                       targetDict.Remove(target);
                   }
                   target = targetDict[Random.Range(0, targetDict.Count)];
                   target.ReduceHP(dmg);
               }
               swingTimerCurrent = 0f;
           }
       }
}
