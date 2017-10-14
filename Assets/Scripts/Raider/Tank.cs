using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

/// <summary>
/// The script used for all tanking partymembers
/// </summary>
public class Tank : Raider {
    private float swingTimerTop = 0.8f; //time between each hit
    private float swingTimerBot = 0.4f;
    private float dmg = 10f; //dmg with each hit

    /// <summary>
    /// Called when the Start() method of the base class finished initialisation.
    /// </summary>
    public void Awake()
    {
        RaiderDB.GetInstance().RegisterTank(this);
    }

    /// <summary>
    /// Called when the Die() method in the base class is called.
    /// </summary>
    public override void OnDie()
    {
        RaiderDB.GetInstance().DeRegisterTank(this);
    }

    /// <summary>
    /// Called when the object is removed.
    /// </summary>
    public override void OnDestroy()
    {
        RaiderDB.GetInstance().DeRegisterTank(this);
    }

    /// <summary>
    /// Called when the raider triggers a swing.
    /// </summary>
    public override void OnSwing()
    {
        gamestate.GetBoss().TakeDamage(dmg);
    }

    /// <summary>
    /// Gets the swing timer.
    /// </summary>
    /// <returns></returns>
    public override float GetSwingTimer()
    {
        return UnityEngine.Random.Range(swingTimerBot, swingTimerTop);
    }
}
