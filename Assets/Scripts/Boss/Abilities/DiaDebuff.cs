﻿using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;


/// <summary>
/// Debuff used in Scene 12 (currently not finished)
/// </summary>
public class DiaDebuff : MonoBehaviour, IBuff
{
    private Color32 debuffColor = new Color32(170, 0, 255, 255);
    private List<Raider> targetDict = new List<Raider>();
    private Raider target = null;
    public Material image;
    public float damagePerTick = 5f;
    public float damageIncrease = 2f;
    public float tickLength = 2f;
    public int numberTargets = 2;
    private Raider raider;

    void Start()
    {
        image = Resources.Load("Feuer_Debuff", typeof(Material)) as Material;
        if (GameControl.control.difficulty == 0)
        {
            damagePerTick *= GameControl.control.easyMultiplier;
            damageIncrease *= GameControl.control.easyMultiplier;
        }
        raider = GetComponent<Raider>();
        StartCoroutine(ApplyDamage());
    }

    IEnumerator ApplyDamage()
    {
        while (1 == 1)
        {
            yield return new WaitForSeconds(tickLength);
            raider.Damage(damagePerTick);
            damagePerTick += damageIncrease;
        }
    }

    public float GetDuration()
    {
        return -1;
    }

    public Material GetMaterial()
    {
        return image;
    }

    public string GetTimeLeft()
    {
        return (" ");
    }

    public bool IsBuff()
    {
        return false;
    }

    public void Destroy()
    {
        jump();
        GetComponent<BuffManager>().DeregisterBuff(this);
        Destroy(this);
    }

    public void jump()
    {
        targetDict = new List<Raider>(RaiderDB.GetInstance().GetAllRaiders());
        for (int i = targetDict.Count - 1; i >= 0; i--)
        {
            if (targetDict[i].GetGameObject().GetComponent<DiaDebuff>() != null)
                targetDict.RemoveAt(i);
        }
        for (int i = 0; i < numberTargets; i++)
        {
            if (target != null && targetDict.Count > 1)
            {
                targetDict.Remove(target);
            }
            target = targetDict[UnityEngine.Random.Range(0, targetDict.Count)];
            DiaDebuff debuff = target.GetGameObject().AddComponent<DiaDebuff>();
            target.GetGameObject().GetComponent<BuffManager>().RegisterBuff(debuff);
        }
    }

    public float OnGlobalDamageTaken(float amount)
    {
        return amount;
    }

    public float OnGlobalHealingTaken(float amount)
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

    public bool IsDispellable()
    {
        return true;
    }

    public void Reset()
    {
    }
}