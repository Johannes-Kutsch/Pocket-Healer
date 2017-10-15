using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Controlls the Boss AutoAttack behaviour.
/// </summary>
public class AutoAttack : MonoBehaviour
{
    private List<IRaider> targetDict = new List<IRaider>();
    private IRaider target = null;

    public int levelIndex;

    private bool canAttack = false;
    private float swingTimer;
    private float currentSwingTimer = 0f;
    private float changeTargetTimer;
    private float currentTargetTimer = 0f;
    private float dmg;
    private float multiplier;

    /// <summary>
    /// Called on Start.
    /// </summary>
    void Start()
    {
        Settings settings = new Settings(levelIndex);

        swingTimer = settings.autoAttackSwingTimer;
        changeTargetTimer = settings.autoAttackChangeTargetTimer;
        dmg = settings.autoAttackDmg;
        multiplier = settings.autoAttackMultiplier;

        if (GameControl.control.difficulty == 0)
        {
            dmg *= GameControl.control.easyMultiplier;
        }

        currentTargetTimer = changeTargetTimer;
    }


    /// <summary>
    /// Called on every fixed update, i.e. 50 times a second.
    /// </summary>
    void FixedUpdate()
    {
        if (!Gamestate.gamestate.GetPaused()) {
            currentTargetTimer += 0.02f;
            currentSwingTimer += 0.02f;

            if (target == null || !target.IsAlive() || currentTargetTimer >= changeTargetTimer) //if we don't have a target, the target is dead or we are rdy for a targetchange, select a new target
            {
                ChangeTarget();
            }

            if (currentSwingTimer >= swingTimer) //the attack timer is rdy
            {
                target.Damage(dmg);
                dmg += multiplier;
                currentSwingTimer = 0f;
            }
        }
    }

    /// <summary>
    /// Changes the autoattack target.
    /// If atleast one tank is alive that is not currently the target, select one of the not selected tanks randomly as new target.
    /// If only the selected tank is alive, select him again.
    /// If no tank is alive, select one dd randomly, increase damage by 4 times.
    /// </summary>
    private void ChangeTarget()
    {
        targetDict = new List<IRaider>(RaiderDB.GetInstance().GetAllTanks());
        int numberTargets = targetDict.Count;

        if (numberTargets <= 0) //no tank is alive, load all raider
        {
            targetDict = new List<IRaider>(RaiderDB.GetInstance().GetAllRaiders());
            numberTargets = targetDict.Count;
            dmg *= 4;
        }

        if (numberTargets > 1) //more than one target, we can remove the current target and select a new target
        {
            targetDict.Remove(target);
            if (target != null)
            {
                target.SetBossTarget(false);
            }

            target = targetDict.First();
            if (target != null)
            {
                target.SetBossTarget(true);
            }
        }
        else if (numberTargets > 0) //only one target, select it
        {
            if (target != null)
            {
                target.SetBossTarget(false);
            }

            target = targetDict.First();
            if (target != null)
            {
                target.SetBossTarget(true);
            }
        }

        currentTargetTimer = 0f; //reset target and swing timer
        currentSwingTimer = 0f;
    }
}
