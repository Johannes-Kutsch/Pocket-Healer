using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class SteinOne : MonoBehaviour, IBuff
{
    private List<IRaider> targetDict;
    public float exploDmg = 70f;
    public Material image;
    public float duration = 10f;
    public float runtime;
    public float damagePerTick = 20f;
    public float tickLength = 2f;
    private int ticksSinceLastEffect = 0;
    private IRaider raider;

    void Start()
    {
        image = Resources.Load("Stein_1", typeof(Material)) as Material;
        if (GameControl.control.difficulty == 0)
        {
            damagePerTick *= GameControl.control.easyMultiplyer;
            exploDmg *= GameControl.control.easyMultiplyer;
        }
        raider = GetComponent<IRaider>();
        StartCoroutine(ApplyDamage());
    }

    void FixedUpdate()
    {
        runtime = runtime + 0.02f;
        if (raider.GetHealth() >= 1f)
        {
            targetDict = RaiderDB.GetInstance().GetAllRaider();
            foreach (IRaider target in targetDict)
                target.ReduceHP(exploDmg);
            Destroy();
        }
    }

    IEnumerator ApplyDamage()
    {
        float ticks = (duration / tickLength);
        for (ticksSinceLastEffect = 0; ticksSinceLastEffect < ticks; ticksSinceLastEffect++)
        {
            yield return new WaitForSeconds(tickLength);
            raider.ReduceHP(damagePerTick);
        }
        Destroy();
    }

    public float GetDuration()
    {
        return -1;
    }

    public Material GetMaterial()
    {
        return image;
    }

    public string GetRemainingTime()
    {
        return (duration - runtime).ToString("F0");
    }

    public bool IsBuff()
    {
        return false;
    }

    public void Destroy()
    {
        GetComponent<BuffManager>().DeregisterBuff(this);
        Destroy(this);
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
        return amount;
    }

    public bool IsDispellable()
    {
        return false;
    }

    public void Reset()
    {
        runtime = 0;
    }
}
