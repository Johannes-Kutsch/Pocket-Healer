using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Randomly damages a single raider, prefers dds
/// </summary>
public class AutoAttackRange : MonoBehaviour {
    public IRaider target = null;
    private List<IRaider> targetDict = new List<IRaider>();

    public int levelIndex;

    private float swingTimerCurrent;
    private float swingTimer;
    private float dmg;
    private float multiplieer;

    /// <summary>
    /// Called on start.
    /// </summary>
    void Start()
    {
        Settings settings = new Settings(levelIndex);

        swingTimerCurrent = settings.rangeAutoAttackSwingTimerStart;
        swingTimer = settings.rangeAutoAttackSwingTimer;
        dmg = settings.rangeAutoAttackDmg;
        multiplieer = settings.rangeAutoAttackMultiplier;

        if (GameControl.control.difficulty == 0)
        {
            dmg *= GameControl.control.easyMultiplier;
        }
    }

    /// <summary>
    /// Called on every fixed update, selects a target randomly and damages it.
    /// If atleast one dd is alive, select one at random, else select a tank.
    /// </summary>
    void FixedUpdate()
    {
        if (!Gamestate.gamestate.GetPaused())
        {
            swingTimerCurrent += 0.02f;
            if (swingTimerCurrent >= swingTimer)
            {
                targetDict = new List<IRaider>(RaiderDB.GetInstance().GetAllDDs());
                int numberTargets = targetDict.Count;

                if (numberTargets <= 0) //no dd is alive, load all raider
                {
                    targetDict = new List<IRaider>(RaiderDB.GetInstance().GetAllRaiders());
                    numberTargets = targetDict.Count;
                }

                if (target != null && targetDict.Count > 1 && targetDict.Contains(target))
                {
                    targetDict.Remove(target);
                }

                target = targetDict[Random.Range(0, targetDict.Count)];
                target.Damage(dmg);
                dmg += multiplieer;
                swingTimerCurrent = 0f;
            }
        }
        
    }
}
