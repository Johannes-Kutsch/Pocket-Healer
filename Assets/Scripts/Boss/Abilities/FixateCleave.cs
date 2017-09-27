using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;

/// <summary>
/// Alterantes between a cleave that hity both tanks and multiple hits on one tank that get stronger with each hit.
/// </summary>
public class FixateCleave : MonoBehaviour {

    public int levelIndex;
    public Image BossModImageCleave;
    private Image cooldownOverlayCleave;
    public Image BossModImageFixate;
    private Image cooldownOverlayFixate;

    public string emoteText1 = "reaches back for a mighty swing.";
    public string emoteText2 = "is about to unleash a fast flurry of swings.";
    
    private float swingTimer; //time between every cleave or fixate
    private float swingTimerCleave;
    private float swingTimerCleaveCurrent = 0f;
    private float swingTimerFixate;
    private float swingTimerFixateCurrent = 0f;

    private int cleavesBetweenFixate;
    private float cleavesBetweenFixateRemaining;
    private float dmgFixateCurrent;
    private float dmgCleave;

    private int countFixate = 0;
    private int hitsFixate;
    private float dmgFixate;
    private float multiplierFixate;
    private static float DELAYFIXATE = 1f;

    private IRaider target = null;

    private float swingTimerAutoAttack;
    private float dmgAutoAttack;
    private float multiplierAutoAttack;
    private bool canAttack = false;
    private Coroutine timer;

    /// <summary>
    /// Called on start.
    /// </summary>
    void Start()
    {
        Settings settings = new Settings(levelIndex);

        swingTimerAutoAttack = settings.autoAttackSwingTimer;
        dmgAutoAttack = settings.autoAttackDmg;
        multiplierAutoAttack = settings.autoAttackMultiplier;
        swingTimer = settings.fixateCleaveSwingTimer;
        cleavesBetweenFixate = settings.fixateCleaveCleaveBetweenFixate;
        dmgCleave = settings.fixateCleaveDmgCleave;
        hitsFixate = settings.fixateCleaveHitsFixate;
        dmgFixate = settings.fixateCleaveDmgFixate;
        multiplierFixate = settings.fixateCleaveMultiplierFixate;

        swingTimerCleave = swingTimer;
        swingTimerFixate = swingTimer * (cleavesBetweenFixate + 1);

        Image[] cooldownOverlays = BossModImageCleave.GetComponentsInChildren<Image>();

        foreach (Image image in cooldownOverlays)
        {
            if (image.transform != BossModImageCleave.transform)
            {
                cooldownOverlayCleave = image;
            }
        }

        BossModImageCleave.enabled = true;
        cooldownOverlayCleave.enabled = true;

        cooldownOverlays = BossModImageFixate.GetComponentsInChildren<Image>();

        foreach (Image image in cooldownOverlays)
        {
            if (image.transform != BossModImageFixate.transform)
            {
                cooldownOverlayFixate = image;
            }
        }

        BossModImageFixate.enabled = true;
        cooldownOverlayFixate.enabled = true;

        if (GameControl.control.difficulty == 0)
        {
            dmgCleave *= GameControl.control.easyMultiplyer;
            dmgFixate *= GameControl.control.easyMultiplyer;
            dmgAutoAttack *= GameControl.control.easyMultiplyer;
        }

        dmgFixateCurrent = dmgFixate;
        cleavesBetweenFixateRemaining = cleavesBetweenFixate;
        timer = StartCoroutine(Timer(0.5f));
    }

