using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class AutoAttack : MonoBehaviour {
    private Coroutine timer;
    private List<IRaider> targetDict = new List<IRaider>();
    private IRaider target = null;

    public int levelIndex;

    private bool canAttack = false;
    public float swingTimer;
    public float changeTargetTimer;
    private float onTarget = 0f;
    public float dmg;
    public float multiplier;
    private int numberTargets = 0;

    void Start()
    {
        Settings settings = new Settings(levelIndex);

        swingTimer = settings.autoAttackSwingTimer;
        changeTargetTimer = settings.autoAttackChangeTargetTimer;
        dmg = settings.autoAttackDmg;
        multiplier = settings.autoAttackMultiplier;

        if (GameControl.control.difficulty == 0)
        {
            dmg *= GameControl.control.easyMultiplyer;
        }
        onTarget = changeTargetTimer;
        timer = StartCoroutine(Timer(swingTimer));
    }

    void FixedUpdate()
    {
        if (target == null || !target.IsAlive())
        {
            ChangeTarget();
        }
        onTarget += 0.02f;
        if (onTarget >= changeTargetTimer || !target.IsAlive())
        {
            ChangeTarget();
        }
        if (canAttack && !Gamestate.gamestate.GetPaused())
        {
            target.Damage(dmg);
            timer = StartCoroutine(Timer(swingTimer));
            dmg += multiplier;
        }
    }

    private void ChangeTarget()
    {
        targetDict = new List<IRaider>(RaiderDB.GetInstance().GetAllTanks());
        numberTargets = targetDict.Count;
        if (numberTargets <= 0)
        {
            targetDict = new List<IRaider>(RaiderDB.GetInstance().GetAllRaider());
            numberTargets = targetDict.Count;
            dmg *= 4;
        }
        if(numberTargets > 1)
        {
            targetDict.Remove(target);
            numberTargets--;
            if(target != null)
            {
                target.SetBossTarget(false);
            }
            target = targetDict.First();
            if (target != null)
            {
                target.SetBossTarget(true);
            }
        }
        else if (numberTargets > 0)
        {
            numberTargets--;
            if (target != null)
            {
                target.SetBossTarget(false);
            }
            target = targetDict.First();
            if (target != null)
            {
                target.SetBossTarget(true);
            }
            targetDict.Remove(target);
        }
        onTarget = 0f;
    }

    IEnumerator Timer(float timeToWait)
    {
        canAttack = false;
        yield return new WaitForSeconds(timeToWait);
        canAttack = true;
    }
}
