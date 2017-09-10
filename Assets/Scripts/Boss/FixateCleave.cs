using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class FixateCleave : MonoBehaviour {
    private List<IRaider> targetDict = new List<IRaider>();
    //private List<IRaider> targetDictAA = new List<IRaider>();
    private IRaider target = null;
    private Coroutine timer;
    private bool canAttack = false;
    private float swingTimerCurrent = -1f;
    private int numberTargets = 0;
    public float swingTimer;
    public string emoteText1 = "holt zu einem mächtigem Schlag aus.";
    public string emoteText2 = "macht sich zu einer schnellen Abfolge von Schlägen bereit.";
    private List<IRaider> targetDictCleave = new List<IRaider>();
    private float cleavesBetweenFixateCurrent;
    private int countFixate = 0;
    private float dmgFixateActual;
    private float dmgCleaveActual;
    public float dmgCleave;
    public float cleavesBetweenFixate;
    public float hitsFixate;
    public float dmgFixate;
    public float multiplyerFixate;
    public float swingTimerAutoAttack;
    public float dmgAutoAttack;
    public float multiplyerAutoAttack;
    




    void Start()
    {
        if (GameControl.control.difficulty == 0)
        {
            dmgCleave *= GameControl.control.easyMultiplyer;
            dmgFixate *= GameControl.control.easyMultiplyer;
            dmgAutoAttack *= GameControl.control.easyMultiplyer;
        }
        dmgFixateActual = dmgFixate;
        cleavesBetweenFixateCurrent = cleavesBetweenFixate;
        timer = StartCoroutine(Timer(0.5f));
    }

    void FixedUpdate()
    {
        if (target == null && canAttack || target != null && !target.IsAlive() && canAttack)
        {
            ChangeTarget();
        }
        if (canAttack)
        {
            target.ReduceHP(dmgAutoAttack);
            timer = StartCoroutine(Timer(swingTimerAutoAttack));
            dmgAutoAttack += multiplyerAutoAttack;
        }
        swingTimerCurrent += 0.02f;
        if (swingTimerCurrent > swingTimer - 2.05f && swingTimerCurrent < swingTimer - 1.95f && cleavesBetweenFixateCurrent > 0)
            GetComponent<Boss>().SetEmoteText(" " + emoteText1);
        else if (swingTimerCurrent > swingTimer - 2.05f && swingTimerCurrent < swingTimer - 1.95f && cleavesBetweenFixateCurrent <= 0)
            GetComponent<Boss>().SetEmoteText(" " + emoteText2);
        if (swingTimerCurrent >= swingTimer && cleavesBetweenFixateCurrent > 0)
        {
            dmgCleaveActual = dmgCleave;
            targetDictCleave = new List<IRaider>(RaiderDB.GetInstance().GetAllTanks());
            if (targetDictCleave.Count <= 1)
            {
                targetDictCleave = new List<IRaider>(RaiderDB.GetInstance().GetAllRaider());
                dmgCleaveActual *= 0.5f;
            }
            foreach (IRaider raider in targetDictCleave)
                raider.ReduceHP(dmgCleaveActual);
            cleavesBetweenFixateCurrent--;
            swingTimerCurrent = 0f;
        }
        else if (swingTimerCurrent >= swingTimer && cleavesBetweenFixateCurrent <= 0)
        {
            StopAllCoroutines();
            canAttack = false;
            target.ReduceHP(dmgFixateActual);
            countFixate++;
            dmgFixateActual *= multiplyerFixate;
            swingTimerCurrent = swingTimer - 1f;
            if (countFixate >= hitsFixate)
            {
                ChangeTarget();
                timer = StartCoroutine(Timer(0.5f));
                dmgFixateActual = dmgFixate;
                countFixate = 0;
                cleavesBetweenFixateCurrent = cleavesBetweenFixate;
                swingTimerCurrent = 0f;
            }

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
            dmgAutoAttack *= 4;
        }
        if (numberTargets > 1)
        {
            targetDict.Remove(target);
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
        }
    }

    IEnumerator Timer(float timeToWait)
    {
        canAttack = false;
        yield return new WaitForSeconds(timeToWait);
        canAttack = true;
    }
}
