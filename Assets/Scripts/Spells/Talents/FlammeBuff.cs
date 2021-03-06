﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class FlammeBuff : MonoBehaviour, IBuff {
    private List<Raider> raiderDict;
    public Material image;
    private float scaler = 0.2f;

    private Raider raider;

    void Start()
    {
        image = Resources.Load("Flamme_Buff", typeof(Material)) as Material;
        raider = GetComponent<Raider>();
    }

    void FixedUpdate()
    {

    }

    public void Reset()
    {

    }


    public float GetDuration()
    {
        return 100000000000;
    }

    public Material GetMaterial()
    {
        return image;
    }

    public string GetTimeLeft()
    {
        return "";
    }

    public float OnGlobalDamageTaken(float amount)
    {
        return amount;
    }

    public float OnHealingTaken(float amount)
    {
        return amount;
    }

    public float OnDamageTaken(float amount)
    {
        return amount;
    }

    public float OnFatalDamage(float amount)
    {
        return amount;
    }

    public float OnGlobalHealingTaken(float amount)
    {
        raider.HealSimple(amount * scaler, true);
        return amount;
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