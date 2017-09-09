using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Alakir : MonoBehaviour {
    private int phasenID = 1;
    private float timeInPhase;
    private float feuerDotTimerCurrent;
    private float frostTimerCurrent;
    private float rangeAttackTimerCurrent;
    private IRaider rangeTarget = null;
    private IRaider feuerTarget = null;
    private List<IRaider> targetDict;
    private List<IRaider> feuerTargetDict;

    public float rangeAttackTimer;
    public float rangeAttackDamage;
    public float frostTimer;
    public float frostDamage;
    public float feuerDotTimer;
    public float feuerDotTimerStart;
    public float phasendauer;
    public string emotePhaseOne = "nimmt seine Luftgestalt an. Deine verursachte Heilung nimmt langsam ab.";
    public string emotePhaseTwo = "nimmt seine Wassergestalt an. Ein Blizzard zieht auf.";
    public string emotePhaseThree = "nimmt seine Feuergestalt an.";

    void Start()
    {
        feuerDotTimerCurrent = feuerDotTimerStart;
        if (GameControl.control.difficulty == 0)
        {
            rangeAttackDamage *= GameControl.control.easyMultiplyer;
            frostDamage *= GameControl.control.easyMultiplyer;
        }
    }

    void FixedUpdate()
    {
        timeInPhase += 0.02f;
        switch (phasenID)
        {
            case 1:
                feuerDotTimerCurrent += 0.02f;
                rangeAttackTimerCurrent += 0.02f;
                if (feuerDotTimerCurrent >= feuerDotTimer)
                {
                    feuerDotTimerCurrent = 0f;
                    feuerTargetDict = new List<IRaider>(RaiderDB.GetInstance().GetAllDDs());
                    foreach (IRaider raider in feuerTargetDict)
                    {
                        if (!raider.GetGameObject().GetComponent<AlakirFeuerDebuff>())
                        {
                            feuerTarget = raider;
                        }
                    }
                    if (feuerTarget == null)
                    {
                        feuerTargetDict = new List<IRaider>(RaiderDB.GetInstance().GetAllRaider());
                        foreach (IRaider raider in feuerTargetDict)
                        {
                            if (!raider.GetGameObject().GetComponent<AlakirFeuerDebuff>())
                            {
                                feuerTarget = raider;
                            }
                        }
                    }
                    if (feuerTarget != null)
                    {
                        AlakirFeuerDebuff debuff = feuerTarget.GetGameObject().AddComponent<AlakirFeuerDebuff>();
                        feuerTarget.GetGameObject().GetComponent<BuffManager>().RegisterBuff(debuff);
                        feuerTarget = null;
                    }
                }
                else if (rangeAttackTimerCurrent >= rangeAttackTimer)
                {
                    rangeAttackTimerCurrent = 0f;
                    targetDict = new List<IRaider>(RaiderDB.GetInstance().GetAllDDs());
                    if (rangeTarget != null && targetDict.Count > 1)
                    {
                        targetDict.Remove(rangeTarget);
                    }
                    rangeTarget = targetDict[Random.Range(0, targetDict.Count)];
                    rangeTarget.ReduceHP(rangeAttackDamage);
                }
                else if (timeInPhase > phasendauer - 2.05f && timeInPhase < phasendauer - 1.95f)
                    GetComponent<Boss>().SetEmoteText(" " + emotePhaseOne);
                else if (timeInPhase > phasendauer)
                {
                    targetDict = new List<IRaider>(RaiderDB.GetInstance().GetAllRaider());
                    foreach (IRaider target in targetDict)
                    {
                        AlakirWasserDebuff debuff = target.GetGameObject().AddComponent<AlakirWasserDebuff>();
                        target.GetGameObject().GetComponent<BuffManager>().RegisterBuff(debuff);
                    }
                    feuerDotTimerCurrent = 0f;
                    phasenID = 2;
                    timeInPhase = 0;
                }
                break;

            case 2:
                if (timeInPhase > phasendauer - 2.05f && timeInPhase < phasendauer - 1.95f)
                    GetComponent<Boss>().SetEmoteText(" " + emotePhaseTwo);
                else if (timeInPhase > phasendauer)
                {
                    targetDict = new List<IRaider>(RaiderDB.GetInstance().GetAllRaider());
                    foreach (IRaider target in targetDict)
                    {
                        AlakirFeuerDebuff feuerBuff = target.GetGameObject().GetComponent<AlakirFeuerDebuff>();
                        if (feuerBuff != null)
                        {
                            feuerBuff.Destroy();
                        }
                        AlakirWasserDebuff wasserBuff = target.GetGameObject().GetComponent<AlakirWasserDebuff>();
                        if (wasserBuff != null)
                        {
                            wasserBuff.Destroy();
                        }
                    }
                    phasenID = 3;
                    timeInPhase = 0;
                }
                break;

            case 3:
                frostTimerCurrent += 0.02f;
                if (frostTimerCurrent >= frostTimer)
                {
                    frostTimerCurrent = 0f;
                    targetDict = new List<IRaider>(RaiderDB.GetInstance().GetAllRaider());
                    foreach (IRaider raider in targetDict)
                    {
                        raider.ReduceHP(frostDamage);
                    }
                }
                if (timeInPhase > phasendauer - 2.05f && timeInPhase < phasendauer - 1.95f)
                    GetComponent<Boss>().SetEmoteText(" " + emotePhaseThree);
                else if (timeInPhase > phasendauer)
                {
                    phasenID = 1;
                    timeInPhase = 0;
                    feuerDotTimerCurrent = feuerDotTimerStart;
                }
                break;
        }
    }
}
