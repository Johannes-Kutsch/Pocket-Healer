using UnityEngine;
using System.Collections;
using System;

public class MarkDebuff : MonoBehaviour, IBuff
{
    public Material image = Resources.Load("Marked_Debuff", typeof(Material)) as Material;
    public float duration = 18f;
    public float runtime;
    public float damagePerTick = 2f;
    public float maxDamagePerTick = 32f;
    public float multiplyer = 2f;
    public float tickLength = 1f;
    public float bossSchaden = 150f;
    public int ticksSinceLastEffect = 0;
    private IRaider raider;
    private Boss boss;

    void Start()
    {
        if (GameControl.control.difficulty == 0)
        {
            damagePerTick *= GameControl.control.easyMultiplyer;
            maxDamagePerTick *= GameControl.control.easyMultiplyer;
        }
        raider = GetComponent<IRaider>();
        boss = FindObjectOfType<Boss>();
        StartCoroutine(ApplyDamage());
    }

    void Update()
    {
    }

    void FixedUpdate()
    {
        runtime = runtime + 0.02f;
    }

    IEnumerator ApplyDamage()
    {
        float ticks = (duration / tickLength);
        for (ticksSinceLastEffect = 0; ticksSinceLastEffect < ticks; ticksSinceLastEffect++)
        {
            
            yield return new WaitForSeconds(tickLength);
            
            if (raider != null)
            {
                raider.ReduceHP(damagePerTick);
                boss.ErhalteSchaden(bossSchaden);
            }
            if (damagePerTick*multiplyer > maxDamagePerTick)
            {
                damagePerTick = maxDamagePerTick;
            } else
            {
                bossSchaden *= multiplyer;
                damagePerTick *= multiplyer;
            }
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