    /// <summary>
    /// Called on every fixet update.
    /// </summary>
    void FixedUpdate()
    {
        if (target == null && canAttack || target != null && !target.IsAlive() && canAttack) //Change target if has no target or target is dead
        {
            ChangeTarget();
        }

        if (canAttack) //AutoAttack Damage
        {
            target.Damage(dmgAutoAttack);
            timer = StartCoroutine(Timer(swingTimerAutoAttack));
            dmgAutoAttack += multiplierAutoAttack;
        }

        swingTimerCleaveCurrent += 0.02f;
        swingTimerFixateCurrent += 0.02f;


        if (swingTimerCleaveCurrent > swingTimerCleave - 2.05f && swingTimerCleaveCurrent < swingTimerCleave - 1.95f)
            GetComponent<Boss>().SetEmoteText(" " + emoteText1);
        else if (swingTimerFixateCurrent > swingTimerFixate - 2.05f && swingTimerFixateCurrent < swingTimerFixate - 1.95f)
            GetComponent<Boss>().SetEmoteText(" " + emoteText2);

        //cooldownoverlay cleave
        cooldownOverlayCleave.fillAmount = swingTimerCleaveCurrent / swingTimerCleave;

        //cooldownoverlay fixate
        cooldownOverlayFixate.fillAmount = swingTimerFixateCurrent / swingTimerFixate;

        if (swingTimerCleaveCurrent >= swingTimerCleave) //cleave
        {
            AttackCleave();
            cleavesBetweenFixateRemaining--;
            swingTimerCleaveCurrent = 0f;

            if (cleavesBetweenFixateRemaining > 0)
            {
                swingTimerCleave = swingTimer;
            }
            else
            {
                swingTimerCleave = swingTimer * 2 + DELAYFIXATE * (hitsFixate - 1);
            }
        }

        if (swingTimerFixateCurrent >= swingTimerFixate) //fixate
        {
            StopAllCoroutines(); //stop autoattacks
            canAttack = false;

            AttackFixate();

            countFixate++;
            swingTimerFixateCurrent = 0f;
            if (countFixate >= hitsFixate)
            {
                ChangeTarget();
                timer = StartCoroutine(Timer(0.5f));
                dmgFixateCurrent = dmgFixate;
                countFixate = 0;
                cleavesBetweenFixateRemaining = cleavesBetweenFixate;
                swingTimerFixate = swingTimer * (cleavesBetweenFixate + 1);
                swingTimerCleaveCurrent = swingTimer + DELAYFIXATE * (hitsFixate - 1);
            } else
            {
                swingTimerFixate = DELAYFIXATE;
            }

        }
    }

    /// <summary>
    /// Changes the current target, if 2 tanks are alive the other tank is selected, if only one tank is alive he satys the target, if no tank is alive a dd is selected.
    /// </summary>
    private void ChangeTarget()
    {
        List<IRaider> targetDict = new List<IRaider>(RaiderDB.GetInstance().GetAllTanks());
        int numberTargets = targetDict.Count;
        if (numberTargets <= 0)
        {
            targetDict = new List<IRaider>(RaiderDB.GetInstance().GetAllRaiders());
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
        else if (numberTargets == 1)
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

    /// <summary>
    /// The Cleave logik, hits both tanks if alive, if only one tank is alive the cleave damage is deacreased and every raider is damaged.
    /// </summary>
    private void AttackCleave()
    {
        float dmgCleaveActual = dmgCleave;
        List<IRaider> targetDictCleave = new List<IRaider>(RaiderDB.GetInstance().GetAllTanks());
        if (targetDictCleave.Count <= 1)
        {
            targetDictCleave = new List<IRaider>(RaiderDB.GetInstance().GetAllRaiders());
            dmgCleaveActual *= 0.5f;
        }
        foreach (IRaider raider in targetDictCleave)
            raider.Damage(dmgCleaveActual);
    }

    /// <summary>
    /// The fixate logik
    /// </summary>
    private void AttackFixate()
    {
        target.Damage(dmgFixateCurrent);
        dmgFixateCurrent *= multiplierFixate;
    }

    /// <summary>
    /// The Autoattack timer
    /// </summary>
    /// <param name="timeToWait">The time to wait.</param>
    /// <returns></returns>
    IEnumerator Timer(float timeToWait)
    {
        canAttack = false;
        yield return new WaitForSeconds(timeToWait);
        canAttack = true;
    }
}
