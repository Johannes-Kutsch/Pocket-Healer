using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class SteinTwo : MonoBehaviour, IBuff
{
    public Material image;
    public float damagePerTick = 20f;
    public float tickLength = 3f;
    //private int ticksSinceLastEffect = 0;
    private IRaider raider;

    void Start()
    {
        image = Resources.Load("Stein_2", typeof(Material)) as Material;
        if (GameControl.control.difficulty == 0)
        {
            damagePerTick *= GameControl.control.easyMultiplyer;
        }
        raider = GetComponent<IRaider>();
        StartCoroutine(ApplyDamage());
    }
    
    void FixedUpdate()
    {
        if (raider.GetHealth() >= 1f)
        {
            GetComponent<BuffManager>().DeregisterBuff(this);
            Destroy(this);
        }
    }

    IEnumerator ApplyDamage()
    {
        while (1 == 1)
        {
            yield return new WaitForSeconds(tickLength);
            raider.Damage(damagePerTick);
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

    public string GetRemainingTime()
    {
        return " ";
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
    }
}
