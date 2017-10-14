using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// The script used for all damage dealing partymembers
/// </summary>
public class DD : Raider
{
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
}

