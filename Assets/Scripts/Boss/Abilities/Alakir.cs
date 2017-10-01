using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Alakir : MonoBehaviour {
    private int phaseID = 1;
    private float timeInPhase;
    private float fireDotTimerCurrent;
    private float frostTimerCurrent;
    private float rangeAttackTimerCurrent;
    private IRaider rangeTarget = null;
    private IRaider feuerTarget = null;

    public float rangeAttackTimer;
    public float rangeAttackDamage;
    public float frostTimer;
    public float frostDamage;
    public float fireDotTimer;
    public float fireDotTimerStart;
    public float phaseduration;
    public string emotePhaseOne = "nimmt seine Luftgestalt an. Deine verursachte Heilung nimmt langsam ab.";
    public string emotePhaseTwo = "nimmt seine Wassergestalt an. Ein Blizzard zieht auf.";
    public string emotePhaseThree = "nimmt seine Feuergestalt an.";

    void Start()
    {
        fireDotTimerCurrent = fireDotTimerStart;

        if (GameControl.control.difficulty == 0)
        {
            rangeAttackDamage *= GameControl.control.easyMultiplyer;
            frostDamage *= GameControl.control.easyMultiplyer;
        }
    }

    void FixedUpdate()
    {
        timeInPhase += 0.02f;
        switch (phaseID)
        {
            case 1:
                FirePhase();
                break;

            case 2:
                AirPhase();
                break;

            case 3:
                WaterPhase();
                break;
        }
    }

    private void FirePhase()
    {
        fireDotTimerCurrent += 0.02f;
        rangeAttackTimerCurrent += 0.02f;

        if (fireDotTimerCurrent >= fireDotTimer) //apply a new Fire Dot
        {
            fireDotTimerCurrent = 0f;
            List<IRaider> targetDict = new List<IRaider>(RaiderDB.GetInstance().GetAllDDs());

            foreach (IRaider raider in targetDict)
            {
                if (!raider.GetGameObject().GetComponent<AlakirFeuerDebuff>())
                {
                    feuerTarget = raider;
                }
            }

            if (feuerTarget == null)
            {
                targetDict = new List<IRaider>(RaiderDB.GetInstance().GetAllRaiders());
                foreach (IRaider raider in targetDict)
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

        else if (rangeAttackTimerCurrent >= rangeAttackTimer) //Range AutoAttack
        {
            rangeAttackTimerCurrent = 0f;

            List<IRaider> targetDict = new List<IRaider>(RaiderDB.GetInstance().GetAllDDs());

            if (rangeTarget != null && targetDict.Count > 1)
            {
                targetDict.Remove(rangeTarget);
            }

            rangeTarget = targetDict[Random.Range(0, targetDict.Count)];
            rangeTarget.Damage(rangeAttackDamage);
        }

        else if (timeInPhase > phaseduration - 2.05f && timeInPhase < phaseduration - 1.95f) //emote for air phase
            GetComponent<Boss>().SetEmoteText(" " + emotePhaseOne);

        else if (timeInPhase > phaseduration) //switch to air phase
        {
            List<IRaider> targetDict = new List<IRaider>(RaiderDB.GetInstance().GetAllRaiders());

            foreach (IRaider target in targetDict)
            {
                AlakirWasserDebuff debuff = target.GetGameObject().AddComponent<AlakirWasserDebuff>();
                target.GetGameObject().GetComponent<BuffManager>().RegisterBuff(debuff);
            }

            fireDotTimerCurrent = 0f;
            phaseID = 2;
            timeInPhase = 0;
        }
    }

    private void AirPhase()
    {
        if (timeInPhase > phaseduration - 2.05f && timeInPhase < phaseduration - 1.95f) //emote for water phase
            GetComponent<Boss>().SetEmoteText(" " + emotePhaseTwo);

        else if (timeInPhase > phaseduration) //switch to water phase
        {
            List<IRaider> targetDict = new List<IRaider>(RaiderDB.GetInstance().GetAllRaiders());

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

            phaseID = 3;
            timeInPhase = 0;
        }
    }

    private void WaterPhase()
    {
        frostTimerCurrent += 0.02f;

        if (frostTimerCurrent >= frostTimer)
        {
            frostTimerCurrent = 0f;

            List<IRaider> targetDict = new List<IRaider>(RaiderDB.GetInstance().GetAllRaiders());

            foreach (IRaider raider in targetDict)
            {
                raider.Damage(frostDamage);
            }
        }

        if (timeInPhase > phaseduration - 2.05f && timeInPhase < phaseduration - 1.95f) //emote for fire phase
            GetComponent<Boss>().SetEmoteText(" " + emotePhaseThree);

        else if (timeInPhase > phaseduration) //siwtch to fire phase
        {
            phaseID = 1;
            timeInPhase = 0;
            fireDotTimerCurrent = fireDotTimerStart;
        }
    }
}

