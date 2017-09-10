﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class SchutzgeistBuffInvis : MonoBehaviour, IBuff, ISchutzgeist {
    private List<IRaider> raiderDict;
    public Material image = null;
    public float duration = 14f;
    public float runtime;
    public float timeLeft;

    private IRaider raider;

    void Start()
    {
        raider = GetComponent<IRaider>();
    }

    void FixedUpdate()
    {
        runtime = runtime + (float)0.02;
        timeLeft = duration - runtime;
        if (timeLeft <= 0)
        {
            Destroy();
        }
    }

    public void Reset()
    {

    }

    public float GetDuration()
    {
        return 99999999999;
    }

    public Material GetMaterial()
    {
        return image;
    }

    public string GetRemainingTime()
    {
        return " ";
    }

    public float GlobalDamageTaken(float amount)
    {
        return amount;
    }

    public float GlobalHealingTaken(float amount)
    {
        return amount;
    }

    public float HealingTaken(float amount)
    {
        return amount;
    }

    public float DamageTaken(float amount)
    {
        return amount;
    }

    public float FatalDamage(float amount)
    {
        raider.HealSimple(300, false);
        Destroy();
        raiderDict = RaiderDB.GetInstance().GetAllRaiderSortetByHealth();
        raiderDict.Remove(raider);
        foreach (IRaider target in raiderDict)
        {
            ISchutzgeist buff = target.GetGameObject().GetComponent<ISchutzgeist>();
            target.GetGameObject().GetComponent<BuffManager>().DeregisterBuff(buff);
            buff.Destroy();
        }
        return 0;
    }

    public bool IsBuff()
    {
        return true;
    }

    public bool IsDispellable()
    {
        return false;
    }

    public void Destroy()
    {
        GetComponent<BuffManager>().DeregisterBuff(this);
        Destroy(this);
    }

}