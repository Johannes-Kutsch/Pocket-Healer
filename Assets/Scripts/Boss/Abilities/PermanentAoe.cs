using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Script used to damage every raider in a fixed intervall.
/// </summary>
public class PermanentAoe : MonoBehaviour {
    public int levelIndex;
    private Coroutine timer;
    private float swingTimerCurrent;
    private float swingTimer;
    private float dmgAoe;
    private float multiplier;

    /// <summary>
    /// Called on start.
    /// </summary>
    void Start()
    {
        Settings settings = new Settings(levelIndex);

        swingTimer = settings.permanentAoeSwingTimer;
        dmgAoe = settings.permanentAoeDmg;
        multiplier = settings.permanentAoeMultiplier;

        if (GameControl.control.difficulty == 0)
        {
            dmgAoe *= GameControl.control.easyMultiplier;
        }
    }

    /// <summary>
    /// Called on every fixed update.
    /// </summary>
    void FixedUpdate()
    {
        swingTimerCurrent += 0.02f;
        if (swingTimerCurrent >= swingTimer)
        {
            List<IRaider> targetDict = new List<IRaider>(RaiderDB.GetInstance().GetAllRaiders());
            foreach (IRaider raider in targetDict)
            {
                raider.Damage(dmgAoe);
            }
            swingTimerCurrent = 0;
            dmgAoe += multiplier;
        }
    }
}
