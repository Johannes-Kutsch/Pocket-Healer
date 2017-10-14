using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// The script used for all damage dealing partymembers
/// </summary>
public class DD : Raider
{
    private float swingTimerTop = 0.8f; //time between each hit
    private float swingTimerBot = 0.4f;
    private float dmg = 10f; //dmg with each hit

    /// <summary>
    /// Called when the Start() method of the base class finished initialisation.
    /// </summary>
    public void Awake()
    {
        RaiderDB.GetInstance().RegisterDD(this);
    }

    /// <summary>
    /// Called when the Die() method in the base class is called.
    /// </summary>
    public override void OnDie()
    {
        RaiderDB.GetInstance().DeRegisterDD(this);
    }

    /// <summary>
    /// Called when the object is removed.
    /// </summary>
    public override void OnDestroy()
    {
        RaiderDB.GetInstance().DeRegisterDD(this);
    }

    /// <summary>
    /// Called when the raider triggers a swing.
    /// </summary>
    public override void OnSwing()
    {
        gamestate.GetBoss().TakeDamage(dmg);
        base.swingTimer = UnityEngine.Random.Range(swingTimerBot, swingTimerTop);
    }
}

